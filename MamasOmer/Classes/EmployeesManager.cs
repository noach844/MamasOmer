using MamasOmer.Classes.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace MamasOmer.Classes
{
    class EmployeesManager
    {
        // list of employees
        private List<Employee> employees;

        EmployeesManager()
        {
            // load employees usin DBHandler to comunicate with db.
            employees = DBHandler.LoadEmployees();
        }


    }
}
