using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class ConsoleView
    {
        #region [ FIELDS ]

        //  Console dimensions
        private const int CONSOLE_WINDOW_WIDTH = 100;
        private const int CONSOLE_WINDOW_HEIGHT = 40;
        private const int CONSOLE_HEADER_HEIGHT = 3;

        //  Console colors
        //      Header
        private const ConsoleColor HEADER_BACKGROUND_COLOR = ConsoleColor.DarkGray;
        private const ConsoleColor HEADER_TEXT_COLOR = ConsoleColor.Blue;
        //      Body
        private const ConsoleColor BODY_BACKGROUND_COLOR = ConsoleColor.Black;
        private const ConsoleColor BODY_TEXT_COLOR = ConsoleColor.White;

        //  Header text
        private const string HEADER_TITLE_TEXT = "Aztecan Corporation Prototype Heist";

        //  Object holders
        private Player _myPlayer;
        private Building _myBuilding;

        #endregion // End of [ FIELDS ] region

        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Creates the console
        /// </summary>
        public ConsoleView()
        {
            Console.WindowHeight = CONSOLE_WINDOW_HEIGHT;
            Console.WindowWidth = CONSOLE_WINDOW_WIDTH;
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
