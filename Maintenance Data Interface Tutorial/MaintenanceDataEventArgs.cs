using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using www.siplace.com.OIB._2019._11.MaintenanceData.Contracts.Service;
using www.siplace.com.OIB._2019_11.MaintenanceData.Contracts.Data;

namespace MaintenanceDataTutorial
{
    internal class MaintenanceDataEventArgs
    {
        public EquipmentConfigurationEventData EquipmentConfigurationEventData { get; }
        public SetupConfigurationEventData SetupConfigurationEventData { get; }
        public StationServiceAndMaintenanceEventData StationServiceAndMaintenanceEventData { get; }
        public string MsgId { get; }
        public DateTime SendTimeStamp { get; }
        public string Isa95Path { get; }

        internal MaintenanceDataEventArgs(MaintenanceDataRequest request)
        {
            EquipmentConfigurationEventData = request.EquipmentConfigurationEventData;
            SetupConfigurationEventData = request.SetupConfigurationEventData;
            StationServiceAndMaintenanceEventData = request.StationServiceAndMaintenanceEventData;

            MsgId = request.MsgId;
            SendTimeStamp = request.SendTimeStamp;
            Isa95Path = request.Isa95Path;
        }
    }
}
