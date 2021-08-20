using EmloyeeManagement.Entities.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Queries
{
    public class GetEmployeeByIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
