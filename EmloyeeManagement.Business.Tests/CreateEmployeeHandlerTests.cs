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
    public class CreateEmployeeHandlerTests
    {
        private readonly Mock<IApiService<Employee, ApiResponse>> _apiService;
        private readonly IValidator<Employee> _validator;
        private readonly CreateEmployeeHandler _createEmployeeHandler;

        public CreateEmployeeHandlerTests()
        {
            _apiService = new Mock<IApiService<Employee, ApiResponse>>();
            _validator = new EmployeeValidator();
            _createEmployeeHandler = new CreateEmployeeHandler(_apiService.Object, _validator);
        }

        [Fact]
        public async Task Invalid_model_throws_validation_exception()
        {
            var entity = Mock.Of<Employee>(x =>
                x.Email == "can@can.com" &&
                x.Gender == "male"
            );

            var requestModel = Mock.Of<CreateEmployeeCommand>(x => x.Employee == entity);

            _apiService.Setup(x => x.Post(entity))
                .Returns(Task.FromResult(It.IsAny<ApiResponse>()));

            Task result() => _createEmployeeHandler.Handle(requestModel, new CancellationToken());

            await Assert.ThrowsAsync<ValidationException>(result);
        }
    }
}
