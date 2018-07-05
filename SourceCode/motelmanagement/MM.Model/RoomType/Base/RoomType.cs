using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MM.Model
{
    /// <summary>
    /// Store name of a Room Type
    /// </summary>
    public enum RoomTypeName
    {
        Guest,
        Single,
        Double,
        Suite
    }

    /// <summary>
    /// Store information of a Room Type
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(DoubleRoom))]
    [XmlInclude(typeof(GuestRoom))]
    [XmlInclude(typeof(SingleRoom))]
    [XmlInclude(typeof(SuiteRoom))]
    public abstract class RoomType
    {
        #region FIELDS

        private string roomTypeName;
        private decimal price;
        private List<Room> rooms;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// RoomTypeName
        /// </summary>
        public string RoomTypeName { get => roomTypeName; set => roomTypeName = value; }

        /// <summary>
        /// Rooms
        /// </summary>
        public List<Room> Rooms { get => rooms; set => rooms = value; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get => price; set => price = value; }

        #endregion

        #region METHODS

        /// <summary>
        /// Service of a room type
        /// </summary>
        public string Service()
        {
            return "Breakfast, Dinner";
        }

        /// <summary>
        /// Extra service of a room type
        /// </summary>
        public abstract string ExtraService();

        #endregion
    }
}
