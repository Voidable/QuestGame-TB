﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class GameController
    {
        //  Enum of valid Commands
        public enum GameCommands
        {
            GO,
            LOOK,
            HELP
        }


        #region [ FIELDS ]

        Player _player;
        Building _building;
        ConsoleView _view;
        StaffList _staff;
        GuardList _guards;

        #endregion // End of [ FIELDS ] region

        #region [ METHODS ]

        /// <summary>
        /// Sets up the game so it can be played.
        /// </summary>
        public void SetupGame()
        {
            //Initialize the View
            InitializeView();

            //  If main menu returns true for continue, go on with the game.
            if (MainMenu())
            {
                //  Create player
                CreatePlayer();

                //  Pass player to view
                _view.InitializePlayerReference(_player);

                //  Create building
                _building = new Building();

                //  Pass building to view
                _view.InitializeBuildingReference(_building);

                //  Create StaffList
                _staff = new StaffList();
                _staff.InitializeStaff();

                //  Pass StaffList to view
                _view.InitializeStaffReference(_staff);

                //  Create GuardList
                _guards = new GuardList();
                _guards.InitializeGuards();

                //  Pass GuardList to view
                _view.InitializeGuardReference(_guards);

                //  Play the game
                PlayGame();
            }

            //  Application Closes at this point

        }

        /// <summary>
        /// Initializes the Console
        /// </summary>
        public void InitializeView()
        {
            _view = new ConsoleView();
        }

        /// <summary>
        /// Runs the Main menu of the game, returns bool for players choice.
        /// </summary>
        /// <returns>True for continue, False for quit</returns>
        public bool MainMenu()
        {
            bool output = false;
            bool validInput = false;
            _view.DisplayClear();

            _view.DisplayMessage("Welcome to the game; Aztecan Corporation Protoype Heist.", true);
            _view.DisplayMessage("Type \"Continue\" to play, or \"Quit\" to exit",false);

            string input; 

            while (!validInput)
            {
                input = _view.GetUserInput();

                if (input.ToUpper() == "CONTINUE")
                {
                    output = true;
                    validInput = true;
                }
                else if (input.ToUpper() == "QUIT")
                {
                    output = false;
                    validInput = true;
                } 
                else
                {
                    _view.DisplayMessage("That was not a valid command. The valid commands are \"Continue\" and \"Quit\"",true);
                }
            }


            return output;
        }

        /// <summary>
        /// Gets the values from the user, the creates the player.
        /// </summary>
        public void CreatePlayer()
        {
            _view.DisplayClear(); //  Blank screen

            //  Get name
            _view.DisplayMessage("What is your name?");
            string name = _view.GetUserInput();

            //  Echo inputs
            _view.DisplayMessage(string.Format("Your name is {0}",name));
            _view.WaitForAnyKey();

            _view.DisplayClear(); //  Blank screen

            //  Get gender
            _view.DisplayMessage("Are you male or female?");
            bool validGender = false;
            Character.Genders gender = Character.Genders.MALE;
            //  Loop until a valid input
            while (!validGender)
            {
                if (Enum.TryParse<Character.Genders>(_view.GetUserInput(),true,out gender))
                {
                    validGender = true;
                }
                else
                {
                    _view.DisplayMessage("That was not a valid gender, try again.");
                }
            }

            //  Echo inputs
            _view.DisplayMessage(string.Format("Your name is {0}", name));
            _view.DisplayMessage(string.Format("You are {0}", gender.ToString()),false);
            _view.WaitForAnyKey();

            _view.DisplayClear(); //  Blank screen

            _view.DisplayMessage("What is your race?");
            foreach (Character.Races r in Enum.GetValues(typeof(Character.Races)))
            {
                _view.DisplayMessage(r.ToString(), false);
            }
            bool validRace = false;
            Character.Races race = Character.Races.HUMAN;
            while (!validRace)
            {
                if (Enum.TryParse<Character.Races>(_view.GetUserInput(),true,out race))
                {
                    validRace = true;
                }
                else
                {
                    _view.DisplayMessage("That was not a valid race, try again.");
                }
            }

            //  Echo inputs
            _view.DisplayMessage(string.Format("Your name is {0}", name));
            _view.DisplayMessage(string.Format("You are {0}", gender.ToString()),false);

            //  Make a into an for grammar reasons.
            string aSuffix = "";
            if (race.ToString() == "ELF")
            {
                aSuffix = "n";
            }

            _view.DisplayMessage(string.Format("You are a{0} {1}",aSuffix, race.ToString()),false);
            _view.WaitForAnyKey();

            //  Create the player
            _player = new Player(name, "You are the Player", gender, race);

        }

        /// <summary>
        /// Runs the game's logic loop
        /// </summary>
        public void PlayGame()
        {
            _view.DisplayClear();
            bool playingGame = true;

            //  Tell the user of their goal
            _view.DisplayMessage("You are a thief.");
            _view.DisplayMessage("Your goal is to steal a prototype device from the lab hidden in this building.");
            _view.DisplayMessage("Due to budget-cuts, you have no gear to start with.");
            _view.DisplayMessage("The \"Help\" command will tell you the basics, should you need them.");
            _view.WaitForAnyKey();

            while (playingGame)
            {
                //  This draws the header with the name of the room in the header.
                _view.DisplayClear(
                    //  Find the room in the list, where the room's number matches the players current room.
                    (_building.Rooms.Find(x => x.RoomNumber - 1 == _player.CurrentRoomNumber)
                    //                  x.RoomNumber is 1-base. Player.CurrentRoom# is 0-base.
                    //  Then get the Name string.
                    .Name));

                //  Get the current rooms description.
                string roomDesc = _building.Rooms.Find(x => x.RoomNumber - 1 == _player.CurrentRoomNumber).Description;

                //  Display the current rooms description
                _view.DisplayMessage(roomDesc);

                //  Display the room's contents
                _view.DisplayRoomContents(_player.CurrentRoomNumber);

                //  Get the player's input
                string input = _view.GetUserInput();
            }
        }

        public void EvaluatePlayerCommand(string commandInput)
        {

        }

        #endregion // End of [ METHODS ] region


        #region [ CONSTRUCTOR ]

        /// <summary>
        /// Creates the GameController
        /// </summary>
        public GameController()
        {
            SetupGame();
        }

        #endregion // End of [ CONSTRUCTOR ] region
    }
}
