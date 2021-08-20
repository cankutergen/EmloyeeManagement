using EmloyeeManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Entities.Helper
{
    public class ApiResponse
    {
        public int Code { get; set; }

        public Meta Meta { get; set; }

        public List<DataResponse> Data { get; set; }
    }
}
