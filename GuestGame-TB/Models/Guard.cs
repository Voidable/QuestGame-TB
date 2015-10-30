using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Guard : Character
    {
                #region [ FIELDS ]

        //  Guard's current room
        private int _currentRoomNumber;

        //  Guard's greeting
        private string _greeting;

        #endregion // End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets or Sets the Guard's Current Room
        /// </summary>
        public int CurrentRoomNumber
        {
            get { return _currentRoomNumber; }
            set { _currentRoomNumber = value; }
        }

        /// <summary>
        /// Gets or Sets the Guard's Greeting
        /// </summary>
        public string Greeting
        {
            get { return _greeting; }
            set { _greeting = value; }
        }

        #endregion // End of [ PROPERTIES ] region


        #region [ METHODS ]

        /// <summary>
        /// Gets the String Describing the Guard, from the Player's perspective.
        /// </summary>
        /// <returns></returns>
        public override string FullDescription()
        {
            string output = "";
            string hisHer = "";

            if (base.Gender == Genders.MALE)
                hisHer = "His";
            else
                hisHer = "Her";

            output = string.Format("{2} {1}. {4} nametag reads {0}. {3}",
                base.Name,
                base.Gender.ToString().ToLower(),
                base.Race.ToString().ToLower(),
                base.Description,
                hisHer);

            return output;
        }

        #endregion // End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default Guard Constructor, creates Steve the Guard
        /// </summary>
        public Guard()
            : base("Steve", "A big threating guard", Genders.MALE, Races.HUMAN)
        {
            _greeting = "Get out of here!";
            _currentRoomNumber = 0;
        }

        /// <summary>
        /// Overloaded Guard Constructor
        /// </summary>
        /// <param name="name">Guard's Name</param>
        /// <param name="description">Guard's Description</param>
        /// <param name="gender">Guard's Gender</param>
        /// <param name="race">Guard's Race</param>
        public Guard(string name, string description, Genders gender, Races race)
            : base(name, description, gender, race)
        {
            _currentRoomNumber = 0;
            _greeting = "Move along!";
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
