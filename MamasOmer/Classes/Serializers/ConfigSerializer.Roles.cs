using MamasOmer.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MamasOmer.Classes
{
    /// <summary>
    /// This part takes charge of Roles section from config
    /// </summary>
    public static partial class ConfigSerializer
    {
        // Dictionary of Roles <string, List<string>> - string: name of role, List: list of ranks(each Role can have multiple ranks).        
        private static Dictionary<string, List<string>> Roles;

        /// <summary>
        /// The function reads from App.config and serialize it into Roles.
        /// </summary>
        private static void RolesSerializer()
        {
            // Get Roles section from App.config
            var section = ConfigurationManager.GetSection("Roles") as NameValueCollection;
            // Loop through the keys in 'Roles' section, each key represents a role
            foreach (string role in section.Keys)
            {
                // Create a key in Roles from key in App.config 
                Roles.Add(role, new List<string>());

                // Get value of key. the value represents ranks, string of ranks seperate by ';'
                string ranks = section.Get(role);

                // Go through ranks after split
                foreach (string rank in ranks.Split(';'))
                {
                    if (Ranks.ContainsKey(rank))
                    {
                        //add rank to it's specific key in Roles
                        Roles[role].Add(rank);
                    }
                    else
                    {
                        throw new InvalidConfigException("All ranks in Roles value should be exist in ranks section!");
                    }
                }
            }
        }

        /// <summary>
        /// The function returns the ranks of specific role given.
        /// </summary>
        /// <param name="role">
        /// string - key of role.
        /// </param>
        /// <returns>
        /// List of roles.
        /// </returns>
        public static List<string> GetRoleRanks(string role)
        {
            return Roles[role];
        }
    }
}
