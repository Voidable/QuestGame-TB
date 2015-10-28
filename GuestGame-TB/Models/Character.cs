using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGame_TB
{
    class Character
    {
        public enum Genders
        {
            MALE,
            FEMALE
        }

        public enum Races
        {
            HUMAN,
            ELF
        }


        #region [ FIELDS ]

        //  Name of Character
        private string _name;

        //  Description of Character
        private string _description;

        //  Gender of Character
        private Genders _gender;

        //  Race of Character
        private Races _race;

        #endregion // End of [ FIELDS ] region


        #region [ PROPERTIES ]

        /// <summary>
        /// Gets or Sets the Character's Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or Sets the Character's Description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets the Character's Gender
        /// </summary>
        public Genders Gender
        {
            get { return _gender; }
        }

        /// <summary>
        /// Gets the Character's Race
        /// </summary>
        public Races Race
        {
            get { return _race; }
        }

        #endregion // End of [ PROPERTIES ] region


        #region [ METHODS ]

        /// <summary>
        /// Returns a string with character's Name, gender, race, and description
        /// </summary>
        /// <returns></returns>
        virtual public string FullDescription()
        {
            string output = "";

            output = string.Format("{0} is a {1} {2}. {3}", _name, _gender.ToString().ToLower(), _race.ToString().ToLower(), _description);

            return output;
        }

        #endregion // End of [ METHODS ] region


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Default constructor, creates Mr. Smith.
        /// </summary>
        public Character()
        {
            _name = "Mr. Smith";
            _description = "He wears a suit, carries a briefcase, and talks kinda funny.";
            _gender = Genders.MALE;
            _race = Races.HUMAN;
        }

        /// <summary>
        /// Overloaded Constructor, allows customization of the Character
        /// </summary>
        /// <param name="name">Character's Name</param>
        /// <param name="description">Character's Description</param>
        /// <param name="gender">Character's Gender</param>
        /// <param name="race">Character's Race</param>
        public Character(string name, string description, Genders gender, Races race)
        {
            _name = name;
            _description = description;
            _gender = gender;
            _race = race;
        }

        #endregion // End of [ CONSTRUCTORS ] region
    }
}
