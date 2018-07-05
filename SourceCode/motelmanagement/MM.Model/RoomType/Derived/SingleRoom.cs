using System.Collections.Generic;

namespace MM.Model
{
    /// <summary>
    /// Store information of Single Room
    /// </summary>
    public class SingleRoom : RoomType
    {
        #region CONSTRUCTOR

        /// <summary>
        /// SingleRoom
        /// </summary>
        public SingleRoom()
        {
            this.Rooms = new List<Room>();
            this.Price = 50.0m;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Extra Service of Single room
        /// </summary>
        public override string ExtraService()
        {
            return ", Lunch";
        }

        /// <summary>
        /// Display all services of Single room
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Service() + ExtraService();
        }

        #endregion
    }
}
