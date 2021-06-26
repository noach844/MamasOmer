using ConsoleTables;
using MamasOmer.Classes.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MamasOmer.Classes
{
    class EmployeesManager
    {
        // list of employees
        private List<Employee> Employees;

        /// <summary>
        /// CTOR        
        /// </summary>
        public EmployeesManager()
        {
            // load employees using DBHandler to comunicate with db.
            Employees = DBHandler.LoadEmployees();
        }

        /// <summary>
        /// The function gets employee's id as input from user and outputs it's wage
        /// </summary>
        public void GetEmployeeWage()
        {
            // Input handler
            int id;
            Console.Write("Enter Employee's ID: ");
            // Validate number input
            while (true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a number!");
                }
            }
            // Get wanted employee by id using Linq
            var result = Employees.Where(employee => employee.ID == id).ToList();
            // Check if there are results
            if (result.Count > 0)
            {
                Employee employee = result[0];
                // Get month name as string
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();                
                string month = mfi.GetMonthName(employee.Month);
                // Output employee's wage
                Console.WriteLine($"The wage of {employee.Name} is {employee.WageCalculator()} right to {month}");
            }
            else
            {
                Console.WriteLine($"No customer with ID:{id} found!");
            }
        }

        /// <summary>
        ///  The function gets employee's id as input from user and insert card for specific employee.
        ///  Works for start of shift and end of shift.
        /// </summary>
        public void InsertCard()
        {
            // handle input
            int id;
            Console.Write("Enter Employee's ID: ");
            // validate number input
            while(true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a number!");
                }
            }            
            // Get employee by id using Linq
            var result = Employees.Where(employee => employee.ID == id).ToList();
            if (result.Count > 0)
            {
                Employee employee = result[0];
                // insert card
                employee.Card();
            }
            else
            {
                // No found output
                Console.WriteLine($"No customer with ID:{id} found!");
            }
        }

        /// <summary>
        /// The function lets the user search employee by his letters or part of name (checks contains)
        /// </summary>
        public void SearchEmployees()
        {
            // Input handle
            Console.Write("Enter name or part of it (keep empty to show all): ");
            string searchText = Console.ReadLine().ToLower();
            // Get employees using Linq
            var result = Employees.Where(employee => employee.Name.ToLower().Contains(searchText)).ToList();
            // Print table of results
            printEmployeeTable(result);            
        }

        /// <summary>
        /// The function gets list of employees and prints it as table using ConsoleTable
        /// </summary>
        /// <param name="employees">
        /// List of employees
        /// </param>
        public void printEmployeeTable(List<Employee> employees)
        {
            // Set columns
            var table = new ConsoleTable("ID", "Name", "Roll");            
            // Loop over employees and adds to table
            foreach(Employee employee in employees)
            {
                table.AddRow(employee.ID, employee.Name, employee.Roll);
            }
            // Output table
            table.Write();
        }
    }
}
