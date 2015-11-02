using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class GameController
    {
        #region [ ENUMS ]
        public enum GameCommands
        {
            HELP,
            GO
        }

        public enum GameDirections
        {
            NORTH,
            NORTHEAST,
            EAST,
            SOUTHEAST,
            SOUTH,
            SOUTHWEST,
            WEST,
            NORTHWEST,
            UP,
            DOWN
        }
        #endregion // End of [ ENUMS ] region

        public delegate void CommandDelegate(string input);

        public Dictionary<GameCommands, CommandDelegate> commandDictionary = new Dictionary<GameCommands, CommandDelegate>
            {
                {GameCommands.GO, MovePlayer },
                {GameCommands.HELP, HelpQuery },
            };
            
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
            //  Boolean that determines if we are currently playing the game.
            bool playingGame = true;

            //  Core Game Loop
            while (playingGame)
            {
                //  Clear the display
                _view.DisplayClear();

                //  Display room information

                //  Get input from player
                string playerInput = _view.GetUserInput();

                //  Parse input to command
                //  Check the first word of the input string for a command verb
                string firstWord = playerInput.Split(' ')[0];   //  I'll admit, I didn't think this would work.

                //  Create a delegate - Help command by default
                CommandDelegate commandChoice = HelpQuery;

                //  Check each command for a match against the first word
                bool foundMatch = false;
                foreach (GameCommands e in Enum.GetValues(typeof(GameCommands)))
                {
                    if (firstWord.ToUpper() == e.ToString().ToUpper())
                    {
                        //  Assign delegate based on Enum value
                        commandChoice = commandDictionary[e];

                        //  Found a match
                        foundMatch = true;
                    }
                }

                //  If we found a match
                if (foundMatch)
                {
                    //  perform command
                    commandChoice(playerInput);
                }
                //  No match, tell the player their input could not be understood
                else
                {
                    _view.DisplayMessage(string.Format("I didn't understand the word {0}", firstWord));
                    _view.WaitForAnyKey();
                }
            }
        }


        #region [ COMMAND METHODS ]

        public static void MovePlayer(string playerInput)
        {

        }

        public static void HelpQuery(string playerInput)
        {

        }

        #endregion // End of [ COMMAND METHODS ] region


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
