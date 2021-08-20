using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Business.Commands
{
    public class UpdateEmployeeCommand : IRequest<ApiResponse>
    {
        public Employee Employee { get; set; }

        public UpdateEmployeeCommand()
        {

        }

        public UpdateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
