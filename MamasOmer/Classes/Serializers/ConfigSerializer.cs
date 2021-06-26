using MamasOmer.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MamasOmer.Classes
{
    /// <summary>
    /// The class takes App.config and serializing it into the right data types.    
    /// This part takes charge of general configs and CTOR
    /// </summary>
    public static partial class ConfigSerializer
    {                               
        // Hour Salary. Money per hour
        public static double HourSalary;

        /// <summary>
        ///  CTOR - init Ranks and Roles.
        /// </summary>
        static ConfigSerializer()
        {
            // Init values
            Roles = new Dictionary<string, List<string>>();
            Ranks = new Dictionary<string, int>();
            Risks = new Dictionary<string, int>();
            ConstHours = new Dictionary<string, int>();
            MinHours = new Dictionary<string, int>();
            // Call Serializers
            GeneralSerializer();
            RanksSerializer();
            RolesSerializer();
            RisksSerializer();
            constHoursSerializer();
            minHoursSerializer();
        }

        /// <summary>
        /// The function reads from App.config and serialize it's general configs.
        /// </summary>
        private static void GeneralSerializer()
        {
            var section = ConfigurationManager.GetSection("General") as NameValueCollection;
            HourSalary = Double.Parse(section.Get("HourSalary"));
        }           
    }
}
