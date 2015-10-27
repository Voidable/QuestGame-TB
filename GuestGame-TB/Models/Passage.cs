using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Passage
    {
        #region [ FIELDS ]

        //  The Room the player can enter the passage
        private Room _entrance;

        //  The Room the player can exit the passage
        private Room _exit;

        #endregion  //  End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets the Entrance of the Passage
        /// </summary>
        public Room Entrance 
        {
            get { return _entrance; }
        }

        /// <summary>
        /// Gets the Exit of the Passage
        /// </summary>
        public Room Exit
        {
            get { return _exit; }
        }

        #endregion  //  End of [ PROPERTIES ] region


        #region [ METHODS ]

        #endregion  //  End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Create the Passage, must be passed an entrance room and an exit room.
        /// </summary>
        public Passage(Room entrance, Room exit)
        {
            _entrance = entrance;
            _exit = exit;
        }

        #endregion  //  End of [ CONSTRUCTORS ] region
    }
}
