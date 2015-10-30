using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Staff : Character
    {
        #region [ FIELDS ]

        //  Staff's current room
        private int _currentRoomNumber;

        //  Staff's greeting
        private string _greeting;

        #endregion // End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets or Sets the Staff's Current Room
        /// </summary>
        public int CurrentRoomNumber
        {
            get { return _currentRoomNumber; }
            set { _currentRoomNumber = value; }
        }

        /// <summary>
        /// Gets or Sets the Staff's Greeting
        /// </summary>
        public string Greeting
        {
            get { return _greeting; }
            set { _greeting = value; }
        }

        #endregion // End of [ PROPERTIES ] region


        #region [ METHODS ]

        /// <summary>
        /// Gets the String Describing the Staff, from the Player's perspective.
        /// </summary>
        /// <returns></returns>
        public override string FullDescription()
        {
            string output = "";

            output = string.Format("{0} is a {1} {2}. {3}",
                base.Name,
                base.Gender.ToString().ToLower(),
                base.Race.ToString().ToLower(),
                base.Description);

            return output;
        }

        #endregion // End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default Staff Constructor, creates Bob the Builder
        /// </summary>
        public Staff()
            : base("Bob", "He looks like a Builder.", Genders.MALE, Races.HUMAN)
        {
            _greeting = "Hi, I build things.";
            _currentRoomNumber = 0;
        }

        /// <summary>
        /// Overloaded Staff Constructor
        /// </summary>
        /// <param name="name">Staff's Name</param>
        /// <param name="description">Staff's Description</param>
        /// <param name="gender">Staff's Gender</param>
        /// <param name="race">Staff's Race</param>
        public Staff(string name, string description, Genders gender, Races race)
            : base(name, description, gender, race)
        {
            _currentRoomNumber = 0;
            _greeting = "Greetings!";
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
