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
            GO,
            MOVE,
            LOOK,
            GRAB,
            TAKE,
            DROP,
            LEAVE,
            INVENTORY,
            ITEMS,
            HELP,
            QUIT
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
                {GameCommands.MOVE, MovePlayer },
                {GameCommands.HELP, HelpQuery },
                {GameCommands.LOOK, LookAt },
                {GameCommands.QUIT, ConfirmExit},
                {GameCommands.GRAB, GrabItem},
                {GameCommands.DROP, DropItem},
                {GameCommands.TAKE, GrabItem},
                {GameCommands.LEAVE, DropItem},
                {GameCommands.INVENTORY, ItemList},
                {GameCommands.ITEMS, ItemList}
            };

        #region [ FIELDS ]

        private static Player _player;
        private static Building _building;
        private static ConsoleView _view;
        private static StaffList _staff;
        private static GuardList _guards;

        //  Boolean that determines if we are currently playing the game.
        private static bool _playingGame = true;

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
            _view.DisplayMessage("Are you MALE or FEMALE?");
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

            //  Tell the user of their goal
            _view.DisplayMessage("You are a thief.");
            _view.DisplayMessage("Your goal is to steal a prototype device from the lab hidden in this building.");
            _view.DisplayMessage("Due to budget-cuts, you have no gear to start with.");
            _view.DisplayMessage("The \"Help\" command will tell you the basics, should you need them.");
            _view.WaitForAnyKey();

            //  Core Game Loop
            while (_playingGame)
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
                _view.WaitForAnyKey();
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
                    //  If the passage is locked
                    if (tempRoom.Passages[(int)direction].IsLocked)
                    {
                        //  Player has unlock item
                        //  If the player's inventory | Contains an Item | with the same itemtype as the passage's unlock item.
                        if (_player.Inventory.Contains(_player.Inventory.Find(x => x.Type == tempRoom.Passages[(int)direction].UnlockItem)))
                        {
                            //  0-base player roomNumber equals the 1-base roomNumber of the exit.
                            _player.CurrentRoomNumber = tempRoom.Passages[(int)direction].Exit.RoomNumber - 1;
                            _view.DisplayMessage(tempRoom.Passages[(int)direction].MoveThrough);
                            _view.WaitForAnyKey();
                        }
                        //  Player does not have unlock item
                        else
                        {
                            _view.DisplayMessage(tempRoom.Passages[(int)direction].LockedResponse);
                            _view.WaitForAnyKey();
                        }
                    }
                    //  Passage is unlocked
                    else
                    {
                        //  0-base player roomNumber equals the 1-base roomNumber of the exit.
                        _player.CurrentRoomNumber = tempRoom.Passages[(int)direction].Exit.RoomNumber - 1;
                        _view.DisplayMessage(tempRoom.Passages[(int)direction].MoveThrough);
                        _view.WaitForAnyKey();
                    }

                }
                else
                {
                    //Building was incorrectly setup
                    _view.DisplayMessage(string.Format("Something went wrong, this Passage is in the wrong place! {0}", tempRoom.RoomNumber));
                    _view.WaitForAnyKey();
                }
            }
            else
            {
                //  There is no passage in that direction, inform the player of this.
                _view.DisplayMessage(string.Format("I can't go {0}", direction.ToString().ToLower()));
                _view.WaitForAnyKey();
            }
        }

        /// <summary>
        /// Command to display commands
        /// </summary>
        /// <param name="playerInput"></param>
        public static void HelpQuery(string playerInput)
        {
            _view.DisplayMessage("The recognized command verbs are:");
            _view.DisplayMessage("", false);
            foreach (GameCommands c in Enum.GetValues(typeof(GameCommands)))
            {
                _view.DisplayMessage(c.ToString(), false);
            }
            _view.DisplayMessage("The recognized directions are:");
            _view.DisplayMessage("", false);
            foreach (GameDirections d in Enum.GetValues(typeof(GameDirections)))
            {
                _view.DisplayMessage(d.ToString(), false);
            }

            _view.WaitForAnyKey();
        }

        /// <summary>
        /// Command to get item information
        /// </summary>
        /// <param name="playerInput"></param>
        public static void LookAt(string playerInput)
        {
            //  Split the input into words
            string[] words = playerInput.Split(' ');
            string target = "";
            bool foundTarget = false;

            // A single word, or two words with the second being "AT" means insufficient input
            if ((words.Count() <= 1) || (words.Count() == 2 & words[1].ToUpper() == "AT"))
            {
                _view.DisplayMessage("You need to tell me what to look at.");
                _view.WaitForAnyKey();
                return;
            }

            //  Two words where the second isn't "AT" assume second word is target
            else if (words.Count() == 2 & words[1].ToUpper() != "AT")
            {
                target = words[1].ToUpper();
            }

            //  Three words where the second is "AT", assume third word is target
            else if (words.Count() == 3 & words[1].ToUpper() == "AT")
            {
                target = words[2].ToUpper();
            }

            //  Couldn't interpret the input based on prior rules
            else
            {
                _view.DisplayMessage("I couldn't understand that. I understand \"Look NOUN\" and \"Look at NOUN\".");
                _view.WaitForAnyKey();
                return;
            }

            //  Check if the target is the player
            string[] playerReference = new string[] { "ME", "MYSELF" };
            if (playerReference.Contains(target))
            {
                _view.DisplayMessage(_player.FullDescription());
                foundTarget = true;
            }

            //  Check the guards for target match
            foreach (Guard g in _guards.Guards)
            {
                if (g.CurrentRoomNumber == _player.CurrentRoomNumber)
                {
                    if (g.Name.ToUpper() == target)
                    {
                        _view.DisplayMessage(g.FullDescription());
                        foundTarget = true;
                    }
                }
            }

            //  Check the staff for target match
            foreach (Staff s in _staff.StaffMembers)
            {
                if (s.CurrentRoomNumber == _player.CurrentRoomNumber)
                {
                    if (s.Name.ToUpper() == target)
                    {
                        _view.DisplayMessage(s.FullDescription());
                        foundTarget = true;
                    }
                }
            }

            //  Check the items for target match
            foreach
                //  Get the Item inventory of the current room
                (Item i in _building.Rooms.Find
                //  x.RoomNumber is 1-base. _player.CurrentRoomNumber is 0-base
                (x => x.RoomNumber - 1 == _player.CurrentRoomNumber).RoomInventory)
            {
                if (i.Name.ToUpper() == target)
                {
                    _view.DisplayMessage(i.Description);
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                _view.DisplayMessage(string.Format("I cant see anything called {0}", target.ToLower()));
            }

            //  Wait for the player before refreshing the view.
            _view.WaitForAnyKey();
        }

        /// <summary>
        /// Command to exit game
        /// </summary>
        /// <param name="playerInput"></param>
        public static void ConfirmExit(string playerInput)
        {
            //  Prompt player for confirmation
            string input = _view.GetUserInput("Are you sure you want to Quit? \"Yes\" \"No\"");

            if (input.ToUpper() == "YES")
            {
                _playingGame = false;
                _view.WaitForAnyKey();
            }
        }

        /// <summary>
        /// Command to Grab an item
        /// </summary>
        /// <param name="playerInput"></param>
        public static void GrabItem(string playerInput)
        {
            //  Split the input into words
            string[] words = playerInput.Split(' ');
            string target = "";
            bool foundTarget = false;

            // A single word means insufficient input
            if ((words.Count() <= 1))
            {
                _view.DisplayMessage("You need to tell me what to grab.");
                _view.WaitForAnyKey();
                return;
            }

            //  Two words assume second word is target
            else if (words.Count() >= 2)
            {
                target = words[1].ToUpper();
            }

            //  Couldn't interpret the input based on prior rules
            else
            {
                _view.DisplayMessage("I couldn't understand that. I understand \"Grab NOUN\"");
                _view.WaitForAnyKey();
                return;
            }

            //  Check the items for target match
            Room tempRoom = _building.Rooms.Find
                (x => x.RoomNumber - 1 == _player.CurrentRoomNumber);
            //  x.RoomNumber is 1-base. _player.CurrentRoomNumber is 0-base

            //  Get the Item inventory of the current room
            foreach (Item i in tempRoom.RoomInventory)
            {
                //  Found the target item - using Contains incase of multi word names
                if (i.Name.ToUpper().Contains(target))
                {
                    foundTarget = true;

                    //  Items too large to be picked up
                    if (i.Size > _player.InventorySize)
                    {
                        _view.DisplayMessage("I can't pick that up, its too big.");
                        _view.WaitForAnyKey();
                        break;
                    }

                    //  Stackable items
                    if (i.IsStackable)
                    {
                        //  Player has already has one of said item
                        if (_player.Inventory.Contains(i))
                        {
                            //  Add one to the stack
                            _player.Inventory.Find(x => x.Type == i.Type).Quantity++;
                            tempRoom.RoomInventory.Remove(i);
                            break;
                        }

                        //  Player does not have one of said item
                        else
                        {
                            //  Player has room in their inventory
                            if (_player.CurrentTotalInventory() + i.Size <= _player.InventorySize)
                            {
                                _player.Inventory.Add(i);
                                tempRoom.RoomInventory.Remove(i);
                                break;
                            }

                            //  Player has insufficient room in their inventory
                            else
                            {
                                _view.DisplayMessage("I don't have room in my bag for that.");
                                _view.WaitForAnyKey();
                                break;
                            }
                        }
                    }

                    //  Non-stackable
                    else
                    {
                        //  Player has room in their inventory
                        if (_player.CurrentTotalInventory() + i.Size <= _player.InventorySize)
                        {
                            _player.Inventory.Add(i);
                            tempRoom.RoomInventory.Remove(i);
                            break;
                        }

                        //  Player has insufficient room in their inventory
                        else
                        {
                            _view.DisplayMessage("I don't have room in my bag for that.");
                            _view.WaitForAnyKey();
                            break;
                        }
                    }
                }
            }

            //  There is no item with that name
            if (!foundTarget)
            {
                _view.DisplayMessage(string.Format("I can't see a {0} here", target));
            }
        }

        /// <summary>
        /// Command to Drop an item
        /// </summary>
        /// <param name="playerInput"></param>
        public static void DropItem(string playerInput)
        {
            //  Split the input into words
            string[] words = playerInput.Split(' ');
            string target = "";
            bool foundTarget = false;

            // A single word means insufficient input
            if ((words.Count() <= 1))
            {
                _view.DisplayMessage("You need to tell me what to drop.");
                _view.WaitForAnyKey();
                return;
            }

            //  Two words assume second word is target
            else if (words.Count() >= 2)
            {
                target = words[1].ToUpper();
            }

            //  Couldn't interpret the input based on prior rules
            else
            {
                _view.DisplayMessage("I couldn't understand that. I understand \"Drop NOUN\"");
                _view.WaitForAnyKey();
                return;
            }

            //  Create reference to current room so we could drop the item
            Room tempRoom = _building.Rooms.Find
                (x => x.RoomNumber - 1 == _player.CurrentRoomNumber);
            //  x.RoomNumber is 1-base. _player.CurrentRoomNumber is 0-base

            //  Get the Item inventory of the current room
            foreach (Item i in _player.Inventory)
            {
                //  Using Contains in case of multi word names
                if (i.Name.ToUpper().Contains(target))
                {
                    foundTarget = true;

                    //  Room has an infinite inventory size
                    if (tempRoom.RoomInventorySize == -1)
                    {
                        tempRoom.RoomInventory.Add(i);

                        //  The item was stackable
                        if (i.IsStackable)
                        {
                            //  Player had more than one
                            if (_player.Inventory.Find(x => x.Type == i.Type).Quantity < 1)
                            {
                                _player.Inventory.Find(x => x.Type == i.Type).Quantity--;
                                break;
                            }

                            //  Player only had one
                            else
                            {
                                _player.Inventory.Remove(i);
                                break;
                            }
                        }
                        //  The item is not stackable
                        else
                        {
                            _player.Inventory.Remove(i);
                            break;
                        }
                    }
                    //  Room doesn't allow items to be dropped.
                    else
                    {
                        _view.DisplayMessage("I cant drop that here, there's nowhere to put it.");
                        _view.WaitForAnyKey();
                        break;
                    }
                }
            }

            //  There is no item with that name
            if (!foundTarget)
            {
                _view.DisplayMessage(string.Format("I don't have a {0}", target));
                _view.WaitForAnyKey();
            }
        }

        /// <summary>
        /// Command to show the player's inventory.
        /// </summary>
        /// <param name="playerInput"></param>
        public static void ItemList(string playerInput)
        {
            _view.DisplayPlayerInventory();
            _view.WaitForAnyKey();
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
