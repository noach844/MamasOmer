using System;
using System.Collections.Generic;
using System.Text;

namespace MamasOmer.Classes.Exceptions
{
    public class InvalidConfigException : Exception
    {
        // Exception for Config Serializers - for more indicative code
        public InvalidConfigException(string message) : base(message)
        {            
        }
    }
}
