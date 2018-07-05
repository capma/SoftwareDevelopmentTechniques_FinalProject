
namespace MM.Model
{
    /// <summary>
    /// Store information of a guest
    /// </summary>
    public class Guest
    {
        #region FIELDS

        private string firstName;
        private string lastName;
        private string address;
        private string phoneNumber;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get => firstName; set => firstName = value; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get => lastName; set => lastName = value; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get => address; set => address = value; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        #endregion
    }
}
