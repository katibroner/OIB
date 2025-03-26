using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asm.As.Oib.ConfigurationManager.Proxy.Architecture.Objects;
using Asm.As.Oib.ConfigurationManager.Proxy.Business.Objects;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Types;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects;

namespace FcSmartProxyDemo
{
    public partial class NewAppointmentForm : Form
    {
        private IEnumMapper<AppointmentType> _appointmentTypeMapper;
        public Appointment Appointment { get; private set; }

        public NewAppointmentForm(IEnumMapper<AppointmentType> appointmentTypeMapper, List<Isa95Base> factoryElements) 
            : this(appointmentTypeMapper, factoryElements, "New Appointment", DateTime.Now)
        {
        }
        public NewAppointmentForm(IEnumMapper<AppointmentType> appointmentTypeMapper, List<Isa95Base> factoryElements, string name, DateTime date)
        {
            _appointmentTypeMapper = appointmentTypeMapper;

            InitializeComponent();
            tbName.Text = name;
            dtStartDate.Value = dtStartTime.Value = date;
            dtEndDate.Value = dtEndTime.Value = dtStartDate.Value.AddHours(1);

            cbType.Items.Clear();
            cbType.Items.AddRange(_appointmentTypeMapper.GetNames().ToArray<object>());
            cbType.SelectedItem = _appointmentTypeMapper.GetNameOrNull(AppointmentType.Shift);

            SetFactoryElements(factoryElements);
        }

        private void SetFactoryElements(List<Isa95Base> elements)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OID", typeof(int));
            dataTable.Columns.Add("Path", typeof(string));
            
            foreach (var element in elements)
            {
                DataRow row = dataTable.NewRow();
                row["OID"] = element.OID;
                row["Path"] = element.Path;
                dataTable.Rows.Add(row);
            }

            cbParent.DataSource = dataTable.DefaultView;
            cbParent.DisplayMember = "Path";
            cbParent.ValueMember = "OID";
            cbParent.SelectedIndex = 0;
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppointmentType appointmentType;
            if (_appointmentTypeMapper.TryParse(cbType.SelectedItem.ToString(), out appointmentType))
            {
                if (appointmentType == AppointmentType.ScheduledDowntime)
                {
                    LoadSubStates(40, 49);
                    cbState.Enabled = cbReason.Enabled = true;
                }
                else if(appointmentType == AppointmentType.Holiday)
                {
                    LoadSubStates(60, 69);
                    cbState.Enabled = cbReason.Enabled = true;
                }
                else
                {
                    cbState.Enabled = cbReason.Enabled = false;
                }
            }

        }

        private void LoadSubStates(int minValue, int maxValue)
        {
            FactoryCalendar fc = FactoryCalendar.GetInstance();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            
            var states = fc.CustomStates.AsEnumerable().Where(s =>
            {
                int value;
                if (int.TryParse(s.Value, out value))
                    return minValue <= value && value <= maxValue;
                return false;
            }).ToList();

            foreach (var customState in states)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = customState.Id;
                row["Name"] = $"{customState.Value} - {customState.Name}";
                dataTable.Rows.Add(row);
            }

            cbState.DataSource = dataTable.DefaultView;
            cbState.DisplayMember = "Name";
            cbState.ValueMember = "Id";
            cbState.SelectedIndex = 0;

            LoadGroupReasons(states.FirstOrDefault());
        }
        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbState.SelectedIndex < 0 || !(cbState.SelectedValue is int))
                return;

            int stateId = (int)cbState.SelectedValue;
            FactoryCalendar fc = FactoryCalendar.GetInstance();
            var state = fc.CustomStates.AsEnumerable().FirstOrDefault(s => s.Id == stateId);
            LoadGroupReasons(state);
        }

        private void LoadGroupReasons(CustomState state)
        {
            if (state == null)
            {
                cbReason.Items.Clear();
                return;
            }

            FactoryCalendar fc = FactoryCalendar.GetInstance();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));

            var stateReasons = fc.GroupReasons.AsEnumerable().Where(r => r.CustomStateId == state.Id);

            foreach (var reason in stateReasons)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = reason.Id;
                row["Name"] = $"{reason.Value} - {reason.Name}";
                dataTable.Rows.Add(row);
            }

            cbReason.DataSource = dataTable.DefaultView;
            cbReason.DisplayMember = "Name";
            cbReason.ValueMember = "Id";
            cbReason.SelectedIndex = 0;
        }

        #region DOC_CREATE_APPOINTMENT
        private void btnSave_Click(object sender, EventArgs e)
        {
            int oid = 1001;
            if (cbParent.SelectedIndex >= 0)
                oid = (int)cbParent.SelectedValue;
            AppointmentType appointmentType;
            if (!_appointmentTypeMapper.TryParse(cbType.SelectedItem.ToString(), out appointmentType))
            {
                MessageBox.Show("Appointment Type not supported!");
                DialogResult = DialogResult.Retry;
                return;
            }
            
            DateTime endTime = dtEndDate.Value.Date + dtEndTime.Value.TimeOfDay;
            DateTime startTime = dtStartDate.Value.Date + dtStartTime.Value.TimeOfDay;
            
            try
            {
                Appointment = Appointment.Create(appointmentType, oid, tbName.Text, tbDescription.Text, startTime, endTime);
                
                if (appointmentType != AppointmentType.Shift)
                {
                    int stateId = (int)cbState.SelectedValue;
                    FactoryCalendar fc = FactoryCalendar.GetInstance();
                    var state = fc.CustomStates.AsEnumerable().FirstOrDefault(s => s.Id == stateId);
                    if (state != null)
                        Appointment.CustomState = state;

                    int reasonId = (int)cbReason.SelectedValue;
                    var reason = fc.GroupReasons.AsEnumerable().FirstOrDefault(r => r.Id == reasonId);
                    if (reason != null)
                        Appointment.GroupReason = reason;
                }
                Appointment.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Save_Click: Got exception: {ex}");
                DialogResult = DialogResult.No;
            }
        }
        #endregion

    }
}
