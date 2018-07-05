using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace MM.Model
{
    /// <summary>
    /// Store a list of reserved rooms
    /// </summary>
    [XmlRoot("ReservationList")]
    public class ReservationList
    {
        #region FIELDS

        private ObservableCollection<Reservation> reservations = null;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Reservations
        /// </summary>
        [XmlArray("ListOfBookedRooms")]
        [XmlArrayItem("Reservation")]
        public ObservableCollection<Reservation> Reservations { get => reservations; set => reservations = value; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// ReservationList
        /// </summary>
        public ReservationList()
        {
            reservations = new ObservableCollection<Reservation>();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Reservation this[int index]
        {
            get => reservations[index];
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count
        {
            get => reservations.Count;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="reservation"></param>
        public void Add(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="reservation"></param>
        public void Remove(Reservation reservation)
        {
            reservations.Remove(reservation);
        }

        #endregion
    }
}
