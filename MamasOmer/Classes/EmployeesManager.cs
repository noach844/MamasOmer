using ConsoleTables;
using MamasOmer.Classes.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamasOmer.Classes
{
    class EmployeesManager
    {
        // list of employees
        private List<Employee> Employees;

        public EmployeesManager()
        {
            // load employees usin DBHandler to comunicate with db.
            Employees = DBHandler.LoadEmployees();
        }

        public void GetEmployeeWage(int id)
        {
            var result = Employees.Where(employee => employee.ID == id).ToList();
            if (result.Count > 0)
            {
                Employee employee = result[0];
                Console.WriteLine($"The wage of {employee.Name} is {employee.WageCalculator()}");
            }
            else
            {
                Console.WriteLine($"No customer with ID:{id} found!");
            }
        }

        public void InsertCard(int id)
        {
            var result = Employees.Where(employee => employee.ID == id).ToList();
            if (result.Count > 0)
            {
                Employee employee = result[0];
                employee.Card();
            }
            else
            {
                Console.WriteLine($"No customer with ID:{id} found!");
            }
        }

        public void SearchEmployees(string name)
        {
            var result = Employees.Where(employee => employee.Name.Contains(name)).ToList();
            printEmployeeTable(result);            
        }

        public void printEmployeeTable(List<Employee> employees)
        {
            var table = new ConsoleTable("ID", "Name", "Roll");            
            foreach(Employee employee in employees)
            {
                table.AddRow(employee.ID, employee.Name, employee.Roll);
            }
            table.Write();
        }
    }
}
