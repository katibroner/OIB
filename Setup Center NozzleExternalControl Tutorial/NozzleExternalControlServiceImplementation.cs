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

namespace NozzleExternalControlTutorial
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NozzleExternalControlServiceImplementation : INozzleExternalControl
    {
        #region Fields

        private readonly Form1 _form;

        #endregion

        #region Constructors

        public NozzleExternalControlServiceImplementation(Form1 form)
        {
            _form = form;
        }

        #endregion

        #region INozzleExternalControl Members

        #region DOC_NozzlePing
        /// <summary>
        /// Called for checks if this service is alive.
        /// </summary>
        /// <returns></returns>
        public PingResponse NozzlePing(PingRequest pingRequest)
        {
            _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");
            return new PingResponse();
        }
        #endregion

        #region DOC_GetNozzleMaintenanceStatus

        public GetNozzleMaintenanceStatusResponse GetNozzleMaintenanceStatus(GetNozzleMaintenanceStatusRequest request)
        {
            try
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received: Nozzle locations: " + request);

                var nozzleResponses = new List<NozzleState>();

                // Loop through all Nozzle Locations and see if we have an entry in our data grid for one of the Nozzle ids
                foreach (Nozzle nozzle in request.NozzlesRequest)
                {
                    foreach (DataRow row in _form.NozzleResultStatiTable.Rows)
                    {
                        if (nozzle.Id.Equals(row[Form1.ColumnNozzleId]))
                        {
                            int.TryParse(row[Form1.ColumnResultState].ToString(), out var resultState);

                            // If yes, then create a NozzleMaintenanceStatusInfo and set the Reason and ResultState according to the values in the grid
                            var nozzleState = new NozzleState
                            {
                                Id = nozzle.Id,
                                // and fill the rest from the grid
                                Status = resultState,
                                Message = row[Form1.ColumnReason].ToString()
                            };
                            nozzleResponses.Add(nozzleState);
                        }
                    }
                }

                // Now copy over the NozzleMaintenanceStatusInfos to the response object
                var verifyToolResponse = new GetNozzleMaintenanceStatusResponse()
                {
                    NozzlesResponse = nozzleResponses.ToArray()
                };

                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " finished: NozzleMaintenanceStatusInfos: " + verifyToolResponse);

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