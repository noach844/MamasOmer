using System;
using System.Configuration;
using MamasOmer.Classes;
using MamasOmer.Classes.Exceptions;

namespace MamasOmer
{
    class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                var ranks = ConfigSerializer.Ranks;
                var rolls = ConfigSerializer.Rolls;
                Console.WriteLine("yay!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
