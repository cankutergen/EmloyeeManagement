using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Queries
{
    public class GetEmployeeListWithQuery : IRequest<ApiResponse>
    {
        public string Query { get; set; }

        public GetEmployeeListWithQuery(string query)
        {
            Query = query;
        }
    }
}
