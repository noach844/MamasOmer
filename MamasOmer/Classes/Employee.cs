using MamasOmer.Classes.DB;
using System;
using System.ComponentModel.DataAnnotations;

namespace MamasOmer.Classes
{
    class Employee
    {
        // ID of employee
        public int ID { get;}
        // Name of employee (fname, lname)
        public string Name { get;}
        // Hours employee worked
        public double Hours { get; set; }
        // Bonus in accordance to his ranks
        private double bonus;
        // Bonus in accordance to Role's risk
        private double riskBonus;
        // Role of employee (from config options)
        public string Role;
        // StartTime of cuurent shift
        public string StartTime { get; set; }
        // Represents last Month worked in
        public int Month { get; set; }
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <param name="name">name (fname lname) of employee</param>
        /// <param name="Role">Role of employee (from options in config)</param>
        public Employee(Int64 ID, string Name, string Role, Decimal Hours, string StartTime, Int64 Month)
        {
            this.ID = Convert.ToInt32(ID);
            this.Name = Name;
            this.Role = Role;
            this.Hours = Decimal.ToDouble(Hours);
            this.StartTime = StartTime;
            this.Month = Convert.ToInt32(Month);
            // set bonus of specific employee
            BonusCalculate();
            // set risk bonus of specific employee
            RiskCalculate();            
        }

        /// <summary>
        /// The function sum all bonuses of ranks and put it in bonus. called on init.
        /// </summary>
        private void BonusCalculate()
        {
            // init value
            double bonus = 100;
            // loop over all ranks of this employee's Role
            foreach (string rank in ConfigSerializer.GetRoleRanks(this.Role))
            {
                // sum
                bonus += ConfigSerializer.GetRankBonus(rank);
            }
            // set bonus. example: 80% => 1.8 for easier calculate later.
            this.bonus = bonus / 100;
        }

        /// <summary>
        /// The function set the riskBonus to Role's riskBonus. called on init;
        /// </summary>
        private void RiskCalculate()
        {
            // init value
            double riskBonus = 100;
            // add risk bonus
            riskBonus += ConfigSerializer.GetRiskBonus(Role);
            // set riskBonus. example: 80% => 1.8 for easier calculate later.
            this.riskBonus = riskBonus / 100;
        }

        /// <summary>
        /// The function checks if there is a need to set Hours according to Role and the Hours he worked.
        /// </summary>
        private void setConstHours()
        {
            // Loop over ranks of employee's Role
            foreach (string rank in ConfigSerializer.GetRoleRanks(Role))
            {
                // get constHours of rank
                int constHours = ConfigSerializer.GetConstHours(rank);
                // check if rank has constHours
                if (constHours != 0)
                {
                    // get minHours of rank
                    int minHours = ConfigSerializer.GetMinHours(rank);
                    // check if employee worked enough Hours.
                    if (Hours >= minHours)
                    {
                        // uses the maximum Hours between worked Hours and const Hours of rank.
                        Hours = Math.Max(Hours, constHours);
                        DBHandler.UpdateEmployee(this);
                    }
                }
            }
        }

        /// <summary>
        /// The function calculate the wage of employee.
        /// </summary>
        /// <returns>The wage of the employee</returns>
        public double WageCalculator()
        {
            // fist, check if there is a need to set employee's Hours
            setConstHours();
            //calculation
            double wage = ((Hours * (ConfigSerializer.HourSalary * bonus)) * riskBonus);
            return Math.Round(wage, 2);
        }

        /// <summary>
        /// The function is in charge of managing shift entrace and exit. (calculate hours of work and updates).
        /// </summary>
        public void Card()
        {
            // Check if start of shift
            if(StartTime == null)
            {
                // Get current time
                var now = DateTime.Now;
                // Get current month
                var month = now.Month;

                // Check if first shift of month
                if(month != Month)
                {                    
                    // Set Month to current month 
                    Month = month;
                    // Reset Hours. (monthly wage)
                    Hours = 0;
                }
                // Convert date to string for DB.
                StartTime = now.ToString();
                // Update on DB
                DBHandler.UpdateEmployee(this);
                Console.WriteLine($"Welcome {Name}! Have a great shift!");
            }
            // End of shift
            else
            {             
                // Get Current time
                DateTime endTime = DateTime.Now;
                // Convert string to DateTime for calculations
                DateTime startTime = Convert.ToDateTime(StartTime);
                // EndTime - startTime (in hours)
                double hoursWorked = double.Parse((endTime - startTime).TotalHours.ToString());
                // Round number to 2 numbers after '.'
                hoursWorked = Math.Round(hoursWorked, 2);
                // Add ended shift hours to total hours.
                Hours += hoursWorked;
                // Reset StartTime (until next shift)
                StartTime = null;
                // Updating the db. Hours and StartTime.
                DBHandler.UpdateEmployee(this);
                Console.WriteLine($"Goodbye {Name}! Hope you had a nice shift :)");
            }
        }        
    }
}
