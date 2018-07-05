using System.Collections.Generic;

namespace MM.Model
{
    /// <summary>
    /// Store information of Double Room
    /// </summary>
    public class DoubleRoom : RoomType
    {
        #region CONSTRUCTOR

        /// <summary>
        /// DoubleRoom
        /// </summary>
        public DoubleRoom()
        {
            this.Rooms = new List<Room>();
            this.Price = 60.0m;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Extra service of Double room
        /// </summary>
        public override string ExtraService()
        {
            return ", Lunch, Laundry";
        }

        /// <summary>
        /// Display all services of Double room
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Service() + ExtraService();
        }

        #endregion
    }
}
