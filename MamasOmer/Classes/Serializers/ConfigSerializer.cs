using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Linq;
using MamasOmer.Classes.Exceptions;

namespace MamasOmer.Classes
{
    /// <summary>
    /// The class takes App.config and serializing it into the right datatypes.    
    /// </summary>
    public static class ConfigSerializer
    {
        // Dictionary of Rolls <string, List<string>> - string: name of roll, List: list of ranks(each roll can have multiple ranks).
        public static Dictionary<string, List<string>> Rolls;
        // Dictionary of Ranks <string, int> - string: name of ranks, int - precents bonus.
        public static Dictionary<string, int> Ranks;

        /// <summary>
        ///  CTOR - init Ranks and Rolls.
        /// </summary>
        static ConfigSerializer()
        {
            // Init values
            Rolls = new Dictionary<string, List<string>>();
            Ranks = new Dictionary<string, int>();

            // Call Serializers
            RanksSerializer();
            RollsSerializer();            
        }   
        
        /// <summary>
        /// The function read from App.config and serialize it into Rolls.
        /// </summary>
        private static void RollsSerializer()
        {
            // Get Rolls section from App.config
            var section = ConfigurationManager.GetSection("Rolls") as NameValueCollection;
            // Loop through the keys in 'Rolls' section, each key represents a roll
            foreach (string roll in section.Keys)
            {
                // Create a key in Rolls from key in App.config 
                Rolls.Add(roll, new List<string>());

                // Get value of key. the value represents ranks, string of ranks seperate by ';'
                string ranks = section.Get(roll);

                // Go through ranks after split
                foreach (string rank in ranks.Split(';'))
                {
                    if(Ranks.Keys.Contains(rank))
                    {
                        //add rank to it's specific key in Rolls
                        Rolls[roll].Add(rank);
                    }
                    else
                    {
                        throw new InvalidConfigException("All ranks in Roll value should be exist in ranks section!");
                    }
                }
            }
        }

        /// <summary>
        ///  The function read from App.config and serialize it into Ranks.     
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
                catch(FormatException)
                {
                    throw new InvalidConfigException("Value of key in ranks section must be an Integer!");
                }
            }
        }
    }
}
