using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace MamasOmer.Classes.DB
{
    /// <summary>
    /// The class takes controll of db actions
    /// </summary>
    class DBHandler
    {
        /// <summary>
        /// The function loads all employees from the db and returns List of employees.
        /// </summary>
        /// <returns>
        /// List of employees
        /// </returns>
        public static List<Employee> LoadEmployees()
        {
            // Use 'using' for safe connection
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                // Query the db for all employees
                var result = conn.Query<Employee>("SELECT * FROM EMPLOYEES", new DynamicParameters());
                // Cast returned type to List (for easier use later)
                return result.ToList();
            }
        }
        
        /// <summary>
        /// The function gets an employee and updates it's Hours and startTime in DB.
        /// </summary>
        /// <param name="employee"></param>
        public static void UpdateEmployee(Employee employee)
        {
            // Use 'using' for safe connection
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {                
                // Updates employee's Hours and StartTime
                conn.Execute("UPDATE EMPLOYEES SET Hours=@Hours, StartTime=@StartTime, Month=@Month WHERE ID=@ID", employee);                
            }
        }
               
        /// <summary>
        /// The function returns the connection string from App.config        
        /// </summary>
        /// <param name="id">
        /// Id of connection string from App.config. Default value is 'Default'
        /// </param>
        /// <returns>Connection string</returns>
        private static string LoadConnectionString(string id="Default")
        {            
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
