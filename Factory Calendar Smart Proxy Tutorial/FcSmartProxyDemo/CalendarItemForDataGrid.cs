using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Objects;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Types;

namespace FcSmartProxyDemo
{
    internal class CalendarItemForDataGrid
    {
        [DisplayName("Factory Element")]
        public string Path { get; set; }

        [DisplayName("Factory Element ID")]
        public int Isa95Id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Type")]
        public string AppointmentType { get; set; }

        [DisplayName("Start")]
        public DateTime StartTime { get; set; }

        [DisplayName("End")]
        public DateTime EndTime { get; set; }
        public bool Recurring { get; set; }

        public CalendarItemForDataGrid(CalendarItem item, EnumMapper<AppointmentType> appointmentTypeMapper)
        {
            Path = item.Isa95Object?.Path;
            Isa95Id = item.Isa95Id;
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            AppointmentType = appointmentTypeMapper.GetNameOrNull(item.AppointmentType);
            StartTime = item.LocalStartTime;
            EndTime = item.LocalEndTime;
            Recurring = item.Recurring;
        }




    }
}
