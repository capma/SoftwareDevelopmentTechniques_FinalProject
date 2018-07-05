using System.Collections.Generic;

namespace MM.Model
{
    /// <summary>
    /// Store information of Guest Room
    /// </summary>
    public class GuestRoom : RoomType
    {
        #region CONSTRUCTOR

        /// <summary>
        /// GuestRoom
        /// </summary>
        public GuestRoom()
        {
            this.Rooms = new List<Room>();
            this.Price = 35.0m;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Extra Service of Guest room
        /// </summary>
        public override string ExtraService()
        {
            return "";
        }

        /// <summary>
        /// Display all services of Guest room
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Service() + ExtraService();
        }

        #endregion
    }
}
