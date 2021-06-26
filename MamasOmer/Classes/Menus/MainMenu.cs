using System;
using System.Collections.Generic;
using System.Text;

namespace MamasOmer.Classes.Menus
{
    class MainMenu
    {
        // Base menu
        private MenuCreator _menu;        
        private EmployeesManager _manager;

        /// <summary>
        /// CTOR
        /// </summary>
        public MainMenu()
        {
            // Init
            _manager = new EmployeesManager();
            _menu = new MenuCreator();
            // Add options to menu
            _menu.AddOption("Search Employee", _manager.SearchEmployees);
            _menu.AddOption("Insert Card", _manager.InsertCard);
            _menu.AddOption("Display Employee's Wage", _manager.GetEmployeeWage);
        }

        /// <summary>
        /// The function runs the menu created on CTOR
        /// </summary>
        public void Run()
        {
            // Run menu
            _menu.Run();
        }
    }
}
