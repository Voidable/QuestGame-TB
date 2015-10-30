using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class StaffList
    {
        #region [ FIELDS ]

        private List<Staff> _staffMembers;

        #endregion // End of [ FIELDS ] region

        #region [ PROPERTIES ]

        public List<Staff> StaffMembers
        {
            get { return _staffMembers; }
            set { _staffMembers = value; }
        }

        #endregion // End of [ PROPERTIES ] region

        #region [ METHODS ]

        public void InitializeStaff()
        {
            Staff staff;

            //  Joe
            staff = new Staff(
                "Joe",
                "A bright eyed guy.",
                Character.Genders.MALE,
                Character.Races.HUMAN);
            staff.CurrentRoomNumber = 0; // North of building
            staff.Greeting = "Good day to you!";
            _staffMembers.Add(staff);

            //  Elise
            staff = new Staff(
                "Elise",
                "She's taking a smoke break.",
                Character.Genders.FEMALE,
                Character.Races.ELF);
            staff.CurrentRoomNumber = 2; // South of building
            staff.Greeting = "Don't bother me, I'm on break";
            _staffMembers.Add(staff);
        }

        #endregion // End of [ METHODS ] region

        #region [ CONSTRUCTORS ]

        public StaffList()
        {
            _staffMembers = new List<Staff>();
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
