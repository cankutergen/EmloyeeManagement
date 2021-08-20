﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.Entities.Concrete
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Status { get; set; }
    }
}