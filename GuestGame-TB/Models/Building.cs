using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Building
    {
        #region [ FIELDS ]

        //  List of Rooms in the building
        private List<Room> _rooms;

        #endregion  //  End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Access the list of Rooms
        /// </summary>
        public List<Room> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        #endregion  //  End of [ PROPERTIES ] region


        #region [ METHODS ]

        /// <summary>
        /// Creates all Rooms in the building.
        /// </summary>
        public void InitializeRooms()
        {
            #region Outside of Building
            Room nOutside = new Room();
            nOutside.Name = "North of the Building";
            nOutside.Description = "The northern side of the building has a public entrance. It looks really tall from this angle. There is an alley to the east. To the west is a road.";
            _rooms.Add(nOutside);   //  Index 0

            Room eOutside = new Room();
            eOutside.Name = "East of the Building";
            eOutside.Description = "The eastern side of the building is an alley-way. There are some dumpsters with an unpleasant smell about them. The alley has exits to the north and south.";
            _rooms.Add(eOutside);   //  Index 1

            Room sOutside = new Room();
            sOutside.Name = "South of the Building";
            sOutside.Description = "The southern side of the building is the employee entrance. There is a security door with a key-card scanner. To the west is a road. There is an alley to the east.";
            _rooms.Add(sOutside);   //  Index 2

            Room wOutside = new Room();
            wOutside.Name = "West of the Building";
            wOutside.Description = "The western side of the building is next to a very busy street. Don't play in traffic. The road runs north-south.";
            _rooms.Add(wOutside);   //  Index 3
            #endregion
            #region Ground Floor
            Room nL1 = new Room();
            nL1.Name = "Northern end of Lobby";
            nL1.Description = "The public entrance to the building. There is a reception desk with a drinking bird toy on it. The exit of the lobby is north. The lobby extends to the south.";
            _rooms.Add(nL1);   //  Index 4

            Room cL1 = new Room();
            cL1.Name = "Southern end of Lobby";
            cL1.Description = "Further inside the lobby, there are some chairs and plants. The lobby extends to the north. There is elevators to the west, but they are out of order. To the south is an employee's only door.";
            _rooms.Add(cL1);   //  Index 5

            Room sL1 = new Room();
            sL1.Name = "Employee break room";
            sL1.Description = "A nice break room with plenty of seats and a vending machine. The door to the south leads out of the building. The door to the north goes to the lobby. There are stairs to the west.";
            _rooms.Add(sL1);   //  Index 6
            #endregion
        }

        public void InitializePassages()
        {
            Passage passage;
            #region Outside of Building
            #region North of Building
            //  North of building -> East of building
            passage = new Passage(_rooms.ElementAt(0), _rooms.ElementAt(1));
            _rooms.ElementAt(0).Passages[2] = passage;  //  Passage is east

            //  North of building -> West of building
            passage = new Passage(_rooms.ElementAt(0), _rooms.ElementAt(3));
            _rooms.ElementAt(0).Passages[6] = passage;  //  Passage is west

            //  North of building -> Northern lobby
            passage = new Passage(_rooms.ElementAt(0), _rooms.ElementAt(4));
            _rooms.ElementAt(0).Passages[4] = passage;  //  Passage is south
            #endregion
            #region East of Building
            //  East of building -> North of building
            passage = new Passage(_rooms.ElementAt(1), _rooms.ElementAt(0));
            _rooms.ElementAt(1).Passages[0] = passage;  //  Passage is north

            //  East of building -> South of building
            passage = new Passage(_rooms.ElementAt(1), _rooms.ElementAt(2));
            _rooms.ElementAt(1).Passages[4] = passage;  //  Passage is south
            #endregion
            #region South of Building
            //  South of building -> East of building
            passage = new Passage(_rooms.ElementAt(2), _rooms.ElementAt(1));
            _rooms.ElementAt(2).Passages[2] = passage;  //  Passage is east

            //  South of building -> West of building
            passage = new Passage(_rooms.ElementAt(2), _rooms.ElementAt(3));
            _rooms.ElementAt(2).Passages[6] = passage;  //  Passage is west

            //  South of building -> Employee break room
            passage = new Passage(_rooms.ElementAt(2), _rooms.ElementAt(6));
            _rooms.ElementAt(2).Passages[0] = passage;  //  Passage is north
            #endregion
            #region West of Building
            //  West of building -> North of building
            passage = new Passage(_rooms.ElementAt(3), _rooms.ElementAt(0));
            _rooms.ElementAt(3).Passages[0] = passage;  //  Passage is north

            //  West of building -> South of building
            passage = new Passage(_rooms.ElementAt(3), _rooms.ElementAt(2));
            _rooms.ElementAt(3).Passages[4] = passage;  //  Passage is south
            #endregion
            #endregion
            #region Inside of Building
            #region Northern Lobby
            //  Northern Lobby -> North of building
            passage = new Passage(_rooms.ElementAt(4), _rooms.ElementAt(0));
            _rooms.ElementAt(4).Passages[0] = passage;  //  Passage is north

            //  Northern Lobby -> Southern Lobby
            passage = new Passage(_rooms.ElementAt(4), _rooms.ElementAt(5));
            _rooms.ElementAt(4).Passages[4] = passage;  //  Passage is south
            #endregion
            #region Southern Lobby
            //  Southern Lobby -> Northern Lobby
            passage = new Passage(_rooms.ElementAt(5), _rooms.ElementAt(4));
            _rooms.ElementAt(5).Passages[0] = passage;  //  Passage is north

            //  Southern Lobby -> Employee break room
            passage = new Passage(_rooms.ElementAt(5), _rooms.ElementAt(6));
            _rooms.ElementAt(5).Passages[4] = passage;  //  Passage is south
            #endregion
            #region Employee Break Room
            //  Employee Break -> Southern Lobby
            passage = new Passage(_rooms.ElementAt(6), _rooms.ElementAt(5));
            _rooms.ElementAt(6).Passages[0] = passage;  //  Passage is north

            //  Employee Break -> South of building
            passage = new Passage(_rooms.ElementAt(6), _rooms.ElementAt(2));
            _rooms.ElementAt(6).Passages[4] = passage;  //  Passage is south
            #endregion

            #endregion
        }

        public void InitializeItems()
        {
            //  For testing, one Soda can on each side of the building.
            Item can = new Item(Item.ItemTypes.SODACAN);
            _rooms.ElementAt(0).RoomInventory.Add(can);
            _rooms.ElementAt(1).RoomInventory.Add(can);
            _rooms.ElementAt(2).RoomInventory.Add(can);
            _rooms.ElementAt(3).RoomInventory.Add(can);

            //  Keycard 001 in the eastern alley
            Item card001 = new Item(Item.ItemTypes.KEYCARD001);
            _rooms.ElementAt(1).RoomInventory.Add(card001);
        }

        #endregion  //  End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default constructor for the Buildings
        /// </summary>
        public Building()
        {
            _rooms = new List<Room>();

            InitializeRooms();

            InitializePassages();

            InitializeItems();
        }

        #endregion  //  End of [ CONSTRUCTORS ] region
    }
}
