using System;

namespace MamasOmer.Classes.Menus
{
    partial class MenuCreator
    {
        /// <summary>
        /// Struct that represents a menu option.
        /// <remarks>
        /// Built from text and action. 
        /// </remarks>
        /// </summary>
        private struct Option
        {           
            public string Text { get; }
            private Action _action;

            /// <summary>
            /// CTOR
            /// </summary>
            /// <param name="text">The text that describes what the action does (the text shown in menu).</param>
            /// <param name="action">The action to run when user chooses this option.</param>
            public Option(string text, Action action)
            {
                Text = text;
                _action = action;
            }            

            /// <summary>
            /// The function runs _action.            
            /// </summary>
            public void Run()
            {
                _action();
            }
        }
    }

}
