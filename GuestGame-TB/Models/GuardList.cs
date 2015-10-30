using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class GuardList
    {
        #region [ FIELDS ]

        private List<Guard> _guards;

        #endregion // End of [ FIELDS ] region

        #region [ PROPERTIES ]

        public List<Guard> Guards 
        {
            get { return _guards; }
            set { _guards = value; }
        }

        #endregion // End of [ PROPERTIES ] region

        #region [ METHODS ]

        public void InitializeGuards()
        {
            Guard guard;

            //  Andy
            guard = new Guard(
                "Andy",
                "He wears a nice suit.",
                Character.Genders.MALE,
                Character.Races.HUMAN);
            guard.CurrentRoomNumber = 0; // North of building
            guard.Greeting = "Hello there!";
            _guards.Add(guard);

            //  Gordon
            guard = new Guard(
                "Gordon",
                "The standard guard uniform doesn't quite fit him.",
                Character.Genders.MALE,
                Character.Races.ELF);
            guard.CurrentRoomNumber = 2; // South of building
            guard.Greeting = "This is the employee entrance, you'll need an ID to enter.";
            _guards.Add(guard);
        }

        #endregion // End of [ METHODS ] region

        #region [ CONSTRUCTORS ]

        public GuardList()
        {
            _guards = new List<Guard>();
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
