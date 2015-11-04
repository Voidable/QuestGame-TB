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

        //  Is the passage locked
        private bool _isLocked;

        //  Can the passage be unlocked perminatly
        private bool _unlockable;

        //  What item will unlock the passage
        private Item.ItemTypes _unlockItem;

        //  What the passage says if it's locked.
        private string _lockedResponse;

        //  What the passage says when its traversed
        private string _moveThrough;

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

        /// <summary>
        /// Gets or Sets the lock state of the passage
        /// </summary>
        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        /// <summary>
        /// Gets or Sets the Unlockable state of the passage
        /// </summary>
        public bool IsUnlockable
        {
            get { return _unlockable; }
            set { _unlockable = value; }
        }

        /// <summary>
        /// Gets or Sets the unlock item of the passage
        /// </summary>
        public Item.ItemTypes UnlockItem
        {
            get { return _unlockItem; }
            set { _unlockItem = value; }
        }

        /// <summary>
        /// Gets or Sets the string for when the passage is locked.
        /// </summary>
        public string   LockedResponse
        {
            get { return _lockedResponse; }
            set { _lockedResponse = value; }
        }

        /// <summary>
        /// Gets or Sets the string for when the player moves through the passage
        /// </summary>
        public string MoveThrough
        {
            get { return _moveThrough; }
            set { _moveThrough = value; }
        }

        #endregion  //  End of [ PROPERTIES ] region


        #region [ METHODS ]

        public bool UnlockRoom(Item key)
        {
            bool output = false;

            if (key.Type == _unlockItem)
            {
                output = true;
                _isLocked = false;
            }

            return output;
        }

        #endregion  //  End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Create the Passage, must be passed an entrance room and an exit room.
        /// </summary>
        public Passage(Room entrance, Room exit)
        {
            _entrance = entrance;
            _exit = exit;
            _isLocked = false;
            _unlockable = false;
            _unlockItem = Item.ItemTypes.GENERIC;
            _lockedResponse = "It's locked. They thought of everything.";
            _moveThrough = "Whoosh!";

        }

        #endregion  //  End of [ CONSTRUCTORS ] region
    }
}
