using System;
using System.Configuration;
using MamasOmer.Classes;
using MamasOmer.Classes.DB;
using MamasOmer.Classes.Exceptions;
using MamasOmer.Classes.Menus;

namespace MamasOmer
{
    class Program
    {        
        static void Main(string[] args)
        {            
            // Init main menu
            MainMenu menu = new MainMenu();
            // Run menu
            menu.Run();
        }
    }
}
