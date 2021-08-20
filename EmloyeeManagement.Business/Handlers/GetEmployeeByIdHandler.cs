using EmloyeeManagement.Business.Queries;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Handlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, ApiResponse>
    {
        private readonly IApiService<Employee, ApiResponse> _apiService;

        public GetEmployeeByIdHandler(IApiService<Employee, ApiResponse> apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _apiService.Get($"users?id={request.Id}");
        }
    }
}
