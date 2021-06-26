using System;
using System.Collections.Generic;
using System.Linq;

namespace Mamas
{
    /// <summary>
    /// The class represents a menu and takes control of user input and executing the wanted options actions. 
    /// </summary>
    partial class Menu
    {
        private List<Option> _optionsList;

        /// <summary>
        ///     CTOR
        /// </summary>
        public Menu()
        {
            _optionsList = new List<Option>();
        }

        /// <summary>
        ///  This function creates an option (Option struct) and adds it as an option to the list.
        /// </summary>
        /// <param name="text">
        ///  The text that describes what the action does (the text shown in menu).   
        /// </param>
        /// <param name="action">The action to run when user chooses this option.</param>
        public void AddOption(string text, Action action)
        {
            // Add option to menu
            _optionsList.Add(new Option(text, action));
        }

        /// <summary>
        ///  This function prints the menu created.
        ///  <remarks>
        ///   The function iterates through all the options and prints it as a nice output for the user.
        /// </remarks>       
        /// </summary>
        private void PrintMenu()
        {
            // Some fun with colors 😁
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*****************");
            Console.WriteLine("Menu:");
            // Iterating over the options list and printing each option with its number.
            foreach (var (opt, index) in _optionsList.Select((value, i) => (value, i)))
            {
                Console.WriteLine($" {index+1}. {opt.Text}");
            }
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("*****************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// This function is gets choice from user and validates it.
        /// <remarks>
        /// The validation includes type (checks if input is number) and value (checks if number is in options range).
        /// </remarks>
        /// </summary>
        /// <returns>
        /// Users choice
        /// </returns>
        private int validateInput()
        {
            int choice;
            Console.Write("Please choose an option: ");
            // Loop until passing the validation checks.
            while (true)
            {
                try
                {
                    // Get user's input.
                    choice = Int32.Parse(Console.ReadLine());
                    // Check if choice is in the options list and if positive.
                    if (choice > _optionsList.Count || choice < 0)
                    {
                        throw new OverflowException();
                    }
                    // Got to here means all good
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Choice must be a number: ");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Enter a number from the list please: ");
                }
            }
            // Returns choice after passed validation checks.
            return choice;
        }

        /// <summary>
        /// This function runs the menu.
        /// <remarks>
        /// The function is the "main" of Menu.Its in charge of outputing the menu and running it until the user quits it, then printing a goodbye message.
        /// </remarks>
        /// </summary>
        public void Run()
        {
            int choice;
            // Loop until user quits the menu.
            while (true)
            {
                // Output the menu.
                PrintMenu();
                // Get user's choice.
                choice = validateInput();
                Console.Clear();
                // Check if user choosed 0 (Exit). If true, print a nice goodbye message and break the loop.
                if (choice == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Thank you! GoodBye");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                // Run chosen option's action. 
                _optionsList[choice - 1].Run();
            }
        }
    }

}
