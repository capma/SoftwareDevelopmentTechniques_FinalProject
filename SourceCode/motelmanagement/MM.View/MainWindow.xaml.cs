using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MM.Model;
using MM.Utilities;

namespace MM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PROPERTIES

        private List<RoomType> roomTypes;
        private Reservation currentReservation;
        private ReservationList reservationList;

        /// <summary>
        /// RoomTypes
        /// </summary>
        public List<RoomType> RoomTypes { get => roomTypes; set => roomTypes = value; }

        /// <summary>
        /// ReservationList
        /// </summary>
        public ReservationList ReservationList { get => reservationList; set => reservationList = value; }

        /// <summary>
        /// CurrentReservation
        /// </summary>
        public Reservation CurrentReservation { get => currentReservation; set => currentReservation = value; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region METHODS

        #region BINDING DATA FOR ALL CONTROLS 

        /// <summary>
        /// Init
        /// </summary>
        private void Init()
        {
            // Initialize variable holding the current reservations
            CurrentReservation = new Reservation();

            // Initialize list of reservations
            ReservationList = new ReservationList();

            // Read data from XML
            ReadDataFromXML();

            // Display XML data to grid
            DisplayListToGrid();

            // Init data for list of room types and room numbers
            InitRoomData();                       

            // Enable/Disable UPDATE, DELETE, SAVE buttons
            EnableButtonWhenRegister();

            // Empty all textboxes
            Clear();

            // Set data context
            DataContext = this;

        }

        #endregion

        #region INITIALIZE DATA FOR SCREEN

        /// <summary>
        /// Initialize data of RoomType and Rooms
        /// </summary>
        private void InitRoomData()
        {
            RoomTypes = new List<RoomType>();

            // Create rooms type Guest
            RoomType roomTypeGuest = new GuestRoom();
            roomTypeGuest.RoomTypeName = RoomTypeName.Guest.ToString();

            roomTypeGuest.Rooms.Add(new Room() { RoomNumber = 101 });
            roomTypeGuest.Rooms.Add(new Room() { RoomNumber = 102 });
            roomTypeGuest.Rooms.Add(new Room() { RoomNumber = 103 });
            roomTypeGuest.Rooms.Add(new Room() { RoomNumber = 104 });
            RoomTypes.Add(roomTypeGuest);

            // Create rooms type Single
            RoomType roomTypeSingle = new SingleRoom();
            roomTypeSingle.RoomTypeName = RoomTypeName.Single.ToString();

            roomTypeSingle.Rooms.Add(new Room() { RoomNumber = 201 });
            roomTypeSingle.Rooms.Add(new Room() { RoomNumber = 202 });
            roomTypeSingle.Rooms.Add(new Room() { RoomNumber = 203 });
            roomTypeSingle.Rooms.Add(new Room() { RoomNumber = 204 });
            RoomTypes.Add(roomTypeSingle);

            // Create rooms type Double
            RoomType roomTypeDouble = new DoubleRoom();
            roomTypeDouble.RoomTypeName = RoomTypeName.Double.ToString();

            roomTypeDouble.Rooms.Add(new Room() { RoomNumber = 301 });
            roomTypeDouble.Rooms.Add(new Room() { RoomNumber = 302 });
            roomTypeDouble.Rooms.Add(new Room() { RoomNumber = 303 });
            roomTypeDouble.Rooms.Add(new Room() { RoomNumber = 304 });
            RoomTypes.Add(roomTypeDouble);

            // Create rooms type Suite
            RoomType roomTypeSuite = new SuiteRoom();
            roomTypeSuite.RoomTypeName = RoomTypeName.Suite.ToString();

            roomTypeSuite.Rooms.Add(new Room() { RoomNumber = 401 });
            roomTypeSuite.Rooms.Add(new Room() { RoomNumber = 402 });
            roomTypeSuite.Rooms.Add(new Room() { RoomNumber = 403 });
            roomTypeSuite.Rooms.Add(new Room() { RoomNumber = 404 });
            RoomTypes.Add(roomTypeSuite);
        }

        /// <summary>
        /// Clear all textboxes and reset CheckIn, CheckOut to the current date
        /// </summary>
        private void Clear()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtNumOfAdult.Text = string.Empty;
            txtNumOfChild.Text = string.Empty;
            lblSearchResult.Content = string.Empty;
            cboCheckIn.Text = DateTime.Now.ToShortDateString();
            cboCheckOut.Text = DateTime.Now.ToShortDateString();
            chkIsCheckedOut.IsChecked = false;
            txtFirstName.Focus();
        }

        #endregion

        #region VALIDATION

        /// <summary>
        /// Force Validation
        /// </summary>
        private void ForceValidation()
        {
            txtFirstName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtLastName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtAddress.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtPhoneNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtNumOfAdult.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtNumOfChild.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            cboCheckIn.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            cboCheckOut.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
        }

        #endregion

        #region READ/WRITE XML AND DISPLAY TO GRID

        /// <summary>
        /// Read Data From XML
        /// </summary>
        private void ReadDataFromXML()
        {
            Utilities.XMLController.ReadXML(ref this.reservationList);
        }

        /// <summary>
        /// Search and display result to grid
        /// </summary>
        /// <param name="textToFilter"></param>
        /// <param name="isSearchAll"></param>
        private void DisplayListToGrid(string textToFilter = "", bool isSearchAll = true)
        {
            if (reservationList != null)
            {
                int searchFound = 0;

                textToFilter = textToFilter.ToLower().Trim();

                var query = from reservation in reservationList.Reservations
                            where
                            (
                                (
                                    textToFilter != ""
                                    &&
                                    (
                                        reservation.Guest.FirstName.ToLower().Contains(textToFilter)
                                            || reservation.Guest.LastName.ToLower().Contains(textToFilter)
                                            || reservation.Guest.Address.ToLower().Contains(textToFilter)
                                            || reservation.Guest.PhoneNumber.Contains(textToFilter)
                                            || reservation.NumberOfAdult.Equals(textToFilter)
                                            || reservation.NumberOfChild.Equals(textToFilter)
                                            || reservation.RoomType.RoomTypeName.ToLower().Contains(textToFilter)
                                            || reservation.RoomType.Rooms.First().RoomNumber.ToString() == textToFilter
                                            || reservation.CheckIn.Equals(textToFilter)
                                            || reservation.CheckOut.Equals(textToFilter)
                                    )
                                )
                                ||
                                (
                                    isSearchAll == true
                                )
                            )
                            select reservation;

                grdReservation.ItemsSource = query.ToList();
                searchFound = query.ToList().Count;

                if (!isSearchAll)
                {
                    if (searchFound > 0)
                    {
                        lblSearchResult.Content = "There are " + searchFound + " records has been found";
                    }
                    else
                    {
                        lblSearchResult.Content = "There are no records";
                    }
                }                
            }
            else
            {
                lblSearchResult.Content = "No data";
            }
        }

        #endregion

        #region ENABLE/DISABLE BUTTONS

        /// <summary>
        /// Enable Button When Register
        /// </summary>
        private void EnableButtonWhenRegister()
        {
            btnRegister.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            ToggleInputControls(true);
        }

        /// <summary>
        /// Enable Button When Update
        /// </summary>
        private void EnableButtonWhenUpdate()
        {
            btnRegister.IsEnabled = false;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = true;
            ToggleInputControls(false);
        }

        /// <summary>
        /// Toggle top area of input controls (ENABLED / DISABLED)
        /// </summary>
        /// <param name="isTurnOn"></param>
        private void ToggleInputControls(bool isTurnOn)
        {
            HeaderInput.IsEnabled = isTurnOn;
        }

        #endregion

        #region CHECK RESERVED ROOM

        /// <summary>
        /// Search for a room in the list and check if it's booked or not
        /// </summary>
        /// <param name="roomNumberToCheck"></param>
        /// <param name="currentCheckedOut"></param>
        /// <returns></returns>
        private bool CheckIfRoomIsAlreadyBooked(int roomNumberToCheck, bool currentCheckedOut)
        {
            bool isReservedRoom = false;

            if (!currentCheckedOut)
            {
                var query = ReservationList
                        .Reservations
                        .Where(p => p.RoomType.Rooms.FirstOrDefault().RoomNumber
                                == roomNumberToCheck && (p.RoomType.Rooms.FirstOrDefault().IsCheckedOut == false));

                isReservedRoom = (query.Count() > 0);
            }
            

            return isReservedRoom;
        }

        #endregion

        #region PROCESS INPUT DATA

        /// <summary>
        /// Update entered data from controls to a variable called CurrentConservation
        /// </summary>
        /// <param name="currentReservation"></param>
        /// <returns></returns>
        private Reservation UpdateReservation(Reservation currentReservation)
        {
            Guest inputGuest = new Guest();
            Room selectedRoom = null;
            RoomType selectedRoomType = null;
            List<Room> tempListRooms = new List<Room>();

            inputGuest = new Guest()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Address = txtAddress.Text,
                PhoneNumber = txtPhoneNumber.Text
            };
            currentReservation.Guest = inputGuest;
            currentReservation.NumberOfAdult = int.Parse(txtNumOfAdult.Text);
            currentReservation.NumberOfChild = int.Parse(txtNumOfChild.Text);

            int roomNumber = ((Room)cboRoomNumber.SelectedValue).RoomNumber;
            string roomType = ((RoomType)lstRoomType.SelectedValue).RoomTypeName;
            selectedRoom = new Room() { RoomNumber = roomNumber, IsCheckedOut = (bool)chkIsCheckedOut.IsChecked };
            tempListRooms.Add(selectedRoom);

            RoomTypeName currentRoomType = (RoomTypeName)Enum.Parse(typeof(RoomTypeName), roomType);

            switch (currentRoomType)
            {
                case RoomTypeName.Guest:
                    selectedRoomType = new GuestRoom() { RoomTypeName = RoomTypeName.Guest.ToString(), Rooms = tempListRooms };
                    break;
                case RoomTypeName.Single:
                    selectedRoomType = new SingleRoom() { RoomTypeName = RoomTypeName.Single.ToString(), Rooms = tempListRooms };
                    break;
                case RoomTypeName.Double:
                    selectedRoomType = new DoubleRoom() { RoomTypeName = RoomTypeName.Double.ToString(), Rooms = tempListRooms };
                    break;
                case RoomTypeName.Suite:
                    selectedRoomType = new SuiteRoom() { RoomTypeName = RoomTypeName.Suite.ToString(), Rooms = tempListRooms };
                    break;
            }

            currentReservation.RoomType = selectedRoomType;
            currentReservation.Service = selectedRoomType.ToString();
            currentReservation.CheckIn = DateTime.Parse(cboCheckIn.Text).ToShortDateString();
            currentReservation.CheckOut = DateTime.Parse(cboCheckOut.Text).ToShortDateString();

            // Calculate number of days between CheckOut and CheckIn
            currentReservation.CalculateNumberOfDay();

            // Calculate the total price = [Price of each type room] x [Number of days between CheckOut and CheckIn]
            currentReservation.CalculateTotalPrice();

            return currentReservation;
        }

        #endregion

        #endregion

        #region EVENTS

        /// <summary>
        /// Window_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Window_Loaded error \n" + ex.Message, 
                                    this.Title, 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of REGISTER button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            bool isInvalid = false;
            bool currentCheckedOut = false;
            int roomNumber = 0;          

            try
            {
                // Call validation on the required fields
                ForceValidation();

                isInvalid = Validation.GetHasError(txtFirstName)
                            || Validation.GetHasError(txtLastName)
                            || Validation.GetHasError(txtAddress)
                            || Validation.GetHasError(txtPhoneNumber)
                            || Validation.GetHasError(txtNumOfAdult)
                            || Validation.GetHasError(txtNumOfChild)
                            || Validation.GetHasError(cboCheckIn)
                            || Validation.GetHasError(cboCheckOut)
                    ;

                // If all data is valid
                if (!isInvalid)
                {
                    roomNumber = ((Room)cboRoomNumber.SelectedValue).RoomNumber;
                    currentCheckedOut = (bool)chkIsCheckedOut.IsChecked;

                    // If the selected room is not booked
                    if (!CheckIfRoomIsAlreadyBooked(roomNumber, currentCheckedOut))
                    {
                        // Register it
                        Reservation newReservation = new Reservation();
                        newReservation = UpdateReservation(newReservation);
                        ReservationList.Add(newReservation);
                        XMLController.WriteToXML(ReservationList);
                        grdReservation.ItemsSource = ReservationList.Reservations;
                        Clear();
                    }
                    else
                    {
                        // If the selected room is booked then raise a message
                        MessageBox.Show(string.Format("Room {0} is already booked!\nPlease select another one.", roomNumber),
                                    this.Title,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while registering.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title,
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of SAVE button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isInvalid = false;
            int newRoomNumber = 0;
            int oldRoomNumber = 0;
            bool newCheckedOut = false;
            bool oldCheckedOut = false;
            bool isBooked = false;

            try
            {
                // Call validation on the required fields
                ForceValidation();

                isInvalid = Validation.GetHasError(txtFirstName)
                            || Validation.GetHasError(txtLastName)
                            || Validation.GetHasError(txtAddress)
                            || Validation.GetHasError(txtPhoneNumber)
                            || Validation.GetHasError(txtNumOfAdult)
                            || Validation.GetHasError(txtNumOfChild)
                            || Validation.GetHasError(cboCheckIn)
                            || Validation.GetHasError(cboCheckOut)
                    ;

                // If all data is valid
                if (!isInvalid)
                {                    
                    Reservation selectedReservation = ReservationList.Reservations.FirstOrDefault(i => i.ReservationID == CurrentReservation.ReservationID);
                    if (selectedReservation != null)
                    {
                        newRoomNumber = ((Room)cboRoomNumber.SelectedValue).RoomNumber;
                        oldRoomNumber = selectedReservation.RoomType.Rooms[0].RoomNumber;
                        newCheckedOut = (bool)chkIsCheckedOut.IsChecked;
                        oldCheckedOut = selectedReservation.RoomType.Rooms[0].IsCheckedOut;

                        // Check if the selected room is booked or not
                        // Case grid contains one row only
                        if (ReservationList.Count == 1)
                        {
                            // skip checking
                            isBooked = false;
                        }
                        // Case grid contains multiple rows
                        else
                        {
                            // if after ischeckout = true
                            if (newCheckedOut)
                            {
                                // skip checking
                                isBooked = false;
                            }
                            else
                            {
                                // if before checkedout (false) = after checkedout (false)
                                if (newCheckedOut == oldCheckedOut)
                                {
                                    // if before roomnumber = after room number
                                    if (newRoomNumber == oldRoomNumber)
                                    {
                                        // skip checking
                                        isBooked = false;
                                    }
                                    else
                                    {
                                        //if before roomnumber <> after room number ==> check
                                        isBooked = CheckIfRoomIsAlreadyBooked(newRoomNumber, newCheckedOut);
                                    }
                                }
                                // if before checkedout (true) != after checkedout (false)
                                else
                                {
                                    isBooked = CheckIfRoomIsAlreadyBooked(newRoomNumber, newCheckedOut);
                                }
                            }
                        }

                        // If the selected room is not booked
                        if (!isBooked)
                        {
                            // Save it
                            selectedReservation = UpdateReservation(selectedReservation);
                            XMLController.WriteToXML(ReservationList);
                            grdReservation.ItemsSource = ReservationList.Reservations;
                            grdReservation.Items.Refresh();
                        }
                        // If the selected room is booked
                        else
                        {
                            // Raise a message
                            MessageBox.Show(string.Format("Room {0} is already booked!\nPlease select another one.",
                                    newRoomNumber),
                                    this.Title,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while saving.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event on the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Toggle buttons in the top area
                EnableButtonWhenUpdate();

                CurrentReservation = grdReservation.SelectedItem as Reservation;

                if (CurrentReservation != null)
                {
                    // Display data from the selected row to the top area
                    RoomType reservedRoomType = RoomTypes.First(r => r.RoomTypeName.ToString().Equals(CurrentReservation.RoomType.RoomTypeName));
                    Room reservedRoom = reservedRoomType.Rooms.First(r => r.RoomNumber == CurrentReservation.RoomType.Rooms.FirstOrDefault().RoomNumber);

                    txtFirstName.Text = CurrentReservation.Guest.FirstName;
                    txtLastName.Text = CurrentReservation.Guest.LastName;
                    txtAddress.Text = CurrentReservation.Guest.Address;
                    txtPhoneNumber.Text = CurrentReservation.Guest.PhoneNumber;
                    txtNumOfAdult.Text = CurrentReservation.NumberOfAdult.ToString();
                    txtNumOfChild.Text = CurrentReservation.NumberOfChild.ToString();
                    lstRoomType.SelectedItem = reservedRoomType;
                    cboRoomNumber.SelectedItem = reservedRoom;
                    cboCheckIn.Text = CurrentReservation.CheckIn.ToString();
                    cboCheckOut.Text = CurrentReservation.CheckOut.ToString();
                    chkIsCheckedOut.IsChecked = CurrentReservation.RoomType.Rooms.FirstOrDefault().IsCheckedOut;

                    // Validate again to clear all previous errors
                    ForceValidation();
                }                
            }
            catch (Exception ex)
            {
            MessageBox.Show("Error occurs while changing selection.\n"
                    + ex.Message + "\n"
                    + ex.InnerException,
                    this.Title,
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of CANCEL button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grdReservation.UnselectAll();
                EnableButtonWhenRegister();
                Clear();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while cancelling.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of UPDATE button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                btnRegister.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnSave.IsEnabled = true;
                btnCancel.IsEnabled = true;
                ToggleInputControls(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while updating.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of DELETE button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you really want to delete the selected reservation?"
                    , this.Title
                    , MessageBoxButton.YesNo
                    , MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                    ReservationList.Remove(CurrentReservation);
                    XMLController.WriteToXML(ReservationList);
                    grdReservation.ItemsSource = ReservationList.Reservations;
                    grdReservation.Items.Refresh();
                    btnCancel_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while deleting.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of SEARCH button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayListToGrid(txtSearch.Text, false);
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while searching.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Handle click event of DISPLAY button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayListToGrid();
                btnCancel_Click(sender, e);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs while displaying.\n"
                                + ex.Message + "\n"
                                + ex.InnerException,
                                this.Title, MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }
        }

        #endregion

    }
}
