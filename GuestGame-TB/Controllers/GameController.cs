using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class GameController
    {
        #region [ FIELDS ]

        Player _myPlayer;
        Building _myBuilding;
        ConsoleView _myView;
        StaffList _myStaff;
        GuardList _myGuards;

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
                _myView.InitializePlayerReference(_myPlayer);

                //  Create building
                _myBuilding = new Building();

                //  Pass building to view
                _myView.InitializeBuildingReference(_myBuilding);

                //  Create StaffList
                _myStaff = new StaffList();
                _myStaff.InitializeStaff();

                //  Pass StaffList to view
                _myView.InitializeStaffReference(_myStaff);

                //  Create GuardList
                _myGuards = new GuardList();
                _myGuards.InitializeGuards();

                //  Pass GuardList to view
                _myView.InitializeGuardReference(_myGuards);

                //  Play the game
                PlayGame();
            }

        }

        /// <summary>
        /// Initializes the Console
        /// </summary>
        public void InitializeView()
        {
            _myView = new ConsoleView();
        }

        /// <summary>
        /// Runs the Main menu of the game, returns bool for players choice.
        /// </summary>
        /// <returns>True for continue, False for quit</returns>
        public bool MainMenu()
        {
            bool output = false;
            bool validInput = false;
            _myView.DisplayClear();

            _myView.DisplayMessage("Welcome to the game; Aztecan Corporation Protoype Heist.", true);
            _myView.DisplayMessage("Type \"Continue\" to play, or \"Quit\" to exit",false);

            string input; 

            while (!validInput)
            {
                input = _myView.GetUserInput();

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
                    _myView.DisplayMessage("That was not a valid command. The valid commands are \"Continue\" and \"Quit\"",true);
                }
            }


            return output;
        }

        /// <summary>
        /// Gets the values from the user, the creates the player.
        /// </summary>
        public void CreatePlayer()
        {
            _myView.DisplayClear(); //  Blank screen

            //  Get name
            _myView.DisplayMessage("What is your name?");
            string name = _myView.GetUserInput();

            //  Echo inputs
            _myView.DisplayMessage(string.Format("Your name is {0}",name));
            _myView.WaitForAnyKey();

            _myView.DisplayClear(); //  Blank screen

            //  Get gender
            _myView.DisplayMessage("Are you male or female?");
            bool validGender = false;
            Character.Genders gender = Character.Genders.MALE;
            //  Loop until a valid input
            while (!validGender)
            {
                if (Enum.TryParse<Character.Genders>(_myView.GetUserInput(),true,out gender))
                {
                    validGender = true;
                }
                else
                {
                    _myView.DisplayMessage("That was not a valid gender, try again.");
                }
            }

            //  Echo inputs
            _myView.DisplayMessage(string.Format("Your name is {0}", name));
            _myView.DisplayMessage(string.Format("You are {0}", gender.ToString()),false);
            _myView.WaitForAnyKey();

            _myView.DisplayClear(); //  Blank screen

            _myView.DisplayMessage("What is your race?");
            foreach (Character.Races r in Enum.GetValues(typeof(Character.Races)))
            {
                _myView.DisplayMessage(r.ToString(), false);
            }
            bool validRace = false;
            Character.Races race = Character.Races.HUMAN;
            while (!validRace)
            {
                if (Enum.TryParse<Character.Races>(_myView.GetUserInput(),true,out race))
                {
                    validRace = true;
                }
                else
                {
                    _myView.DisplayMessage("That was not a valid race, try again.");
                }
            }

            //  Echo inputs
            _myView.DisplayMessage(string.Format("Your name is {0}", name));
            _myView.DisplayMessage(string.Format("You are {0}", gender.ToString()),false);

            //  Make a into an for grammar reasons.
            string aSuffix = "";
            if (race.ToString() == "ELF")
            {
                aSuffix = "n";
            }

            _myView.DisplayMessage(string.Format("You are a{0} {1}",aSuffix, race.ToString()),false);
            _myView.WaitForAnyKey();

            //  Create the player
            _myPlayer = new Player(name, "You are the Player", gender, race);

        }

        /// <summary>
        /// Runs the game's logic loop
        /// </summary>
        public void PlayGame()
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
