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
            GO,
            QUIT,
            LOOK
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
                {GameCommands.LOOK, LookAt },
                {GameCommands.QUIT, ConfirmExit}
            };

        #region [ FIELDS ]

        private static Player _player;
        private static Building _building;
        private static ConsoleView _view;
        private static StaffList _staff;
        private static GuardList _guards;

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
            _view.DisplayMessage("Type \"Continue\" to play, or \"Quit\" to exit", false);

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
                    _view.DisplayMessage("That was not a valid command. The valid commands are \"Continue\" and \"Quit\"", true);
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
            _view.DisplayMessage(string.Format("Your name is {0}", name));
            _view.WaitForAnyKey();

            _view.DisplayClear(); //  Blank screen

            //  Get gender
            _view.DisplayMessage("Are you male or female?");
            bool validGender = false;
            Character.Genders gender = Character.Genders.MALE;
            //  Loop until a valid input
            while (!validGender)
            {
                if (Enum.TryParse<Character.Genders>(_view.GetUserInput(), true, out gender))
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
            _view.DisplayMessage(string.Format("You are {0}", gender.ToString()), false);
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
                if (Enum.TryParse<Character.Races>(_view.GetUserInput(), true, out race))
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
            _view.DisplayMessage(string.Format("You are {0}", gender.ToString()), false);

            //  Make a into an for grammar reasons.
            string aSuffix = "";
            if (race.ToString() == "ELF")
            {
                aSuffix = "n";
            }

            _view.DisplayMessage(string.Format("You are a{0} {1}", aSuffix, race.ToString()), false);
            _view.WaitForAnyKey();

            //  Create the player
            _player = new Player(name, "You are the Player", gender, race);

        }

        /// <summary>
        /// Runs the game's logic loop
        /// </summary>
        public void PlayGame()
        {
            //  Clear the game screen
            _view.DisplayClear();

            //  Boolean that determines if we are currently playing the game.
            bool playingGame = true;

            //  Tell the user of their goal
            _view.DisplayMessage("You are a thief.");
            _view.DisplayMessage("Your goal is to steal a prototype device from the lab hidden in this building.");
            _view.DisplayMessage("Due to budget-cuts, you have no gear to start with.");
            _view.DisplayMessage("The \"Help\" command will tell you the basics, should you need them.");
            _view.WaitForAnyKey();

            //  Core Game Loop
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

        /// <summary>
        /// Command for player movement
        /// </summary>
        /// <param name="playerInput"></param>
        public static void MovePlayer(string playerInput)
        {
            //  Break apart the string, and check it for a direction
            string[] words = playerInput.Split(' ');
            GameDirections direction = GameDirections.NORTH;
            bool validInput = false;

            //  For each word in the input string. (Logically it should only be two, but users input whatever
            foreach (string word in words)
            {
                //  For each possible direction
                foreach (GameDirections dWord in Enum.GetValues(typeof(GameDirections)))
                {
                    //  If a word matches a direction, assign it to direction, set the bool, and get out of the loop.
                    if (word.ToUpper() == dWord.ToString().ToUpper())
                    {
                        direction = dWord;
                        validInput = true;
                        break;
                    }
                }

                //  Stop checking words once a valid direction has been found.
                if (validInput)
                {
                    break;
                }
            }

            //  We werent able to find a valid direction
            if (validInput == false)
            {
                _view.DisplayMessage("There isn't a valid direction in your input.");
                return;
            }
            //  Find the room with a 1-base room number that matches the 0-base player room.
            Room tempRoom = _building.Rooms.Find(x => x.RoomNumber - 1 == _player.CurrentRoomNumber);

            //  There is a passage in that direction.
            //  Note: the Passsages array index's match the GameDirections enum index's.
            if (tempRoom.Passages[(int)direction] != null)
            {
                //  Make sure the entrance is the room we're currently in.
                //  This should always return true
                if (tempRoom.Passages[(int)direction].Entrance.RoomNumber == tempRoom.RoomNumber)
                {
                    //  0-base player roomNumber equals the 1-base roomNumber of the exit.
                    _player.CurrentRoomNumber = tempRoom.Passages[(int)direction].Exit.RoomNumber - 1;
                }
                else
                {
                    //Building was incorrectly setup
                    _view.DisplayMessage(string.Format("Something went wrong, this Passage is in the wrong place! {0}",tempRoom.RoomNumber));
                }
            }
            else
            {
                //  There is no passage in that direction, inform the player of this.
                _view.DisplayMessage(string.Format("I can't go {0}",direction.ToString().ToLower()));
            }
        }

        /// <summary>
        /// Command to display commands
        /// </summary>
        /// <param name="playerInput"></param>
        public static void HelpQuery(string playerInput)
        {

        }

        /// <summary>
        /// Command to get item information
        /// </summary>
        /// <param name="playerInput"></param>
        public static void LookAt(string playerInput)
        {

        }

        /// <summary>
        /// Command to exit game
        /// </summary>
        /// <param name="playerInput"></param>
        public static void ConfirmExit(string playerInput)
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
