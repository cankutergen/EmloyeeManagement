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
    public class CreateEmployeeCommand : IRequest<ApiResponse>
    {
        public Employee Employee { get; set; }

        public CreateEmployeeCommand()
        {

        }

        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
