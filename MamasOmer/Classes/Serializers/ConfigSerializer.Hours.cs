using MamasOmer.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MamasOmer.Classes
{
    /// <summary>
    /// This part takes charge of MinHours and ConstHours sections from config
    /// </summary>
    public static partial class ConfigSerializer
    {
        // Dictionary of constHours <string, int> - string: name of rank, int - hours to pay for constantly
        private static Dictionary<string, int> ConstHours;
        // Dictionary of minHours <string, int> - string: name of rank, int - minimum hours of work in need to get const hours.
        private static Dictionary<string, int> MinHours;

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
                    if (Ranks.ContainsKey(rank))
                    {
                        // add hours into it's key in constHours.                
                        ConstHours.Add(rank, hours);
                    }
                    else
                    {
                        throw new InvalidConfigException("All keys in ConstHours must be Ranks!");
                    }
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
                    if (Ranks.ContainsKey(rank))
                    {
                        // add hours into it's key in MinHours.                
                        MinHours.Add(rank, hours);
                    }
                    else
                    {
                        throw new InvalidConfigException("All keys in MinHours must be Ranks!");
                    }
                }
                catch (FormatException)
                {
                    throw new InvalidConfigException("Value of key in MinHours section must be an Integer!");
                }
            }
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
