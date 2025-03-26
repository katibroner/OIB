using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Asm.As.Oib.FactoryCalendar.Proxy.Architecture.Notifications;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects;
using Asm.As.Controls.WinForms.CalendarControl;
using Asm.As.Controls.WinForms.CalendarControl.Architecture.EventArgs;
using Asm.As.Controls.WinForms.CalendarControl.Architecture.Objects;
using Asm.As.Controls.WinForms.CalendarControl.Controls;
using Asm.As.Oib.ConfigurationManager.Proxy.Architecture.Objects;
using Asm.As.Oib.ConfigurationManager.Proxy.Business.Objects;
using Asm.As.Oib.FactoryCalendar.Proxy.FactoryCalendarServiceRef;
using Appointment = Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects.Appointment;
using AppointmentRecurrence = Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects.AppointmentRecurrence;
using AppointmentType = Asm.As.Oib.FactoryCalendar.Proxy.Business.Types.AppointmentType;
using DailyRecurrenceRepeat = Asm.As.Oib.FactoryCalendar.Proxy.Business.Types.DailyRecurrenceRepeat;
using DayOfWeekRecurrence = Asm.As.Oib.FactoryCalendar.Proxy.Business.Types.DayOfWeekRecurrence;
using FactoryCalendar = Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects.FactoryCalendar;
using RecurrencePatternType = Asm.As.Oib.FactoryCalendar.Proxy.Business.Types.RecurrencePatternType;

namespace FcSmartProxyDemo
{
    public partial class MainForm : Form, ICalendarControlAppointmentsProvider
    {
        public event EventHandler AppointmentsChanged;

        private FactoryCalendar _factoryCalendar;
        private Site _fcSite;
        private UpdateSubscription _updateSubscription;
        private List<CalendarItem> _calendarItems;
        private CalendarItem _selectedCalendarItem;
        private Appointment _selectedAppointment;
        DataTable _factoryLayoutDataTable;
        private readonly EnumMapper<AppointmentType> _appointmentTypeMapper;
        private readonly CustomVisualAppointmentFactory _visualAppointmentFactory;
        private readonly List<CalendarControlAppointment> _internalAppointments = new List<CalendarControlAppointment>();
        private readonly bool _isLoadingAppointmentsAutomatically = false;
        private bool _updatingSelections;


        public MainForm()
        {
            InitializeComponent();
            
            calendarMonthControl.SuppressUpdate();
            calendarMonthControl.Culture = CultureInfo.CurrentCulture;
            calendarMonthControl.FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            calendarMonthControl.CurrentDate = DateTime.Now;
            calendarMonthControl.AreMonthButtonsVisible = true;

            _visualAppointmentFactory = new CustomVisualAppointmentFactory();
            calendarMonthControl.SetVisualAppointmentsFactory(_visualAppointmentFactory);
            calendarMonthControl.SetDayOfMonthControlFactory(new CustomDayOfMonthControlFactory());
            calendarMonthControl.SetAppointmentsProvider(this);
            calendarMonthControl.AllowUpdate();
            calendarMonthControl.UpdateContents(calendarMonthControl.CurrentDate);
            calendarMonthControl.AppointmentSelected += Calendar_AppointmentSelected;
            calendarMonthControl.DaySelected += Calendar_DaySelected;
            calendarMonthControl.MonthChanged += CalendarMonthControl_MonthChanged;

            dtLoadFrom.Value = calendarMonthControl.FirstVisibleDay;
            dtLoadTo.Value = calendarMonthControl.LastVisibleDay;
            
            _appointmentTypeMapper = new EnumMapper<AppointmentType>();
            _appointmentTypeMapper.ForbidObsolete();
            _appointmentTypeMapper.Forbid(AppointmentType.Undefined);
            _appointmentTypeMapper.Forbid(AppointmentType.InheritanceBlocker);
            _appointmentTypeMapper.AddSubstitution("Non-Scheduled Time", AppointmentType.Holiday);
            _appointmentTypeMapper.AddSubstitution("Scheduled Downtime", AppointmentType.ScheduledDowntime);

            Size = new Size(800, 600);

        }


