using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Item
    {
        public enum ItemTypes
        {
            GENERIC
        }

        #region [ FIELDS ]

        //  Type of the Item
        private ItemTypes _type;

        //  Name of the Item
        private string _name;

        //  Description of the Item
        private string _description;

        //  Size of the item, for inventory limit purposes
        private int _size;

        //  Can the item be stacked upon similar items in an inventory
        private bool _isStackable;

        //  Number of the item stored, used for items that can be stacked
        private int _quantity;

        //  Can the item be seen outside of the inventory, ie. player looks at guard
        private bool _isVisibleOutsideInventory;

        #endregion  //  End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets the Type of the Item
        /// </summary>
        public ItemTypes Type 
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or Sets the Name of the Item
        /// </summary>
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; }
        }

        /// <summary>
        /// Gets or Sets the Description of the Item
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or Sets the Size of the Item
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Gets the Stackable state of the Item
        /// </summary>
        public bool IsStackable 
        {
            get { return _isStackable; }
        }

        /// <summary>
        /// Gets or Sets the Quantity of the Item
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Gets the Visible Outside Inventory state of the Item
        /// </summary>
        public bool VisibleOutsideInventory
        {
            get { return _isVisibleOutsideInventory; }
        }

        #endregion  //  End of [ PROPERTIES ] region


        #region [ METHODS ]

        #endregion  //  End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default Item Constructor, assumes GENERIC ItemType
        /// </summary>
        public Item()
        {
            _type = ItemTypes.GENERIC;
            _name = "A very Generic, thing...";
            _description = "It's so Generic I can't describe it.";
            _size = 0;
            _isStackable = true;
            _quantity = 0;
            _isVisibleOutsideInventory = true;
        }

        #endregion  //  End of [ CONSTRUCTORS ] region
    }
}
