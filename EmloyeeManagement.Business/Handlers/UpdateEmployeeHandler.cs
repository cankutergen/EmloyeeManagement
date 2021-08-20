using EmloyeeManagement.Business.Commands;
using EmloyeeManagement.Core.CrossCuttingConcerns.FluentValidation;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using FluentValidation;
using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse>
    {
        private readonly IApiService<Employee, ApiResponse> _apiService;
        private readonly IValidator<Employee> _validator;

        public UpdateEmployeeHandler(IApiService<Employee, ApiResponse> apiService, IValidator<Employee> validator)
        {
            _apiService = apiService;
            _validator = validator;
        }

        public async Task<ApiResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // throws exception if validation fails
            ValidatorTool.FluentValidate(_validator, request.Employee);

            return await _apiService.Modify($"/{request.Employee.Id}", Method.PUT, request.Employee);
        }
    }
}
