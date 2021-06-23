using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MamasOmer.Classes
{
    class RollsInitializer
    {
        public Dictionary<string, List<string>> Rolls {get;}

        public RollsInitializer()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroupCollection groups = config.SectionGroups;
            foreach (ConfigurationSectionGroup sectionGroup in groups)
            {
                if(sectionGroup.Name == "Employee")
                {
                    foreach(ConfigurationSection configSection in sectionGroup.Sections)
                    {
                        var section = ConfigurationManager.GetSection(configSection.SectionInformation.SectionName) as NameValueConfigurationCollection;
                        var zutar = section["zutar"].ToString();
                        Console.WriteLine(zutar);
                    }
                }
            }
        }
    }
}
