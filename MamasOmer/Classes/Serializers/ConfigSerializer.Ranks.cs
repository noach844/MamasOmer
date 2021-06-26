using MamasOmer.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MamasOmer.Classes
{
    /// <summary>
    /// This part takes charge of Ranks section from config
    /// </summary>
    public static partial class ConfigSerializer
    {
        // Dictionary of Ranks <string, int> - string: name of ranks, int - precents bonus.
        private static Dictionary<string, int> Ranks;

        /// <summary>
        ///  The function reads from App.config and serialize it into Ranks.     
        /// </summary>
        private static void RanksSerializer()
        {
            // Get Ranks section from App.config
            var section = ConfigurationManager.GetSection("Ranks") as NameValueCollection;
            // Loop through the keys in 'Ranks' section, Each key represents a rank.
            foreach (string rank in section.Keys)
            {
                try
                {
                    // Cast value of rank to int. The value represents the bonus precents.
                    int precents = Int32.Parse(section.Get(rank));
                    // add rank into it's key in Ranks.                
                    Ranks.Add(rank, precents);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in ranks section must be an Integer!");
                }
            }
        }

        /// <summary>
        /// The function returns the bonus precents of specific rank given.
        /// </summary>
        /// <param name="rank">
        /// string - key of rank.
        /// </param>
        /// <returns>
        /// int - bonus precents
        /// </returns>
        public static int GetRankBonus(string rank)
        {
            return Ranks[rank];
        }
    }
}
