using System.Collections.Generic;

namespace MM.Model
{
    /// <summary>
    /// Store information of Suite Room
    /// </summary>
    public class SuiteRoom : RoomType
    {
        #region CONSTRUCTOR

        /// <summary>
        /// SuiteRoom
        /// </summary>
        public SuiteRoom()
        {
            this.Rooms = new List<Room>();
            this.Price = 70.0m;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Extra Service of Suite room
        /// </summary>
        public override string ExtraService()
        {
            return ", Lunch, Laundry, Pool, Gym";
        }

        /// <summary>
        /// Display all services of Suite room
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Service() + ExtraService();
        }

        #endregion
    }
}
