using System;
using System.Collections.Generic;
using System.Text;

namespace MamasOmer.Classes
{
    class Employee
    {
        // ID of employee
        public int ID { get; }
        // Name of employee (fname, lname)
        public string Name { get; }
        // Hours employee worked
        private double hours { get; }
        // Bonus in accordance to his ranks
        private double bonus;
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
            // set base bonus of specific employee
            BaseCalculate();
            Console.WriteLine($"Employee created! name:{Name}, ID:{ID}, Roll:{roll}, Bonus:{bonus}");
        }

        /// <summary>
        /// The function sum all bonuses of ranks and put it in bonus. called on init.
        /// </summary>
        private void BaseCalculate()
        {
            // init value
            double bonus = 100;
            // loop over all ranks of this employee's roll
            foreach(string rank in ConfigSerializer.GetRanks(this.roll))
            {
                // sum
                bonus += ConfigSerializer.GetBonus(rank);
            }
            // set bonus. example: 80% => 1.8 for easier calculate later.
            this.bonus = bonus / 100;
        }        


    }
}
