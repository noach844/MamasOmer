using System;

namespace MamasOmer.Classes
{
    class Employee
    {
        // ID of employee
        public int ID { get; }
        // Name of employee (fname, lname)
        public string Name { get; }
        // Hours employee worked
        private double hours;
        // Bonus in accordance to his ranks
        private double bonus;
        // Bonus in accordance to roll's risk
        private double riskBonus;
        // Roll of employee (from config options)
        private string roll;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <param name="name">name (fname lname) of employee</param>
        /// <param name="roll">roll of employee (from options in config)</param>
        public Employee(int id, string name, string roll)
        {
            this.ID = id;
            this.Name = name;
            this.roll = roll;
            this.hours = 0;
            // set bonus of specific employee
            BonusCalculate();
            // set risk bonus of specific employee
            RiskCalculate();
            Console.WriteLine($"Employee created! name:{Name}, ID:{ID}, Roll:{roll}, Bonus:{bonus}, Risk: {riskBonus}");
        }

        /// <summary>
        /// The function sum all bonuses of ranks and put it in bonus. called on init.
        /// </summary>
        private void BonusCalculate()
        {
            // init value
            double bonus = 100;
            // loop over all ranks of this employee's roll
            foreach (string rank in ConfigSerializer.GetRanks(this.roll))
            {
                // sum
                bonus += ConfigSerializer.GetBonus(rank);
            }
            // set bonus. example: 80% => 1.8 for easier calculate later.
            this.bonus = bonus / 100;
        }

        /// <summary>
        /// The function set the riskBonus to roll's riskBonus. called on init;
        /// </summary>
        private void RiskCalculate()
        {
            // init value
            double riskBonus = 100;
            // add risk bonus
            riskBonus += ConfigSerializer.GetRiskBonus(roll);
            // set riskBonus. example: 80% => 1.8 for easier calculate later.
            this.riskBonus = riskBonus / 100;
        }

        /// <summary>
        /// The function checks if there is a need to set hours according to roll and the hours he worked.
        /// </summary>
        private void setConstHours()
        {
            // Loop over ranks of employee's roll
            foreach (string rank in ConfigSerializer.GetRanks(roll))
            {
                // get consthours of rank
                int constHours = ConfigSerializer.GetConstHours(rank);
                // check if rank has consthours
                if (constHours != 0)
                {
                    // get minhours of rank
                    int minHours = ConfigSerializer.GetMinHours(rank);
                    // check if employee worked enough hours.
                    if (hours >= minHours)
                    {
                        // uses the maximum hours between worked hours and const hours of rank.
                        hours = Math.Max(hours, constHours);
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
            // fist, check if there is a need to set employee's hours
            setConstHours();
            //calculation
            return (hours * (ConfigSerializer.HourSalary * bonus)) * riskBonus;
        }
    }
}
