#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ServiceModel;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;

#endregion

namespace FeederExternalControlTutorial
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FeederExternalControlServiceImplementation : ISiplaceFeederExternalControl
    {
        #region Fields

        private readonly Form1 _form;

        #endregion

        #region Constructors

        public FeederExternalControlServiceImplementation(Form1 form)
        {
            _form = form;
        }

        #endregion

        #region ISiplaceFeederExternalControl Members
#region DOC_Ping
        /// <summary>
        /// Called for checks if this service is alive.
        /// </summary>
        /// <returns></returns>
        public PingResponse FeederExternalControlPing(PingRequest pingRequest)
        {
            _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");
            return new PingResponse();
        }
        #endregion

        #region DOC_GetFeederMaintenanceStatus

        public GetFeederMaintenanceStatusResponse GetFeederMaintenanceStatus(GetFeederMaintenanceStatusRequest request)
        {
            try
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received: Feeder locations: " + request);

                var feederMaintenanceStatusInfos = new List<FeederMaintenanceStatusInfo>();

                // Loop through all Feeder Locations and see if we have an entry in our data grid for one of the feeder ids
                foreach (FeederLocation feederLocation in request.FeederLocations)
                {
                    foreach (DataRow row in _form.FeederResultStatiTable.Rows)
                    {
                        if (feederLocation.FeederId.Equals(row[Form1.ColumnFeederId]))
                        {
                            int resultState;
                            int.TryParse(row[Form1.ColumnResultState].ToString(), out resultState);

                            // If yes, then create a FeederMaintenanceStatusInfo and set the Reason and ResultState according to the values in the grid
                            var feederMaintenanceStatusInfo = new FeederMaintenanceStatusInfo
                            {
                                // Copy over all values from the FeederLocation
                                DockingStation = feederLocation.DockingStation,
                                Location = feederLocation.Location,
                                Machine = feederLocation.Machine,
                                TableId = feederLocation.TableId,
                                TableState = feederLocation.TableState,
                                Device = feederLocation.Device,
                                FeederId = feederLocation.FeederId,
                                FeederType = feederLocation.FeederType,
                                Track = feederLocation.Track,
                                FeederTypeSpiOidLong = feederLocation.FeederTypeSpiOidLong,
                                Divisions = feederLocation.Divisions,
                                // and fill the rest from the grid
                                ResultState = resultState,
                                Reason = row[Form1.ColumnReason].ToString()
                            };
                            feederMaintenanceStatusInfos.Add(feederMaintenanceStatusInfo);
                        }
                    }
                }

                // Now copy over the FeederMaintenanceStatusInfos to the response object
                var verifyToolResponse = new GetFeederMaintenanceStatusResponse
                {
                    FeederMaintenanceStatusInfos = feederMaintenanceStatusInfos.ToArray()
                };

                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " finished: FeederMaintenanceStatusInfos: " + verifyToolResponse);

                return verifyToolResponse;
            }
            catch (Exception ex)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem!");
                var siplaceSetupCenterFault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = MethodBase.GetCurrentMethod().Name +
                                      " failed, on an unknown problem! Exception: " + ex
                };
                var faultReason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(siplaceSetupCenterFault, faultReason);
            }
        }
        #endregion
        #endregion
    }
}