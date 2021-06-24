using System;
using System.Collections.Generic;
using System.Text;

namespace MamasOmer.Classes.Exceptions
{
    public class InvalidConfigException : Exception
    {
        public InvalidConfigException(string message) : base(message)
        {            
        }
    }
}
