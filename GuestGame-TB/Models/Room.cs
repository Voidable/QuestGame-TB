using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Room
    {
        #region [ FIELDS ]

        //  Count of all rooms
        private static int _roomCount = 0;

        //  Room's unique ID
        private int _roomNumber;

        //  Name of room
        private string _name;

        //  Description of room
        private string _description;

        //  List of items in the room
        private List<Item> _roomInventory;

        //  Size limit for items in the inventory, defaults to -1 for unlimited.
        private int _roomInventorySize = -1;

        //  Array of passages of the room
        private Passage[] _passages;

        #endregion  // End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets or Sets the Room Count for all rooms
        /// </summary>
        public int RoomCount 
        {
            get { return _roomCount; }
            set { _roomCount = value; }
        }

        /// <summary>
        /// Gets or Sets the Room Number for the room
        /// </summary>
        public int RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }

        /// <summary>
        /// Gets or Sets the Name of the room
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or Sets the Description of the room
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Accesses the List of Items in the room
        /// </summary>
        public List<Item> RoomInventory
        {
            get { return _roomInventory; }
            set { _roomInventory = value; }
        }

        /// <summary>
        /// Gets or Sets the Room's Inventory Size
        /// </summary>
        public int RoomInventorySize 
        {
            get { return _roomInventorySize; }
            set { _roomInventorySize = value; }
        }

        /// <summary>
        /// Accesses the Array of Passages in the room, 0-7 is compass direction, North = 0 and rotating clock-wise. 8 = Up and 9 = Down. 
        /// </summary>
        public Passage[] Passages
        {
            get { return _passages; }
            set { _passages = value; }
        }

        #endregion  //  End of [ PROPERTIES ] region


        #region [ METHODS ]

        #endregion  // End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default constructor, instantiates to generic values.
        /// </summary>
        public Room()
        {
            _name = "Generic White Room";
            _description = "It's a small room, with hard white walls and floor. You suspect the ceiling is also hard and unforgiving.";
            _roomInventory = new List<Item>();
            _passages = new Passage[10];

            //  Increment roomCount by one, then make roomNumber equal to roomCount
            _roomNumber = ++_roomCount;
        }

        #endregion  //  End of [ CONSTRUCTORS ] region





    }
}
