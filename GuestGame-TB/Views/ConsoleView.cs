﻿using System;
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
        private const int CONSOLE_HEADER_HEIGHT = 5;

        //  Console colors
        //      Header
        private const ConsoleColor HEADER_BACKGROUND_COLOR = ConsoleColor.Gray;
        private const ConsoleColor HEADER_TITLE_TEXT_COLOR = ConsoleColor.Blue;
        private const ConsoleColor HEADER_SUBTITLE_TEXT_COLOR = ConsoleColor.Red;
        //      Body
        private const ConsoleColor BODY_BACKGROUND_COLOR = ConsoleColor.Black;
        private const ConsoleColor BODY_TEXT_COLOR = ConsoleColor.White;

        //  Header text
        private const string HEADER_TITLE_TEXT = "Aztecan Corporation Prototype Heist";
        private static readonly int HEADER_TITLE_SPACE = (CONSOLE_WINDOW_WIDTH - HEADER_TITLE_TEXT.Count()) / 2;
        //  "static read-only" is because constant only works at compile time. using string.Count() only occurs when the program is running.

        //  Header positions
        private const int HEADER_TITLE_POSITION = 1;
        private const int HEADER_SUBTITLE_POSITION = 3;

        //  Object holders
        private Player _myPlayer;
        private Building _myBuilding;
        private StaffList _myStaff;
        private GuardList _myGuards;

        #endregion // End of [ FIELDS ] region


        #region [ METHODS ]

        #region [ INITIALIZER METHODS ]

        /// <summary>
        /// Creates the reference to the player for the ConsoleView
        /// </summary>
        /// <param name="player"></param>
        public void InitializePlayerReference(Player player)
        {
            _myPlayer = player;
        }

        /// <summary>
        /// Creates the reference to the building for the ConsoleView
        /// </summary>
        /// <param name="player"></param>
        public void InitializeBuildingReference(Building building)
        {
            _myBuilding = building;
        }

        /// <summary>
        /// Creates the reference to the staffList for the ConsoleView
        /// </summary>
        /// <param name="player"></param>
        public void InitializeStaffReference(StaffList staff)
        {
            _myStaff = staff;
        }

        /// <summary>
        /// Creates the reference to the guardList for the ConsoleView
        /// </summary>
        /// <param name="player"></param>
        public void InitializeGuardReference(GuardList guards)
        {
            _myGuards = guards;
        }

        #endregion // End of [ INITIALIZER METHODS ] region

        #region [ DISPLAY METHODS ]

        /// <summary>
        /// Method to draw the header on the top of the window
        /// </summary>
        public void DrawHeader()
        {
            //  Set the colors for the header
            Console.BackgroundColor = HEADER_BACKGROUND_COLOR;
            Console.ForegroundColor = HEADER_TITLE_TEXT_COLOR;

            //  For each line in the Header
            for (int i = 0; i < CONSOLE_HEADER_HEIGHT; i++)
            {
                //  The first line is just blank
                if (i == 0)
                {
                    for (int j = 0; j < CONSOLE_WINDOW_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }
                //  The second line contains the text
                else if (i == HEADER_TITLE_POSITION)
                {
                    //  For the blank space before the text. WindowWidth
                    for (int j = 0; j < HEADER_TITLE_SPACE; j++)
                    {
                        Console.Write(" ");
                    }
                    //  For the Text itself
                    for (int k = 0; k < HEADER_TITLE_TEXT.Count(); k++)
                    {
                        Console.Write(HEADER_TITLE_TEXT.ElementAt(k));
                    }
                    //  For the last of the blank space
                    for (int l = 0; l < CONSOLE_WINDOW_WIDTH - (HEADER_TITLE_SPACE + HEADER_TITLE_TEXT.Count()); l++)
                    {
                        Console.Write(" ");
                    }
                }
                //  The remaining lines are just blank
                else
                {
                    for (int j = 0; j < CONSOLE_WINDOW_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }
            }

            //  Reset the colors
            Console.BackgroundColor = BODY_BACKGROUND_COLOR;
            Console.ForegroundColor = BODY_TEXT_COLOR;
        }

        /// <summary>
        /// Draws the Header, with a subtitle bar
        /// </summary>
        /// <param name="subtitleMessage">Text to go underneath the title</param>
        public void DrawHeader(string subtitleMessage)
        {
            //  Set the colors for the header
            Console.BackgroundColor = HEADER_BACKGROUND_COLOR;
            Console.ForegroundColor = HEADER_TITLE_TEXT_COLOR;

            //  For each line in the Header
            for (int i = 0; i < CONSOLE_HEADER_HEIGHT; i++)
            {
                //  The first line is just blank
                if (i == 0)
                {
                    for (int j = 0; j < CONSOLE_WINDOW_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }
                //  The second line contains the TITLE
                else if (i == HEADER_TITLE_POSITION)
                {
                    //  For the blank space before the text.
                    for (int j = 0; j < HEADER_TITLE_SPACE; j++)
                    {
                        Console.Write(" ");
                    }
                    //  For the Text itself
                    for (int k = 0; k < HEADER_TITLE_TEXT.Count(); k++)
                    {
                        Console.Write(HEADER_TITLE_TEXT.ElementAt(k));
                    }
                    //  For the last of the blank space
                    for (int l = 0; l < CONSOLE_WINDOW_WIDTH - (HEADER_TITLE_SPACE + HEADER_TITLE_TEXT.Count()); l++)
                    {
                        Console.Write(" ");
                    }
                }
                //  Third line contains the SUBTITLE
                else if (i == HEADER_SUBTITLE_POSITION)
                {
                    //  Subtitle Color
                    Console.ForegroundColor = HEADER_SUBTITLE_TEXT_COLOR;

                    //  For the blank space before the text.
                    for (int j = 0; j < ((CONSOLE_WINDOW_WIDTH - subtitleMessage.Count()) / 2); j++)
                    {
                        Console.Write(" ");
                    }
                    //  For the Text itself
                    for (int k = 0; k < subtitleMessage.Count(); k++)
                    {
                        Console.Write(subtitleMessage.ElementAt(k));
                    }
                    //  For the last of the blank space
                    for (int l = 0; l < CONSOLE_WINDOW_WIDTH - (subtitleMessage.Count() + ((CONSOLE_WINDOW_WIDTH - subtitleMessage.Count()) / 2)); l++)
                    {
                        Console.Write(" ");
                    }

                    //  Reset color
                    Console.ForegroundColor = HEADER_TITLE_TEXT_COLOR;
                }
                //  The remaining lines are just blank
                else
                {
                    for (int j = 0; j < CONSOLE_WINDOW_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }
            }

            //  Reset the colors
            Console.BackgroundColor = BODY_BACKGROUND_COLOR;
            Console.ForegroundColor = BODY_TEXT_COLOR;
        }

        /// <summary>
        /// Clears the display, redraws the header
        /// </summary>
        public void DisplayClear()
        {
            Console.Clear();
            DrawHeader();
        }

        /// <summary>
        /// Clears the display, redraws the header with the subtitle
        /// </summary>
        /// <param name="subtitle"></param>
        public void DisplayClear(string subtitle)
        {
            Console.Clear();
            DrawHeader(subtitle);
        }

        /// <summary>
        /// Draws a blank line followed by a message
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        public void DisplayMessage(string message)
        {

            Console.WriteLine();
            Console.WriteLine(message);
        }

        /// <summary>
        /// Draws a blank line followed by a message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="blankLine">Write a blank line before the message</param>
        public void DisplayMessage(string message, bool blankLine)
        {
            if (blankLine)
                Console.WriteLine();
            Console.WriteLine(message);
        }

        /// <summary>
        /// Returns Input from the user
        /// </summary>
        /// <returns></returns>
        public string GetUserInput()
        {
            Console.WriteLine();

            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user with the message, then returns the input
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetUserInput(string message)
        {
            Console.WriteLine();

            Console.WriteLine(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the player to press a key to continue
        /// </summary>
        public void WaitForAnyKey()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Given a room number, display's the rooms contents
        /// </summary>
        /// <param name="roomNumber"></param>
        public void DisplayRoomContents(int roomNumber)
        {
            //  A list to hold the guards in the room
            List<Guard> roomGuards = new List<Guard>();

            //  A list to hold the staff in the room
            List<Staff> roomStaff = new List<Staff>();

            //  If a guard is in the room, add it to roomGuards
            foreach (Guard g in _myGuards.Guards)
            {
                if (g.CurrentRoomNumber == roomNumber)
                {
                    roomGuards.Add(g);
                }
            }

            //  If a staff member is in the rooom, add it to roomStaff
            foreach (Staff s in _myStaff.StaffMembers)
            {
                if (s.CurrentRoomNumber == roomNumber)
                {
                    roomStaff.Add(s);
                }
            }

            //  Get the room so we can get its items.
            Room targetRoom = _myBuilding.Rooms.Find(x => x.RoomNumber - 1 == roomNumber);

            // If there are guards, display the message.
            if (roomGuards.Count != 0)
            {
                //  Start of the string
                string guardLine = string.Format("There are {0} guards; ", roomGuards.Count);

                foreach (Guard g in roomGuards)
                {
                    guardLine = string.Format(guardLine + g.Name + " ");
                }

                //  Display the string
                DisplayMessage(guardLine, false);
            }

            //  If there are staff, display the message.
            if (roomStaff.Count != 0)
            {
                //  Start of the string
                string staffLine = string.Format("There are {0} staff members; ", roomStaff.Count);

                foreach (Staff s in roomStaff)
                {
                    staffLine = string.Format(staffLine + s.Name + " ");
                }

                //  Display the string
                DisplayMessage(staffLine, false);
            }

            //  If there are items, display them
            foreach (Item i in targetRoom.RoomInventory)
            {
                DisplayMessage(string.Format("There is a " + i.Name),false);
            }
        }

        /// <summary>
        /// Display's all of the players items
        /// </summary>
        public void DisplayPlayerInventory()
        {
            DisplayMessage("I have: ");
            foreach (Item i in _myPlayer.Inventory)
            {
                DisplayMessage(string.Format(i.Quantity + " " + i.Name + ", " + i.Description),false);
            }
        }

        #endregion // End of [ DISPLAY METHODS ] region

        #endregion // End of the [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Creates the console
        /// </summary>
        public ConsoleView()
        {
            //  Set dimensions of the window
            Console.WindowHeight = CONSOLE_WINDOW_HEIGHT;
            Console.WindowWidth = CONSOLE_WINDOW_WIDTH;

            //  Set the colors of the window
            Console.BackgroundColor = BODY_BACKGROUND_COLOR;
            Console.ForegroundColor = BODY_TEXT_COLOR;
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
