using EmloyeeManagement.Business.Commands;
using EmloyeeManagement.Business.Handlers;
using EmloyeeManagement.Business.ValidationRules.FluentValidation;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EmloyeeManagement.Business.Tests
{
    public class UpdateEmployeeHandlerTests
    {
        private readonly Mock<IApiService<Employee, ApiResponse>> _apiService;
        private readonly IValidator<Employee> _validator;
        private readonly UpdateEmployeeHandler _updateEmployeeHandler;

        public UpdateEmployeeHandlerTests()
        {
            _apiService = new Mock<IApiService<Employee, ApiResponse>>();
            _validator = new EmployeeValidator();
            _updateEmployeeHandler = new UpdateEmployeeHandler(_apiService.Object, _validator);
        }

        [Fact]
        public async Task Invalid_model_throws_validation_exception()
        {
            var entity = Mock.Of<Employee>(x =>
                x.Email == "can" &&
                x.Gender == "male"
            );

            var requestModel = Mock.Of<UpdateEmployeeCommand>(x => x.Employee == entity);

            _apiService.Setup(x => x.Post(entity))
                .Returns(Task.FromResult(It.IsAny<ApiResponse>()));

            Task result() => _updateEmployeeHandler.Handle(requestModel, new CancellationToken());

            await Assert.ThrowsAsync<ValidationException>(result);
        }
    }
}
