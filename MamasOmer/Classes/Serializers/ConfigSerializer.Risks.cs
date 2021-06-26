using MamasOmer.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MamasOmer.Classes
{
    /// <summary>
    /// This part takes charge of Risks section from config
    /// </summary>
    public static partial class ConfigSerializer
    {
        // Dictionary of Risks <string, int> - string: name of role, int - precents bonus for risk.
        private static Dictionary<string, int> Risks;

        /// <summary>
        /// The function reads from App.config and serialize it into Risks.     
        /// </summary>
        private static void RisksSerializer()
        {
            // Get Ranks section from App.config
            var section = ConfigurationManager.GetSection("Risks") as NameValueCollection;
            // Loop through the keys in 'Risks' section, Each key represents a role.
            foreach (string role in section.Keys)
            {
                try
                {
                    // Cast value of Risk to int. The value represents the bonus precents given for risk.
                    int precents = Int32.Parse(section.Get(role));                   
                    if (Roles.ContainsKey(role))
                    {
                        // add risk into it's key in Risks.                                    
                        Risks.Add(role, precents);
                    }
                    else
                    {
                        throw new InvalidConfigException("All keys in Risks must be keys in Roles!");
                    }
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in Risks section must be an Integer!");
                }
            }
        }

        /// <summary>
        /// The function returns the bonus precents given for risk of specific role given.
        /// </summary>
        /// <param name="role">string - key of role</param>
        /// <returns>
        /// Bonus precents
        /// </returns>
        public static int GetRiskBonus(string role)
        {
            // checks if role needs to get bonus, else returns 0 
            return Risks.ContainsKey(role) ? Risks[role] : 0;
        }
    }
}
