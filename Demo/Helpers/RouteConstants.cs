﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Helpers
{
    public static class RouteConstants
    {
        public const string GetEmployee = "GetEmployee";
        public const string GetEmployeeById_id = "GetEmployeeById/{id}";
        public const string Employee_Create = "Employee/Create";
        public const string Employee_Update = "Employee/Update";
        public const string Employee_Delete_id = "Employee/Delete/{id}";
    }
}