        #region UI event handlers
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }
        private void btnCreateTestData_Click(object sender, EventArgs e)
        {
            CreateTestData();
            RefreshLists();
        }
        private void btnCleanup_Click(object sender, EventArgs e)
        {
            Cleanup();
            RefreshLists();
            _factoryLayoutDataTable.Clear();
        }
        private void btnLoadAppointments_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAppointments();
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
            tabControl.SelectedTab = tabAppointments;
        }
        private void btnLoadCalendarItems_Click(object sender, EventArgs e)
        {
            LoadCalendarItems();
            if (tabControl.SelectedTab != tabCalendar && tabControl.SelectedTab != tabCalendarItems)
                tabControl.SelectedTab = tabCalendarItems;
        }
        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            ShowNewAppointmentForm();
        }
        private void btnEditAppointment_Click(object sender, EventArgs e)
        {
            ShowEditAppointmentForm();
        }
        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            DeleteAppointment(_selectedAppointment);
        }
        private void cbFactoryElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIncludeInheritance.Enabled = cbFactoryElement.SelectedIndex > 0;
            cbSubscribeUpdates.Enabled = cbFactoryElement.SelectedIndex > 0;
        }
        private void dataGridCalendarItems_SelectionChanged(object sender, EventArgs e)
        {
            int rowIndex = -1;
            if (dataGridCalendarItems.SelectedRows.Count > 0)
                rowIndex = dataGridCalendarItems.SelectedRows[0].Index;
            else if(dataGridCalendarItems.SelectedCells.Count > 0)
                rowIndex = dataGridCalendarItems.SelectedCells[0].RowIndex;

            if (rowIndex >= 0)
            {
                CalendarItemForDataGrid item = dataGridCalendarItems.Rows[rowIndex].DataBoundItem as CalendarItemForDataGrid;
                _selectedCalendarItem = _calendarItems.FirstOrDefault(a => a.Id == item.Id);
            }

            //Print(dataGridCalendarItems.Rows[dataGridCalendarItems.SelectedCells[0].RowIndex].DataBoundItem?.ToString());

            if (_selectedCalendarItem != null)
                OnSelectedCalendarItemChanged();
        }
        private void dataGridCalendarItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEditAppointmentForm();
        }
        private void dataGridAppointments_SelectionChanged(object sender, EventArgs e)
        {
            int rowIndex = -1;
            if (dataGridAppointments.SelectedRows.Count > 0)
                rowIndex = dataGridAppointments.SelectedRows[0].Index;
            else if(dataGridAppointments.SelectedCells.Count > 0)
                rowIndex = dataGridAppointments.SelectedCells[0].RowIndex;

            if (rowIndex >= 0)
            {
                //int id = (int)dataGridAppointments.Rows[rowIndex].Cells[1].Value;
                AppointmentForDataGrid appointment = dataGridAppointments.Rows[rowIndex].DataBoundItem as AppointmentForDataGrid;
                _selectedAppointment = _factoryCalendar.CalendarModel.Appointments.FirstOrDefault(a => a.Id == appointment.Id);
            }

            if(_selectedAppointment != null)
                OnSelectedAppointmentChanged();
        }
        private void dataGridAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEditAppointmentForm();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            OnDisconnected();
            _factoryCalendar?.Dispose();
            base.OnClosing(e);
        }

        #endregion

        #region Methods

        #region DOC_CREATE_INSTANCE
        /// <summary>
        /// Obtain a FactoryCalendar Proxy instance
        /// </summary>
        private void Connect()
        {
            if (_factoryCalendar != null)
            {
                PrintLine("FactoryCalendar already connected");
                return;
            }
                
            try
            {
                PrintLine($"{DateTime.Now:T}: Connecting to FactoryCalendar service");
                _factoryCalendar = FactoryCalendar.GetInstance();
                OnConnected();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to FactoryCalendar service.");
                PrintLine(e.Message);
            }
            
            LoadFactoryElements();
        }
        #endregion

        /// <summary>
        /// Opens a Window with a form that allows you to configure and create a new appointment
        /// </summary>
        /// <returns>A newly created Appointment or null</returns>
        private Appointment ShowNewAppointmentForm()
        {
            return ShowNewAppointmentForm("New Appointment", DateTime.Now);
        }
        /// <summary>
        /// Opens a Window with a form that allows you to configure and create a new appointment
        /// </summary>
        /// <param name="name">Initial Name for the new Appointment</param>
        /// <param name="startTime">Initial start for the new Appointment</param>
        /// <returns>A newly created Appointment or null</returns>
        private Appointment ShowNewAppointmentForm(string name, DateTime startTime)
        {
            NewAppointmentForm form = new NewAppointmentForm(_appointmentTypeMapper, GetAllElements(_fcSite));
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult dialogResult = form.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                PrintLine($"{DateTime.Now:T}: New Appointment added");
                RefreshLists();
            }
            return form.Appointment;
        }

        /// <summary>
        /// Opens a Window with a form that allows you to make changes to the selected appointment
        /// </summary>
        private void ShowEditAppointmentForm()
        {
            if (_selectedAppointment == null && _selectedCalendarItem == null)
            {
                MessageBox.Show("Please select an Appointment or Calendar Item first.", "No Appointment selected");
                return;
            }

            List<int> allowedElements = GetAllElementIds(_fcSite);
            if(_selectedAppointment == null || !allowedElements.Contains(_selectedAppointment.Isa95Id))
            {
                MessageBox.Show("You cannot edit this Appointment");
                return;
            }

            var form = new EditAppointmentForm(_appointmentTypeMapper);
            form.StartPosition = FormStartPosition.CenterParent;
            form.Appointment = _selectedAppointment;
            DialogResult dialogResult = form.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                PrintLine($"{DateTime.Now:T}: Appointment edited");
                RefreshLists();
            }
        }

        #region DOC_DELETE_APPOINTMENT
        /// <summary>
        /// Delete selected appointment (asks User for confirmation)
        /// </summary>
        /// <param name="appointment">Appointment to be deleted</param>
        private void DeleteAppointment(Appointment appointment)
        {
            List<int> allowedElements = GetAllElementIds(_fcSite);
            if (appointment == null || !allowedElements.Contains(appointment.Isa95Id))
            {
                MessageBox.Show("You cannot delete this Appointment");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Do you really want to delete this appointment?", "Delete Appointment",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                appointment.Delete();
                PrintLine($"{DateTime.Now:T}: Appointment deleted");
                RefreshLists();
            }
        }
        #endregion

        #region DOC_GET_APPOINTMENTS
        /// <summary>
        /// Lists available appointments
        /// </summary>
        private void LoadAppointments()
        {
            List<int> factoryElements = GetAllElementIds(_fcSite);
            IEnumerable<Appointment> appointments = _factoryCalendar.CalendarModel.AppointmentsReadOnly
                .Where(a => factoryElements.Contains(a.Isa95Id));
            

            int id = _selectedAppointment?.Id ?? -1;
            //Update List
            List<AppointmentForDataGrid> appointmentsForDataGrid = new List<AppointmentForDataGrid>();
            foreach (var item in appointments)
            {
                appointmentsForDataGrid.Add(new AppointmentForDataGrid(item, _appointmentTypeMapper));
            }
            dataGridAppointments.AutoGenerateColumns = false;
            dataGridAppointments.DataSource = appointmentsForDataGrid;
            PrintLine($"{DateTime.Now:T}: Appointments loaded");

            if(id > 0) SelectAppointment(id);
        }
        #endregion

        /// <summary>
        /// Loads Calendar Items for the specified time period 
        /// </summary>
        private void LoadCalendarItems()
        {
            int? oid = cbFactoryElement.SelectedIndex > 0 ? (int?)cbFactoryElement.SelectedValue : null;
            LoadCalendarItems(dtLoadFrom.Value.Date, dtLoadTo.Value.Date.AddDays(1), oid, cbIncludeInheritance.Checked, cbSubscribeUpdates.Checked);
        }

        #region DOC_GET_CALENDAR_ITEMS
        /// <summary>
        /// Loads Calendar Items for the specified time period 
        /// </summary>
        /// <param name="fromDate">Start of the time period</param>
        /// <param name="toDate">End of the time period</param>
        /// <param name="oid">Optional: the OIB ID of a Factory Element</param>
        /// <param name="includeInheritance">Optional: if true (default), the appointments of the parent Factory Elements are also included</param>
        /// <param name="subscribeForUpdates">Optional: if true (default), the SubscribedChange event will be raised if one of the Calendar Items changes</param>
        private void LoadCalendarItems(DateTime fromDate, DateTime toDate, int? oid = null, bool includeInheritance = true, bool subscribeForUpdates = false)
        {
            try
            {

                PrintLine($"{DateTime.Now:T}: Loading CalendarItems");
                if (_fcSite != null)
                {
                    FactoryCalendarQuery fcQuery = FactoryCalendarQuery.GetInstance();
                    var calendarItems = oid == null
                        ? fcQuery.GetCalendarItems(fromDate, toDate)
                        : fcQuery.GetCalendarItems(fromDate, toDate, includeInheritance, oid.Value);
                    //_calendarItems = calendarItems.Where((ci) => ci.Isa95Object.Path.StartsWith(_fcSite.Path)).ToList();
                    _calendarItems = calendarItems.Where((ci) => ci.Isa95Object.Path.StartsWith(_fcSite.Path) || _fcSite.Path.StartsWith(ci.Isa95Object.Path)).ToList();
                    //_calendarItems = calendarItems.ToList();
                }
                else if(_calendarItems != null)
                {
                    _calendarItems.Clear();
                }
                OnCalendarItemsChanged();

                UnsubscribeUpdates();
                if(oid != null && subscribeForUpdates)
                    SubscribeForUpdates(fromDate, toDate, oid.Value, includeInheritance);
                
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
            
        }
        #endregion

        #region DOC_SUBSCRIBE_UPDATES
        /// <summary>
        /// Subscribe for updates through the SubscribedChange event
        /// </summary>
        /// <param name="fromDate">Start of the time period</param>
        /// <param name="toDate">End of the time period</param>
        /// <param name="oid">the OIB ID of a Factory Element</param>
        /// <param name="includeInheritance">if true, the appointments of the parent Factory Elements are also considered</param>
        private void SubscribeForUpdates(DateTime fromDate, DateTime toDate, int oid, bool includeInheritance)
        {
            try
            {
                PrintLine($"{DateTime.Now:T}: Subscribing for Calendar Items updates");
                _updateSubscription = new UpdateSubscription(oid, includeInheritance, fromDate, toDate);
                _factoryCalendar.SubscribeForCalendarItems(_updateSubscription);
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
        }
        #endregion

        #region DOC_UNSUBSCRIBE_UPDATES
        /// <summary>
        /// Drop the existing subscription
        /// </summary>
        private void UnsubscribeUpdates()
        {
            if (_updateSubscription != null)
                UnsubscribeUpdates(_updateSubscription);
            _updateSubscription = null;
        }
        /// <summary>
        /// Drop the provided subscription
        /// </summary>
        /// <param name="subscription">The subscription object</param>
        private void UnsubscribeUpdates(UpdateSubscription subscription)
        {
            if(subscription == null) return;;
            try
            {
                PrintLine($"{DateTime.Now:T}: Unsubscribing Calendar Items updates: {subscription}");
                _factoryCalendar.UnsubscribeForCalendarItems(subscription);
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Load new data for the DataGrids in the UI
        /// </summary>
        private void RefreshLists()
        {
            if (dataGridAppointments.Rows.Count > 0)
                LoadAppointments();
            if (_calendarItems != null && _calendarItems.Count > 0)
            {
                DateTime from = _calendarItems.First().StartTime.Date;
                DateTime to = _calendarItems.Last().EndTime.Date.AddDays(1);
                int? oid = cbFactoryElement.SelectedIndex > 0 ? (int?)cbFactoryElement.SelectedValue : null;
                LoadCalendarItems(from, to, oid, cbIncludeInheritance.Checked);
            }
        }
        /// <summary>
        /// Load Factory Elements for the ComboBox
        /// </summary>
        private void LoadFactoryElements()
        {
            PrintLine($"{DateTime.Now:T}: Loading Factory Elements");
            try
            {
                //Session configurationManager = Session.CurrentSession;
                //List<Isa95Base> elements = configurationManager.GetAllObjects();
                CreateOrLoadTestFactoryElements();

                List<Isa95Base> elements = GetAllElements(_fcSite);

                PrintLine($"{DateTime.Now:T}: Factory Elements updated");
                _factoryLayoutDataTable = GetFactoryLayoutDataTable(elements, true);

                cbFactoryElement.DataSource = _factoryLayoutDataTable.DefaultView;
                cbFactoryElement.DisplayMember = "Path";
                cbFactoryElement.ValueMember = "OID";
                cbFactoryElement.SelectedIndex = 0;

                cbFactoryElement.Enabled = true;
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
        }

        /// <summary>
        /// Add or Load Factory Elements for this Demo App, so we can not mess up the real appointments
        /// </summary>
        private void CreateOrLoadTestFactoryElements()
        {
            if (_factoryCalendar == null) return;
            if (_fcSite != null) return;

            PrintLine($"{DateTime.Now:T}: Creating Factory Elements");
            try
            {
                Session configurationManager = Session.CurrentSession;
                string siteName = "FC Test Site";
                //create site if it not exists
                _fcSite = configurationManager.Enterprise.Sites.FirstOrDefault(site => site.Name == siteName);
                if (_fcSite == null)
                {
                    _fcSite = configurationManager.Enterprise.CreateNewSiteAndAddToCollection();
                    _fcSite.Name = siteName;
                }

                //create lines if they not exist
                for (int lineIndex = 0; lineIndex < 2; lineIndex++)
                {
                    string lineName = $"Line {lineIndex + 1}";
                    ProductionLine line = _fcSite.ProductionLines.FirstOrDefault(l => l.Name == lineName);
                    if (line == null)
                    {
                        line = _fcSite.CreateNewProductionLineAndAddToCollection();
                        line.Name = lineName;
                    }
                    //create cells if they not exist
                    for (int cellIndex = 0; cellIndex < 2; cellIndex++)
                    {
                        string cellName = $"Cell {lineIndex + 1}-{cellIndex + 1}";
                        WorkCell cell = line.WorkCells.FirstOrDefault(c => c.Name == cellName);
                        if (cell == null)
                        {
                            cell = line.CreateNewWorkCellAndAddToCollection();
                            cell.Name = cellName;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
        }

        /// <summary>
        /// Get all children of the provided Factory Site (including the Site itself)
        /// </summary>
        /// <param name="site">The Factory Site Element</param>
        /// <returns>A List containing all child elements (including the Site)</returns>
        private List<Isa95Base> GetAllElements(Site site)
        {
            List<Isa95Base> elements = new List<Isa95Base>();
            if (site == null) return elements;
            elements.Add(site);
            foreach (var line in _fcSite.ProductionLines)
            {
                elements.Add(line);
                foreach (var cell in line.WorkCells)
                {
                    elements.Add(cell);
                }
            }
            return elements;
        }

        /// <summary>
        /// Get only the IDs of all children of the provided Factory Site (including the Site itself)
        /// </summary>
        /// <param name="site">The Factory Site Element</param>
        /// <returns>A List containing only the IDs of all child elements (including the Site)</returns>
        private List<int> GetAllElementIds(Site site)
        {
            List<int> elements = new List<int>();
            if (site == null) return elements;
            elements.Add(site.OID);
            foreach (var line in _fcSite.ProductionLines)
            {
                elements.Add(line.OID);
                foreach (var cell in line.WorkCells)
                {
                    elements.Add(cell.OID);
                }
            }
            return elements;
        }

        /// <summary>
        /// Create a DataTable from a list of Factory Elements as Source for a ComboBox
        /// </summary>
        /// <param name="elements">List of Factory Elements</param>
        /// <param name="addUndefinedFirstRow">if true an "Undefined" Elements is added as first option</param>
        /// <returns>A DataTable that can be used as Source for a ComboBox
        ///             Example: 
        ///             comboBox.DataSource = dataTable.DefaultView;
        ///             comboBox.DisplayMember = "Path";
        ///             comboBox.ValueMember = "OID";
        /// </returns>
        private DataTable GetFactoryLayoutDataTable(List<Isa95Base> elements, bool addUndefinedFirstRow = false)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OID", typeof(int));
            dataTable.Columns.Add("Path", typeof(string));

            if (addUndefinedFirstRow)
            {
                DataRow firstRow = dataTable.NewRow();
                firstRow["OID"] = -1;
                firstRow["Path"] = "Undefined";
                dataTable.Rows.Add(firstRow);
            }

            foreach (var element in elements)
            {
                DataRow row = dataTable.NewRow();
                row["OID"] = element.OID;
                row["Path"] = element.Path;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #region DOC_CREATE_RECURRING_APPOINTMENT
        /// <summary>
        /// Create some Appointments as Test Data for the Demo
        /// </summary>
        private void CreateTestData()
        {
            if(_fcSite == null)
                LoadFactoryElements();

            PrintLine($"{DateTime.Now:T}: Creating some appointments");

            int oid;
            string name, description;
            DateTime startTime, endTime;
            AppointmentType type;
            Appointment appointment;

            oid = _fcSite.OID;
            name = "Downtime";
            if (!AppointmentExists(name, oid))
            {
                description = "Site Downtime";
                startTime = new DateTime(2022, 4, 4, 6, 0, 0);
                endTime = new DateTime(2022, 4, 4, 7, 0, 0);
                type = AppointmentType.ScheduledDowntime;
                appointment = CreateAppointment(type, oid, name, description, startTime, endTime);
                appointment.CustomState = _factoryCalendar.CustomStates.First(state => state.Value == "42");
                appointment.GroupReason = _factoryCalendar.GroupReasons[0];
                appointment.Recurring = true;
                SaveAppointment(appointment);
            }

            oid = _fcSite.ProductionLines[0].OID;
            name = "Daily 9to5";
            if (!AppointmentExists(name, oid))
            {
                description = "Daily shift";
                type = AppointmentType.Shift;
                startTime = new DateTime(2022, 4, 4, 9, 0, 0);
                endTime = new DateTime(2022, 4, 4, 17, 0, 0);
                appointment = CreateAppointment(type, oid, name, description, startTime, endTime);
                appointment.Recurring = true;
                appointment.Recurrence.RecurrenceType = RecurrencePatternType.Daily;
                appointment.Recurrence.Daily.RepeatOnDaysOfWeek = DailyRecurrenceRepeat.WeekDays;
                SaveAppointment(appointment);
            }

            oid = _fcSite.ProductionLines[1].OID;
            name = "Non-Scheduled Time";
            if (!AppointmentExists(name, oid))
            {
                description = "No Plan for Line 2";
                startTime = new DateTime(2022, 4, 4, 17, 0, 0);
                endTime = new DateTime(2022, 4, 4, 22, 0, 0);
                type = AppointmentType.Holiday;
                appointment = CreateAppointment(type, oid, name, description, startTime, endTime);
                appointment.CustomState = _factoryCalendar.CustomStates.First(state => state.Value == "61");
                appointment.GroupReason = _factoryCalendar.GroupReasons[0];
                appointment.Recurring = true;
                appointment.Recurrence.RecurrenceType = RecurrencePatternType.Weekly;
                appointment.Recurrence.Weekly.RepeatOnDaysOfWeek =
                    DayOfWeekRecurrence.Wednesday | DayOfWeekRecurrence.WeekendDays;
                SaveAppointment(appointment);
            }

            PrintLine($"{DateTime.Now:T}: Appointments created");
        }
        #endregion

        /// <summary>
        /// Checks if the Appointment already exists.
        /// </summary>
        /// <param name="appointmentName">Name of the appointment.</param>
        /// <param name="isa95Id">The isa95 identifier.</param>
        /// <returns>True if the Appointment already exists</returns>
        bool AppointmentExists(string appointmentName, int isa95Id)
        {
            return _factoryCalendar.CalendarModel.Appointments.Any(a => a.Name == appointmentName && a.Isa95Id == isa95Id);
        }

        /// <summary>
        /// Creates a new appointment without saving it
        /// </summary>
        /// <param name="type">Type of the Appointment</param>
        /// <param name="oid">The OID of the Factory Element the Appointment will be assigned to</param>
        /// <param name="name">Name of the Appointment</param>
        /// <param name="description">Description of the Appointment</param>
        /// <param name="startTime">Start time of the Appointment</param>
        /// <param name="endTime">End time of the Appointment</param>
        /// <returns>A new Appointment that is not saved to Database yet (call SaveAppointment to save)</returns>
        Appointment CreateAppointment(AppointmentType type, int oid, string name, string description, DateTime startTime, DateTime endTime)
        {
            try
            {
                return Appointment.Create(type, oid, name, description, startTime, endTime);
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Saves changes made to the provided appointment
        /// </summary>
        /// <param name="appointment">Appointment to save</param>
        void SaveAppointment(Appointment appointment)
        {
            try
            {
                appointment.SaveChanges();
            }
            catch (Exception ex)
            {
                PrintLine(ex.Message);
            }
        }

        /// <summary>
        /// Removes the Test Site and it's appointments
        /// Needs confirmation from user
        /// </summary>
        private void Cleanup()
        {
            if (_factoryCalendar != null && _fcSite != null)
            {
                if (MessageBox.Show("Would you like to remove the Test Site and it's appointments from FactoryLayout?",
                        "Remove test data", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteTestData();
                    if (Session.CurrentSession.Enterprise.Sites.Contains(_fcSite))
                    {
                        Session.CurrentSession.Enterprise.Sites.Remove(_fcSite);
                        _fcSite = null;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all Appointments assigned to the current Factory Site or it's child elements.
        /// Otherwise the Factory Elements can not be deleted, because they are referenced by the Appointments
        /// </summary>
        private void DeleteTestData()
        {
            if (_fcSite == null) return;;

            PrintLine($"{DateTime.Now:T}: Removing test appointments");

            List<int> factoryElements = GetAllElementIds(_fcSite);
            List<Appointment> appointments = _factoryCalendar.CalendarModel.AppointmentsReadOnly
                .Where(a => factoryElements.Contains(a.Isa95Id)).ToList();
            foreach (var appointment in appointments)
            {
                appointment.Delete();
            }
        }


        /// <summary>
        /// Get the corresponding Factory Calendar Appointment for the provided internal CalendarControlAppointment
        /// </summary>
        /// <param name="appointment">Internal representation of an Appointment for the CalendarControl</param>
        /// <returns>Factory Calendar Appointment</returns>
        private Appointment GetCorrespondingAppointment(ICalendarControlAppointment appointment)
        {
            return _factoryCalendar.CalendarModel.Appointments.FirstOrDefault(ap => ap.Id == appointment.Id);
        }
        /// <summary>
        /// Get the corresponding Factory Calendar Appointment for the provided CalendarItem object
        /// </summary>
        /// <param name="calendarItem">Calendar Item obtained from FactoryCalendarQuery</param>
        /// <returns>Factory Calendar Appointment</returns>
        private Appointment GetCorrespondingAppointment(CalendarItem calendarItem)
        {
            return _factoryCalendar.CalendarModel.Appointments.FirstOrDefault(ap => ap.Id == calendarItem.Id);
        }

        /// <summary>
        /// Select the provided Appointment in the DataGrids in the GUI
        /// </summary>
        /// <param name="appointment">Calendar Item obtained from FactoryCalendarQuery</param>
        private void SelectAppointment(CalendarControlAppointment appointment)
        {
            SelectAppointment(GetCorrespondingAppointment(appointment));
        }
        /// <summary>
        /// Select the provided Appointment in the DataGrids in the GUI.
        /// Selects first CalendarItem referencing this Appointment too.
        /// </summary>
        /// <param name="appointment">Appointment object to select</param>
        private void SelectAppointment(Appointment appointment)
        {
            if (appointment == null) return;

            if (dataGridAppointments.RowCount == 0)
                _selectedAppointment = appointment;
            else if (_selectedAppointment == null || _selectedAppointment.Id != appointment.Id)
            {
                SelectAppointment(appointment.Id);
            }

            if (_selectedCalendarItem == null || _selectedCalendarItem.Id != appointment.Id)
                SelectCalendarItem(appointment);
        }
        /// <summary>
        /// Select the Appointment with the provided ID in the DataGrids in the GUI.
        /// Does not select corresponding CalendarItem.
        /// </summary>
        /// <param name="id">ID of the Appointment to select</param>
        private void SelectAppointment(int id)
        {
            if (id < 0)
            {
                _selectedAppointment = null;
                return;
            }

            if (_selectedAppointment == null || _selectedAppointment.Id != id)
            {
                _selectedAppointment = null;
                dataGridAppointments.ClearSelection();
                foreach (DataGridViewRow row in dataGridAppointments.Rows)
                {
                    var appointmentForDataGrid = row.DataBoundItem as AppointmentForDataGrid;
                    if (appointmentForDataGrid.Id == id)
                    {
                        row.Selected = true;
                        break;
                    }
                    //if ((int)row.Cells[1].Value == id)
                    //{
                    //    row.Selected = true;
                    //    break;
                    //}
                }
            }
        }

        /// <summary>
        /// Select corresponding CalendarItem of provided Appointment.
        /// Selects the corresponding Appointment too.
        /// </summary>
        /// <param name="appointment">CalendarControlAppointment</param>
        private void SelectCalendarItem(CalendarControlAppointment appointment)
        {
            SelectCalendarItem(GetCorrespondingAppointment(appointment));
        }

        /// <summary>
        /// Select first CalendarItem of provided Appointment
        /// Selects the corresponding Appointment too.
        /// </summary>
        /// <param name="appointment">FactoryCalendar Appointment</param>
        private void SelectCalendarItem(Appointment appointment)
        {
            if (appointment == null || _calendarItems == null) return;

            if (_selectedCalendarItem == null || _selectedCalendarItem.Id != appointment.Id)
            {
                SelectCalendarItem(appointment.Id);

                if (_selectedAppointment == null || _selectedAppointment.Id != appointment.Id)
                    SelectAppointment(appointment);
            }
        }
        /// <summary>
        /// Select the first CalendarItem referencing the Appointment ID in the DataGrid in the GUI.
        /// Does not select the Appointment.
        /// </summary>
        /// <param name="appointmentId">ID of the Appointment to select</param>
        private void SelectCalendarItem(int appointmentId)
        {
            if (appointmentId < 0) return;

            if (_selectedCalendarItem == null || _selectedCalendarItem.Id != appointmentId)
            {
                dataGridCalendarItems.ClearSelection();
                foreach (DataGridViewRow row in dataGridCalendarItems.Rows)
                {
                    var calendarItemForDataGrid = row.DataBoundItem as CalendarItemForDataGrid;
                    if (calendarItemForDataGrid.Id == appointmentId)
                    {
                        row.Selected = true;
                        break;
                    }
                    //if ((int)row.Cells[2].Value == appointmentId)
                    //{
                    //    row.Selected = true;
                    //    break;
                    //}
                }
            }
        }

        #endregion

        #region Application events
        /// <summary>
        /// This Method is called after an instance of the FactoryCalendar Proxy has been obtained
        /// </summary>
        protected virtual void OnConnected()
        {
            btnConnect.Enabled = false;
            btnCreateTestData.Enabled = true;
            btnCleanup.Enabled = true;
            btnLoadCalendarItems.Enabled = true;
            btnLoadAppointments.Enabled = true;
            btnNewAppointment.Enabled = true;

            calendarMonthControl.FirstDayOfWeek = _factoryCalendar.CalendarModel.Options.FirstDayOfWeek;

            PrintLine($"{DateTime.Now:T}: Successfully Connected");
            lblConnectionState.Text = "connected";
            _visualAppointmentFactory.FactoryCalendar = _factoryCalendar;

            _factoryCalendar.TransactionCommit += FactoryCalendar_TransactionCommit;
            _factoryCalendar.ListAdded += FactoryCalendar_ListAdded;
            _factoryCalendar.ListRemoved += FactoryCalendar_ListRemoved;
            _factoryCalendar.ListSwapped += FactoryCalendar_ListSwapped;
            _factoryCalendar.ObjectCreated += FactoryCalendar_ObjectCreated;
            _factoryCalendar.ObjectDeleted += FactoryCalendar_ObjectDeleted;
            _factoryCalendar.PropertyUpdate += FactoryCalendar_PropertyUpdate;
            _factoryCalendar.ServiceReconnect += FactoryCalendar_ServiceReconnect;
            _factoryCalendar.ServiceUnreachable += FactoryCalendar_ServiceUnreachable;
            _factoryCalendar.SubscribedChange += FactoryCalendar_SubscribedChange;
            
            if (_isLoadingAppointmentsAutomatically)
            {
                LoadCalendarItems();
            }
        }

        /// <summary>
        /// This Method is called before the instance of the FactoryCalendar Proxy gets disposed
        /// </summary>
        protected virtual void OnDisconnected()
        {
            btnConnect.Enabled = true;
            btnCreateTestData.Enabled = false;
            btnCleanup.Enabled = false;
            btnLoadCalendarItems.Enabled = false;
            btnLoadAppointments.Enabled = false;
            btnNewAppointment.Enabled = false;
            btnEditAppointment.Enabled = false;

            PrintLine($"{DateTime.Now:T}: Disconnected");
            lblConnectionState.Text = "not connected";

            if (_factoryCalendar != null)
            {
                _factoryCalendar.TransactionCommit -= FactoryCalendar_TransactionCommit;
                _factoryCalendar.ListAdded -= FactoryCalendar_ListAdded;
                _factoryCalendar.ListRemoved -= FactoryCalendar_ListRemoved;
                _factoryCalendar.ListSwapped -= FactoryCalendar_ListSwapped;
                _factoryCalendar.ObjectCreated -= FactoryCalendar_ObjectCreated;
                _factoryCalendar.ObjectDeleted -= FactoryCalendar_ObjectDeleted;
                _factoryCalendar.PropertyUpdate -= FactoryCalendar_PropertyUpdate;
                _factoryCalendar.ServiceReconnect -= FactoryCalendar_ServiceReconnect;
                _factoryCalendar.ServiceUnreachable -= FactoryCalendar_ServiceUnreachable;
                _factoryCalendar.SubscribedChange -= FactoryCalendar_SubscribedChange;

                Cleanup();
            }

        }

        /// <summary>
        /// This Method is called when new CalendarItems were loaded
        /// </summary>
        protected virtual void OnCalendarItemsChanged()
        {
            PrintLine($"{DateTime.Now:T}: CalendarItems updated");

            int id = _selectedAppointment?.Id ?? -1;
            //Update List
            //dataGridCalendarItems.Rows.Clear();
            //foreach (var item in _calendarItems)
            //{
            //    object[] row = {
            //        item.Isa95Object?.Path,
            //        item.Isa95Id,
            //        item.Id,
            //        item.Name,
            //        item.Description,
            //        _appointmentTypeMapper.GetNameOrNull(item.AppointmentType),
            //        item.StartTime,
            //        item.EndTime,
            //        item.Recurring
            //    };
            //    dataGridCalendarItems.Rows.Add(row);
            //}
            var calendarItemsForDataGrid = new List<CalendarItemForDataGrid>();
            foreach (var item in _calendarItems)
            {
                calendarItemsForDataGrid.Add(new CalendarItemForDataGrid(item, _appointmentTypeMapper));
            }
            dataGridCalendarItems.AutoGenerateColumns = false;
            dataGridCalendarItems.DataSource = calendarItemsForDataGrid;


            //Update CalendarControl
            _internalAppointments.Clear();
            foreach (CalendarItem calendarItem in _calendarItems)
            {
                CalendarControlAppointment appointment = new CalendarControlAppointment()
                {
                    Id = calendarItem.Id,
                    Name = calendarItem.Name,
                    Description = calendarItem.Description,
                    StartTime = calendarItem.LocalStartTime,
                    EndTime = calendarItem.LocalEndTime
                };
                _internalAppointments.Add(appointment);
            }
            AppointmentsChanged?.Invoke(this, EventArgs.Empty);

            if (id > 0) SelectCalendarItem(id);
        }

        /// <summary>
        /// This Method is called when a Calender Item was selected
        /// </summary>
        protected virtual void OnSelectedCalendarItemChanged()
        {
            btnEditAppointment.Enabled = btnDeleteAppointment.Enabled = _selectedAppointment != null || _selectedCalendarItem != null;

            if (!_updatingSelections)
            {
                _updatingSelections = true;
                if (_selectedCalendarItem != null && (_selectedAppointment == null || _selectedAppointment.Id != _selectedCalendarItem.Id))
                    SelectAppointment(_selectedCalendarItem.Id);
                _updatingSelections = false;
            }
        }
        /// <summary>
        /// This Method is called when an Appointment was selected
        /// </summary>
        protected virtual void OnSelectedAppointmentChanged()
        {
            btnEditAppointment.Enabled = btnDeleteAppointment.Enabled = _selectedAppointment != null || _selectedCalendarItem != null;

            if (!_updatingSelections)
            {
                _updatingSelections = true;
                if (_selectedAppointment != null && (_selectedCalendarItem == null || _selectedCalendarItem.Id != _selectedAppointment.Id))
                    SelectCalendarItem(_selectedAppointment.Id);
                _updatingSelections = false;
            }
        }

        #endregion
        

        #region Print Methods
        /// <summary>
        /// Adds provided text to the Output
        /// </summary>
        /// <param name="text">Text to print in the output</param>
        private void Print(string text)
        {
            textOutput.AppendText(text ?? string.Empty);
        }
        /// <summary>
        /// Adds provided text to the Output as new line
        /// </summary>
        /// <param name="text">Text to print in the output</param>
        private void PrintLine(string text = "")
        {
            Print(text + Environment.NewLine);
        }
        /// <summary>
        /// Prints the textual description of the provided FactoryCalendar Proxy to the output.
        /// Replaces commas by new lines for a better readability
        /// </summary>
        /// <param name="fc">Factory Calendar Proxy</param>
        private void Print(FactoryCalendar fc)
        {
            string text = fc.ToString().Replace(", ", "\n");
            PrintLine(text);
        }

        private void Print(Appointment appointment, string indent = "")
        {
            string text = indent + appointment.ToString().Replace(", ", "\n" + indent);

            PrintLine(text);
            if (appointment.Recurring)
            {
                indent += "\t";
                PrintLine(indent + "Recurrence:");
                indent += "\t";
                Print(appointment.Recurrence, indent);
            }

            PrintLine();
        }

        private void Print(AppointmentRecurrence recurrence, string indent = "")
        {
            string text = indent + recurrence.ToString().Replace(", ", "\n" + indent);
            PrintLine(text);
        }

        private string AppointmentToString(Appointment appointment, string indent = "")
        {
            StringBuilder sb = new StringBuilder(indent);
            sb.Append(appointment.Name).AppendLine($"(ID:{appointment.Id})");
            indent += "\t";
            sb.Append(indent).Append("Description: ").AppendLine(appointment.Description)
                .Append(indent).Append("Type: ").AppendLine(appointment.AppointmentType.ToString())
                .Append(indent).Append("Start: ").AppendLine(DateTimeToString(appointment.StartTime))
                .Append(indent).Append("End: ").AppendLine(DateTimeToString(appointment.EndTime));
            if (appointment.Isa95Id > 0)
                sb.Append(indent).Append("Object: ").AppendLine(appointment.Isa95Id.ToString());
            //sb.Append(indent).Append("Object: ").AppendLine(cmProxy.GetObject(appointment.Isa95Id)?.Path);
            if (appointment.Recurring)
            {
                sb.Append(indent).AppendLine("Recurrence: ")
                    .Append(RecurrenceToString(appointment.Recurrence, indent + "\t"));
            }

            return sb.ToString();
        }


        private void Print(ObjectCreatedArgs args, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent)
                .Append("CreatedObject: ")
                .Append(args.CreatedObject.ToString().Replace(", ", "\n" + indent));
            sb.AppendLine("ObjectType: ")
                .Append(args.ObjectType);
            PrintLine(sb.ToString());
        }
        private void Print(PropertyUpdateArgs args)
        {
            PrintLine("PropertyUpdateArgs");
            PrintLine($"\tType: {args.Type}");
            PrintLine($"\tObjectType: {args.ObjectType}");
            PrintLine($"\tPID: {args.PID}");
            PrintLine($"\tOldValue: {args.OldValue}");
            PrintLine($"\tNewValue: {args.NewValue}");
            PrintLine($"\tParentObject: {args.ParentObject}");
        }
        private void Print(QueryDataChangedArgs args)
        {
            PrintLine("Subscription: ");
            PrintLine($"\tFactory Element ID: {args.Subscription.FactoryLayoutElementId}");
            PrintLine($"\tStart Time: {args.Subscription.StartTime}");
            PrintLine($"\tEnd Time: {args.Subscription.StartTime}");
            PrintLine($"\tInclude inheritance: {args.Subscription.IncludeInheritance}");
            PrintLine($"\tChanged Calendar Items:");
            foreach (var changeSet in args.UpdatedCalendarItems)
            {
                PrintLine($"\t\tItem/Key: {changeSet.Key}");
                PrintLine($"\t\t{changeSet.Value.Count} Changes");
                foreach (var changedItem in changeSet.Value)
                {
                    PrintLine($"\t\tChange: {changedItem}");
                }
            }
        }

        private string RecurrenceToString(AppointmentRecurrence recurrence, string indent = "")
        {
            StringBuilder sb = new StringBuilder(indent);
            sb.Append("StartDate: ")
                .AppendLine(recurrence.RecurrenceStartDate.ToLocalTime().ToShortDateString())
                .Append(indent).Append("EndDate: ")
                .AppendLine(recurrence.RangeEndDate.ToLocalTime().ToShortDateString())
                .Append(indent).Append("Occurrences: ").AppendLine(recurrence.RangeNumberOfOccurrences.ToString())
                .Append(indent).Append("Type: ").AppendLine(recurrence.RecurrenceType.ToString());

            switch (recurrence.RecurrenceType)
            {
                case RecurrencePatternType.Daily:
                    sb.Append(indent).Append("Interval: ").AppendLine(recurrence.Daily.RepeatInterval.ToString());
                    sb.Append(indent).Append("DaysOfWeek: ").AppendLine(recurrence.Daily.RepeatOnDaysOfWeek.ToString());
                    break;
                case RecurrencePatternType.Weekly:
                    sb.Append(indent).Append("Interval: ").AppendLine(recurrence.Weekly.RepeatInterval.ToString());
                    sb.Append(indent).Append("DaysOfWeek: ")
                        .AppendLine(recurrence.Weekly.RepeatOnDaysOfWeek.ToString());
                    break;
                case RecurrencePatternType.Monthly:
                    sb.Append(indent).Append("RepeatInterval: ")
                        .AppendLine(recurrence.Monthly.RepeatInterval.ToString());
                    sb.Append(indent).Append("Repeat on day of month: ")
                        .AppendLine(recurrence.Monthly.RepeatOnDayOfMonth.ToString());
                    sb.Append(indent).Append("Repeat on rel. day of month: ")
                        .AppendLine(recurrence.Monthly.RepeatOnRelativeDayInMonth.ToString());
                    sb.Append(indent).Append("Rel. day of week: ")
                        .AppendLine(recurrence.Monthly.RelativeDayOfWeek.ToString());
                    break;
                case RecurrencePatternType.Yearly:
                    sb.Append(indent).Append("Repeat on rel. day in month: ")
                        .AppendLine(recurrence.Yearly.RepeatOnRelativeDayInMonth.ToString());
                    sb.Append(indent).Append("Rel. day of week: ")
                        .AppendLine(recurrence.Yearly.RelativeDayOfWeek.ToString());
                    sb.Append(indent).Append("Repeat on day of month: ")
                        .AppendLine(recurrence.Yearly.RepeatOnDayOfMonth.ToString());
                    sb.Append(indent).Append("Repeat on month: ")
                        .AppendLine(recurrence.Yearly.RepeatOnMonth.ToString());
                    break;
            }

            return sb.ToString();
        }

        private string DateTimeToString(DateTime dt)
        {
            return dt.ToLocalTime().ToShortDateString() + " " + dt.ToLocalTime().ToShortTimeString();
        }

        #endregion

        #region DOC_FACTORY_CALENDAR_EVENTS

        private void FactoryCalendar_SubscribedChange(object sender, QueryDataChangedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_SubscribedChange");
            Print(args);
        }

        private void FactoryCalendar_ServiceUnreachable(string strServiceEndpoint, string strComment)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ServiceUnreachable");
            lblConnectionState.Text = "not connected, service unreachable";
        }

        private void FactoryCalendar_ServiceReconnect(string strServiceEndpoint, string strComment)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ServiceReconnect");
            lblConnectionState.Text = "connected";
        }

        private void FactoryCalendar_PropertyUpdate(object sender, PropertyUpdateArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_PropertyUpdate");
            Print(args);

            //Calendar options changed
            if (args.ObjectType == ObjectType.CalendarOptions)
            {
                //First day of week changed
                if (args.PID == PropertyIds.CalendarOptions_FirstDayOfWeek)
                    calendarMonthControl.FirstDayOfWeek = _factoryCalendar.CalendarModel.Options.FirstDayOfWeek;
            }
        }

        private void FactoryCalendar_ObjectDeleted(object sender, ObjectDeletedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ObjectDeleted");
        }

        private void FactoryCalendar_ObjectCreated(object sender, ObjectCreatedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ObjectCreated");
            Print(args);
        }

        private void FactoryCalendar_ListSwapped(object sender, ListSwappedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ListSwapped");
        }

        private void FactoryCalendar_ListRemoved(object sender, ListRemovedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ListRemoved");
        }

        private void FactoryCalendar_ListAdded(object sender, ListAddedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_ListAdded");
        }

        private void FactoryCalendar_TransactionCommit(object sender, TransactionCommittedArgs args)
        {
            PrintLine($"{DateTime.Now:T}: FactoryCalendar_TransactionCommit");
        }

        #endregion

        #region CalendarControl Events

        private void Calendar_DaySelected(object sender, DaySelectedEventArgs e)
        {
            int count = _internalAppointments.Count(a => IsDateInRange(e.Date, a.StartTime, a.EndTime));
            PrintLine($"Day selected: {e.Date} ({count}) Appointments");
        }

        private void CalendarMonthControl_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            PrintLine($"Month changed: {e.FirstDayOfMonth},  FirstVisibleDate: {e.FirstVisibleDate }, LastVisibleDate: {e.LastVisibleDate }");
            dtLoadFrom.Value = e.FirstVisibleDate;
            dtLoadTo.Value = e.LastVisibleDate;
            if (_isLoadingAppointmentsAutomatically && _factoryCalendar != null)
            {
                int? oid = cbFactoryElement.SelectedIndex > 0 ? (int?)cbFactoryElement.SelectedValue : null;
                LoadCalendarItems(e.FirstVisibleDate, e.LastVisibleDate, oid, cbIncludeInheritance.Checked);
            }
        }

        private static bool IsDateInRange(DateTime date, DateTime startTime, DateTime endTime)
        {
            return startTime.Date == date.Date || endTime.Date == date.Date ||
                   (startTime.Date < date.Date && endTime.Date > date.Date);
        }

        private void Calendar_AppointmentSelected(object sender, AppointmentSelectedEventArgs e)
        {
            PrintLine("Appointment Selected: " + e.Appointment);
            PrintLine(GetCorrespondingAppointment(e.Appointment).ToString());
            // MessageBox.Show(e.Appointment.ToString());

            SelectCalendarItem((CalendarControlAppointment)e.Appointment);
        }


        #endregion

        #region IAppointmentsProvider

        /// <summary>
        /// Provides the Appointment representation for the CalendarControl
        /// This is a method of the IAppointmentsProvider interface for the internal CalendarControl
        /// </summary>
        /// <returns>Enumerable collection of ICalendarControlAppointment objects representing the Appointments</returns>
        public IEnumerable<ICalendarControlAppointment> GetAppointments()
        {
            //List<CalendarControlAppointment> appointments = new List<CalendarControlAppointment>();

            //FactoryCalendarQuery fcQuery = FactoryCalendarQuery.GetInstance();

            //DateTime currentDate = calendarMonthControl.CurrentDate.Date;
            //DateTime firstDay = currentDate.AddDays(-currentDate.Day + 1);

            //CalendarItem[] calendarItems = fcQuery.GetCalendarItems(firstDay, firstDay.AddMonths(1));
            //foreach (CalendarItem calendarItem in calendarItems)
            //{
            //    CalendarControlAppointment appointment = new CalendarControlAppointment()
            //    {
            //        Id = calendarItem.Id,
            //        Name = calendarItem.Name,
            //        Description = calendarItem.Description,
            //        StartTime = calendarItem.LocalStartTime,
            //        EndTime = calendarItem.LocalEndTime
            //    };
            //    appointments.Add(appointment);
            //}

            //if (calendarModel != null)
            //{
            //    foreach (var fcAppointment in calendarModel.Appointments)
            //    {
            //        CalendarControlAppointment appointment = new CalendarControlAppointment()
            //        {
            //            Id = fcAppointment.Id,
            //            Name = fcAppointment.Name,
            //            Description = fcAppointment.Description,
            //            StartTime = fcAppointment.LocalStartTime,
            //            EndTime = fcAppointment.LocalEndTime
            //        };
            //        appointments.Add(appointment);
            //    }
            //}
            //return appointments;
            return _internalAppointments;
        }

        /// <summary>
        /// Provides the Appointment representation for the CalendarControl
        /// This is a method of the IAppointmentsProvider interface for the internal CalendarControl
        /// </summary>
        /// <returns>ICalendarControlAppointment object representing the Appointments</returns>
        public ICalendarControlAppointment CreateAppointment(DateTime date, string name)
        {
            Appointment appointment = ShowNewAppointmentForm(name, date);
            if (appointment != null)
            {
                CalendarControlAppointment calendarControlAppointment = new CalendarControlAppointment()
                {
                    Id = appointment.Id,
                    Name = appointment.Name,
                    Description = appointment.Description,
                    StartTime = appointment.LocalStartTime,
                    EndTime = appointment.LocalEndTime
                };
                return calendarControlAppointment;
            }
            return null;
        }


        #endregion

        #region CalendarControl Style

        /// <summary>
        /// This an optional class is for the internal CalendarControl only.
        /// It allows you to provide own DayOfMonthControls or customize the predefined.
        /// </summary>
        private class CustomDayOfMonthControlFactory : IDayOfMonthControlFactory
        {
            public void UpdateDayOfMonthControl(IDayOfMonth day, DateTime dt, bool isCurrentMonth)
            {
                day.Date = dt;
                ((Control)day).BackColor = isCurrentMonth ? Color.White : Color.LightGray;
            }

            public IDayOfMonth CreateDayOfMonthControl(DateTime dt)
            {
                var day = new DayOfMonthControl();
                day.Date = dt;
                day.Dock = DockStyle.Fill;
                day.BackColor = Color.White;
                day.FocusedColor = Color.Moccasin;
                day.MouseOverColor = Color.LightYellow;
                day.BorderStyle = BorderStyle.FixedSingle;
                return day;
            }

            public Control GetControl(IDayOfMonth day)
            {
                return (Control)day;
            }
        }

        /// <summary>
        /// This class is for the internal CalendarControl only.
        /// Because the CalendarControl does not know FactoryCalendar Appointments and AppointmentTypes
        /// this factory class creates the visual representation of the Appointments
        /// </summary>
        private class CustomVisualAppointmentFactory : IVisualAppointmentFactory
        {
            public FactoryCalendar FactoryCalendar { get; set; }

            public CustomVisualAppointmentFactory()
            {
            }

            public Control CreateVisualAppointment(ICalendarControlAppointment appointment, Control container, DateTime date)
            {

                int y = 0;
                foreach (Control control in container.Controls)
                {
                    if (control.Location.Y + control.Size.Height > y) y = control.Location.Y + control.Size.Height;
                }

                Label lbl = new Label();
                lbl.Location = new Point(2, y + 2);
                lbl.MaximumSize = new Size(container.Width, 0);
                lbl.AutoSize = true;
                lbl.Padding = new Padding(2);

                lbl.Text = appointment.Name;

                ToolTip toolTip = new ToolTip();
                string text = appointment.Name;
                if (date != appointment.StartTime.Date)
                    text = $"00:00 - {appointment.EndTime.ToString("HH:mm")}";
                else if (date != appointment.EndTime.Date)
                    text = $"{appointment.StartTime.ToString("HH:mm")} - 00:00";
                else
                    text = $"{appointment.StartTime.ToString("HH:mm")} - {appointment.EndTime.ToString("HH:mm")}";
                text += Environment.NewLine + appointment.Name;
                if (!string.IsNullOrWhiteSpace(appointment.Description))
                    text += Environment.NewLine + appointment.Description;
                toolTip.SetToolTip(lbl, text);
                
                lbl.BackColor = GetColor(appointment);
                lbl.MouseEnter += Lbl_MouseEnter;
                lbl.MouseLeave += Lbl_MouseLeave;

                lbl.Tag = appointment;
                return lbl;
            }

            private Color GetColor(ICalendarControlAppointment appointment)
            {
                AppointmentType type = AppointmentType.Undefined;
                if (FactoryCalendar != null)
                {
                    Appointment fcAppointment = GetCorrespondingAppointment(appointment);
                    if (fcAppointment != null)
                        type = fcAppointment.AppointmentType;
                }
                return GetColor(type);
            }
            private static Color GetColor(AppointmentType type)
            {
                switch (type)
                {
                    case AppointmentType.Shift:
                        return Color.Aqua;
                    case AppointmentType.ScheduledDowntime:
                        return Color.Gold;
                    case AppointmentType.Holiday:
                        return Color.Gray;
                    case AppointmentType.InheritanceBlocker:
                        return Color.MediumPurple;
                    case AppointmentType.Undefined:
                    default:
                        return Color.Red;
                }
            }

            private Appointment GetCorrespondingAppointment(ICalendarControlAppointment appointment)
            {
                return FactoryCalendar.CalendarModel.Appointments.FirstOrDefault(ap => ap.Id == appointment.Id);
            }

            private void Lbl_MouseLeave(object sender, EventArgs e)
            {
                Control appointment = (Control)sender;
                appointment.BackColor = GetColor((ICalendarControlAppointment)appointment.Tag);
            }

            private void Lbl_MouseEnter(object sender, EventArgs e)
            {
                Control appointment = (Control)sender;
                appointment.BackColor = Highlight(appointment.BackColor, 100);
            }

            private static Color Highlight(Color color, int by)
            {
                if (by == 0)
                    return color;
                
                Color result = Color.FromArgb(Highlight(color.A, -by), Highlight(color.R, by), Highlight(color.G, by), Highlight(color.B, by));
                return result;
            }
            private static int Highlight(int color, int by)
            {
                return Math.Max(0, Math.Min(color + by, 255));
            }

            public void DisposeVisualAppointment(Control appointment)
            {
                appointment.MouseEnter -= Lbl_MouseEnter;
                appointment.MouseLeave -= Lbl_MouseLeave;
            }
        }

        #endregion

    }

}
