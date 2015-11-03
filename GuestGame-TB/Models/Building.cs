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
            nOutside.Description = "The northern side of the building has a public entrance. It looks really tall from this angle.";
            _rooms.Add(nOutside);   //  Index 0

            Room eOutside = new Room();
            eOutside.Name = "East of the Building";
            eOutside.Description = "The eastern side of the building is an alley-way. There are some dumpsters with an unpleasant smell about them.";
            _rooms.Add(eOutside);   //  Index 1

            Room sOutside = new Room();
            sOutside.Name = "South of the Building";
            sOutside.Description = "The southern side of the building is the employee entrance. There is a security door with a key-card scanner.";
            _rooms.Add(sOutside);   //  Index 2

            Room wOutside = new Room();
            wOutside.Name = "West of the Building";
            wOutside.Description = "The western side of the building is next to a very busy street. Don't play in traffic.";
            _rooms.Add(wOutside);   //  Index 3
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
        }

        #endregion  //  End of [ CONSTRUCTORS ] region
    }
}
