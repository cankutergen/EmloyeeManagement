using EmloyeeManagement.Business.Commands;
using EmloyeeManagement.Core.CrossCuttingConcerns.FluentValidation;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse>
    {
        private readonly IApiService<Employee, ApiResponse> _apiService;
        private readonly IValidator<Employee> _validator;

        public CreateEmployeeHandler(IApiService<Employee, ApiResponse> apiService, IValidator<Employee> validator)
        {
            _apiService = apiService;
            _validator = validator;
        }

        public async Task<ApiResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // throws exception if validation fails
            ValidatorTool.FluentValidate(_validator, request.Employee);

            return await _apiService.Post(request.Employee);
        }
    }
}
