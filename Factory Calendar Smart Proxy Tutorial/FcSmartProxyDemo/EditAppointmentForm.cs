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
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Types;

namespace FcSmartProxyDemo
{
    public partial class EditAppointmentForm : Form
    {
        private readonly IEnumMapper<AppointmentType> _appointmentTypeMapper;

        private Appointment _appointment;
        public Appointment Appointment
        {
            get { return _appointment; }
            set
            {
                _appointment = value;
                OnAppointmentSet();
            }
        }


        public EditAppointmentForm(IEnumMapper<AppointmentType> appointmentTypeMapper)
        {
            _appointmentTypeMapper = appointmentTypeMapper;
            InitializeComponent();
            cbType.Items.Clear();
            cbType.Items.AddRange(_appointmentTypeMapper.GetNames().ToArray<object>());
        }
        private void OnAppointmentSet()
        {
            if(Appointment == null)
                return;
            
            tbId.Text = Appointment.Id.ToString();
            tbName.Text = Appointment.Name;
            tbDescription.Text = Appointment.Description;
            dtStartDate.Value = dtStartTime.Value = Appointment.StartTime;
            dtEndDate.Value = dtEndTime.Value = Appointment.EndTime;

            string appointmentType = _appointmentTypeMapper.GetNameOrNull(Appointment.AppointmentType);
            if (appointmentType != null)
                cbType.SelectedItem = appointmentType;
            //UpdateSubStates(Appointment.AppointmentType);
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppointmentType appointmentType;
            if (_appointmentTypeMapper.TryParse(cbType.SelectedItem.ToString(), out appointmentType))
            {
                if(Appointment.CustomState != null)
                    UpdateSubStates(appointmentType, Appointment.CustomState.Id);
                else
                    UpdateSubStates(appointmentType);
            }
        }
        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbState.SelectedIndex < 0 || !(cbState.SelectedValue is int))
                return;

            int stateId = (int)cbState.SelectedValue;
            FactoryCalendar fc = FactoryCalendar.GetInstance();
            var state = fc.CustomStates.AsEnumerable().FirstOrDefault(s => s.Id == stateId);
            if(state != null && state.Id == Appointment.CustomState.Id)
                LoadGroupReasons(state, Appointment.GroupReason.Id);
            else
                LoadGroupReasons(state);
        }

        #region DOC_MODIFY_APPOINTMENT
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Appointment != null)
            {
                try
                {
                    Appointment.Name = tbName.Text;
                    Appointment.Description = tbDescription.Text;
                    Appointment.StartTime = dtStartDate.Value.Date + dtStartTime.Value.TimeOfDay;
                    Appointment.EndTime = dtEndDate.Value.Date + dtEndTime.Value.TimeOfDay;
                    AppointmentType appointmentType;
                    if (_appointmentTypeMapper.TryParse(cbType.SelectedItem.ToString(), out appointmentType))
                    {
                        Appointment.AppointmentType = appointmentType;

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
                    }

                    Appointment.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"Save_Click: Got exception: {ex}");
                    DialogResult = DialogResult.No;
                }
            }
        }
        #endregion

        private void UpdateSubStates(AppointmentType appointmentType, int selectedId = -1)
        {
            if (appointmentType == AppointmentType.ScheduledDowntime)
            {
                LoadSubStates(40, 49, selectedId);
                cbState.Enabled = cbReason.Enabled = true;
            }
            else if (appointmentType == AppointmentType.Holiday)
            {
                LoadSubStates(60, 69, selectedId);
                cbState.Enabled = cbReason.Enabled = true;
            }
            else
            {
                cbState.Enabled = cbReason.Enabled = false;
            }
        }
        private void LoadSubStates(int minValue, int maxValue, int selectedId = -1)
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
            });

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
            if (selectedId >= 0)
                cbState.SelectedValue = selectedId;
            else
                cbState.SelectedIndex = 0;

            LoadGroupReasons(states.FirstOrDefault());
        }
        private void LoadGroupReasons(CustomState state, int selectedId = -1)
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
            if (selectedId >= 0)
                cbReason.SelectedValue = selectedId;
            else
                cbReason.SelectedIndex = 0;
        }


    }
}
