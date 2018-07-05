
namespace MM.Model
{
    /// <summary>
    /// Store information of a Room
    /// </summary>
    public class Room
    {
        #region FIELDS
        
        private int roomNumber;
        private bool isCheckedOut;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// RoomNumber
        /// </summary>
        public int RoomNumber { get => roomNumber; set => roomNumber = value; }

        /// <summary>
        /// IsReserved
        /// </summary>
        public bool IsCheckedOut { get => isCheckedOut; set => isCheckedOut = value; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor
        /// </summary>
        public Room()
        {
            this.isCheckedOut = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="RoomID"></param>
        /// <param name="RoomNumber"></param>
        public Room(int RoomNumber)
        {
            this.IsCheckedOut = false;
            this.RoomNumber = RoomNumber;
        }

        #endregion

    }
}
