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
    /// The class takes App.config and serializing it into the right data types.    
    /// </summary>
    public static class ConfigSerializer
    {
        // Dictionary of Rolls <string, List<string>> - string: name of roll, List: list of ranks(each roll can have multiple ranks).
        private static Dictionary<string, List<string>> Rolls;
        // Dictionary of Ranks <string, int> - string: name of ranks, int - precents bonus.
        private static Dictionary<string, int> Ranks;
        // Dictionary of Risks <string, int> - string: name of roll, int - precents bonus for risk.
        private static Dictionary<string, int> Risks;
        // Dictionary of constHours <string, int> - string: name of rank, int - hours to pay for constantly
        private static Dictionary<string, int> ConstHours;
        // Dictionary of minHours <string, int> - string: name of rank, int - minimum hours of work in need to get const hours.
        private static Dictionary<string, int> MinHours;

        /// <summary>
        ///  CTOR - init Ranks and Rolls.
        /// </summary>
        static ConfigSerializer()
        {
            // Init values
            Rolls = new Dictionary<string, List<string>>();
            Ranks = new Dictionary<string, int>();
            Risks = new Dictionary<string, int>();
            ConstHours = new Dictionary<string, int>();
            MinHours = new Dictionary<string, int>();

            // Call Serializers
            RanksSerializer();
            RollsSerializer();
            RisksSerializer();
        }   
        
        /// <summary>
        /// The function reads from App.config and serialize it into Rolls.
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
                    if(Ranks.(rank))
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
                catch(FormatException)
                {
                    throw new InvalidConfigException("Value of key in ranks section must be an Integer!");
                }
            }
        }

        /// <summary>
        /// The function reads from App.config and serialize it into Risks.     
        /// </summary>
        private static void RisksSerializer()
        {
            // Get Ranks section from App.config
            var section = ConfigurationManager.GetSection("Risks") as NameValueCollection;
            // Loop through the keys in 'Risks' section, Each key represents a roll.
            foreach (string roll in section.Keys)
            {
                try
                {
                    // Cast value of Risk to int. The value represents the bonus precents given for risk.
                    int precents = Int32.Parse(section.Get(roll));
                    // add risk into it's key in Risks.                
                    Risks.Add(roll, precents);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in Risks section must be an Integer!");
                }
            }
        }

        /// <summary>
        /// The function reads from App.config and serialize it into ConstHours.
        /// </summary>
        private static void constHoursSerializer()
        {
            // Get constHours section from App.config
            var section = ConfigurationManager.GetSection("ConstHours") as NameValueCollection;
            // Loop through the keys in 'ConstHours' section, Each key represents a rank.
            foreach (string rank in section.Keys)
            {
                try
                {
                    // Cast value of ConstHours to int. The value represents hours to pay for specific rank.
                    int hours = Int32.Parse(section.Get(rank));
                    // add hours into it's key in constHours.                
                    ConstHours.Add(rank, hours);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in ConstHours section must be an Integer!");
                }
            }
        }

        /// <summary>
        /// The function reads from App.config and serialize it into MinHours.        
        /// </summary>
        private static void minHoursSerializer()
        {
            // Get constHours section from App.config
            var section = ConfigurationManager.GetSection("MinHours") as NameValueCollection;
            // Loop through the keys in 'MinHours' section, Each key represents a rank.
            foreach (string rank in section.Keys)
            {
                try
                {
                    // Cast value of MinHours to int. The value represents minimum hours to get the constHours for rank.
                    int hours = Int32.Parse(section.Get(rank));
                    // add hours into it's key in MinHours.                
                    MinHours.Add(rank, hours);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in MinHours section must be an Integer!");
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
        public static int GetBonus(string rank)
        {
            return Ranks[rank];
        }

        /// <summary>
        /// The function returns the ranks of specific roll given.
        /// </summary>
        /// <param name="roll">
        /// string - key of roll.
        /// </param>
        /// <returns>
        /// List of rolls.
        /// </returns>
        public static List<string> GetRanks(string roll)
        {
            return Rolls[roll];
        }        

        /// <summary>
        /// The function returns the bonus precents given for risk of specific roll given.
        /// </summary>
        /// <param name="roll">string - key of roll</param>
        /// <returns>
        /// Bonus precents
        /// </returns>
        public static int GetRiskBonus(string roll)
        {
            // checks if roll needs to get bonus, else returns 0 
            return Risks.ContainsKey(roll) ? Risks[roll] : 0;
        }        

        /// <summary>
        /// The function returns the constant hours for specific rank
        /// </summary>
        /// <param name="rank">string - key of rank</param>
        /// <returns>number of rank hours needs to get payed by constantly</returns>
        public static int GetConstHours(string rank)
        {
            // checks if rank needs to get const hours, else returns 0 
            return ConstHours.ContainsKey(rank) ? ConstHours[rank] : 0;
        }

        /// <summary>
        /// The function returns the minimum hours a rank needs to work to get his const hours
        /// </summary>
        /// <param name="rank">string - key of rank</param>
        /// <returns>minimum hours to get rank's const hours</returns>
        public static int GetMinHours(string rank)
        {
            // checks if rank has minimum hours to get his const hours, else returns 0
            return MinHours.ContainsKey(rank) ? MinHours[rank] : 0;
        }
    }
}
