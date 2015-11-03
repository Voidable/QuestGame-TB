using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Player : Character
    {
        #region [ FIELDS ]

        private List<Item> _inventory;

        private int _inventorySize;

        private int _currentRoomNumber;

        #endregion // End of [ FIELDS ] region


        #region [ PROPERTIES ]

        public List<Item> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public int InventorySize
        {
            get { return _inventorySize; }
            set { _inventorySize = value; }
        }

        public int CurrentRoomNumber
        {
            get { return _currentRoomNumber; }
            set { _currentRoomNumber = value; }
        }

        #endregion // End of the [ PROPERTIES ] region


        #region [ METHODS ]

        public int CurrentTotalInventory()
        {
            int total = 0;

            foreach (Item i in _inventory)
            {
                total += i.Size;
            }

            return total;
        }

        public override string FullDescription()
        {
            string output = "";

            output = string.Format("I am {0}. I am a {1} {2}. My goal is to steal the prototype from the secret lab at the top of this building. {3}", base.Name, base.Gender, base.Race, base.Description);

            return output;
        }

        #endregion // End of the [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default constructor, creates Bob the player
        /// </summary>
        public Player()
            : base("Bob", "I am the Player that wasn't customized", Genders.MALE, Races.HUMAN)
        {
            _inventory = new List<Item>();
            _inventorySize = 12;
            _currentRoomNumber = 0;
        }

        /// <summary>
        /// Overloaded constructor, creates player based on inputs
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="description">Player's description</param>
        /// <param name="gender">Player's gender</param>
        /// <param name="race">Player's race</param>
        public Player(string name, string description, Genders gender, Races race)
            : base(name, description, gender, race)
        {
            _inventory = new List<Item>();
            _inventorySize = 12;
            _currentRoomNumber = 0;
        }

        #endregion // End of the [ CONSTRUCTORS ] region
    }
}
