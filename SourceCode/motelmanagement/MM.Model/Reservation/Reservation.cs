using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MM.Model
{
    /// <summary>
    /// Store information of a reservation
    /// </summary>
    [Serializable]
    public class Reservation : IDisposable, IDataErrorInfo, INotifyPropertyChanged
    {
        #region FIELDS

        private Guid reservationID;
        private Guest guest;
        private int numberOfAdult;
        private int numberOfChild;
        private string checkIn;
        private string checkOut;
        private RoomType roomType;
        private decimal totalPrice;
        private string service;
        private int numberOfDay;

        #endregion
        
        #region CONSTRUCTOR

        /// <summary>
        /// Constructor
        /// </summary>
        public Reservation()
        {
            NumberOfDay = 0;
            TotalPrice = 0;
            reservationID = Guid.NewGuid();
            Guest = new Guest();
            NumberOfChild = -1;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Using Guid to generate an unique ReservationID
        /// </summary>
        public Guid ReservationID { get => reservationID; set => reservationID = value; }

        /// <summary>
        /// NumberOfAdult
        /// </summary>
        public int NumberOfAdult
        {
            get
            {
                return numberOfAdult;
            }
            set
            {
                numberOfAdult = value;
                RaisePropertyChanged("NumberOfAdult");
            }
        }

        /// <summary>
        /// NumberOfChild
        /// </summary>
        public int NumberOfChild
        {
            get
            {
                return numberOfChild;
            }
            set
            {
                numberOfChild = value;
                RaisePropertyChanged("NumberOfChild");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(numberOfChild)));
            }
        }

        /// <summary>
        /// Date of CheckIn 
        /// </summary>
        public string CheckIn
        {
            get
            {
                return checkIn;
            }
            set
            {
                checkIn = value;
                RaisePropertyChanged("CheckIn");
                RaisePropertyChanged("CheckOut");
            }            
        }

        /// <summary>
        /// Date of CheckOut
        /// </summary>
        public string CheckOut
        {
            get
            {
                return checkOut;
            }
            set
            {
                checkOut = value;
                RaisePropertyChanged("CheckIn");
                RaisePropertyChanged("CheckOut");
            }
        }

        /// <summary>
        /// Each reservation has a Guest
        /// </summary>
        public Guest Guest { get => guest; set => guest = value; }

        /// <summary>
        /// Each reservation has a Room Type
        /// </summary>
        public RoomType RoomType { get => roomType; set => roomType = value; }

        /// <summary>
        /// Store Total price
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
            }
        }

        /// <summary>
        /// Store service depends on each type of Room.
        /// </summary>
        public string Service { get => service; set => service = value; }

        /// <summary>
        /// Number of days between CheckOut and CheckIn
        /// </summary>
        public int NumberOfDay { get => numberOfDay; set => numberOfDay = value; }

        #endregion

        #region METHODS

        /// <summary>
        /// Calculate Number Of Day = CheckOut - CheckIn
        /// </summary>
        public void CalculateNumberOfDay()
        {
            DateTime dateCheckIn = DateTime.Parse(CheckIn);
            DateTime dateCheckOut = DateTime.Parse(CheckOut);
            this.NumberOfDay = (int)(dateCheckOut - dateCheckIn).TotalDays + 1;
        }

        /// <summary>
        /// CalculateTotalPrice: [Price of each type of room] * [Days between CheckOut and CheckIn]
        /// </summary>
        public void CalculateTotalPrice()
        {
            this.TotalPrice = this.NumberOfDay * this.RoomType.Price;
        }

        /// <summary>
        /// Check if CheckOut is after CheckIn
        /// </summary>
        /// <returns></returns>
        private bool IsValidCheckInCheckOut()
        {
            bool compareResult = true;

            if (checkIn != null && checkOut != null)
            {
                CultureInfo enUS = new CultureInfo("en-US");
                DateTime checkInValue, checkOutValue;

                checkInValue = DateTime.Parse(checkIn, new CultureInfo("en-CA"));
                checkOutValue = DateTime.Parse(checkOut, new CultureInfo("en-CA"));

                compareResult = checkOutValue >= checkInValue;
            }

            return compareResult;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// this
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "CheckIn" && !IsValidCheckInCheckOut())
                    result = "Check in must be before Check out";
                else if (columnName == "CheckOut" && !IsValidCheckInCheckOut())
                    result = "Check out must be after Check in";

                return result;
            }
        }
        
        /// <summary>
        /// Error
        /// </summary>
        public string Error => throw new NotImplementedException();

        /// <summary>
        /// This is used to raise an event to call setter in both CheckIn or CheckOut 
        /// everytime CheckIn or CheckOut is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// RaisePropertyChanged
        /// </summary>
        /// <param name="prop"></param>
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #endregion
    }
}
