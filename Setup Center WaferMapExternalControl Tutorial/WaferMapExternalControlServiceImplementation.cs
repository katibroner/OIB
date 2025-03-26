#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;

#endregion

namespace WaferMapExternalControlTutorial
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WaferMapExternalControlServiceImplementation : IWaferMapExternalControl
    {
        #region Fields

        private readonly Form1 _form;

        #endregion

        #region Constructors

        public WaferMapExternalControlServiceImplementation(Form1 form)
        {
            _form = form;
        }

        #endregion

        #region DOC_WaferMapPing

        public PingResponse WaferMapPing(PingRequest request)
        {
            _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");
            return new PingResponse();
        }

        #endregion

        #region DOC_GetWaferMap

        public GetWaferMapResponse GetWaferMap(GetWaferMapRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.WaferId))
                {
                    throw new ArgumentNullException(nameof(request.WaferId));
                }

                string summary = MethodBase.GetCurrentMethod().Name + " received: WaferId: " + request.WaferId;
                _form.MsgOut(summary);

                FileInfo fileInfo = _form.GetWaferMapFile(request.WaferId + ".xml");
                if (fileInfo != null)
                {
                    var waferMapResponse = new GetWaferMapResponse
                    {
                        WaferId = request.WaferId,
                        WaferMap = File.ReadAllText(fileInfo.FullName)
                    };

                    summary = MethodBase.GetCurrentMethod().Name + " received finished: WaferId: " + request.WaferId;
                    _form.MsgOut(summary);
                    return waferMapResponse;
                }

                throw new FaultException($"Wafer Map with Id '{request.WaferId}' does not exist.");
            }
            catch (Exception exception)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem!");
                var siplaceSetupCenterFault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = MethodBase.GetCurrentMethod().Name +
                                      " failed, on an unknown problem! Exception: " + exception
                };
                var faultReason = new FaultReason(exception.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(siplaceSetupCenterFault, faultReason);
            }
        }

        #endregion

        #region DOC_UpdateWaferMap

        public void UpdateWaferMap(UpdateWaferMapRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.WaferId))
                {
                    throw new ArgumentNullException(nameof(request.WaferId));
                }

                string summary = MethodBase.GetCurrentMethod().Name + " received: Wafer Map Id: " + request.WaferId;
                _form.MsgOut(summary);

                if (string.IsNullOrEmpty(request.WaferMap))
                {
                    throw new ArgumentNullException(nameof(request.WaferMap));
                }

                FileInfo fileInfo = _form.GetWaferMapFile(request.WaferId + ".xml");
                if (fileInfo != null)
                {
                    File.WriteAllText(fileInfo.FullName, request.WaferMap);
                }
                else
                {
                    throw new FaultException($"Wafer Map with Id '{request.WaferId ?? string.Empty}' does not exist.");
                }
            }
            catch (Exception exception)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem!");
                var siplaceSetupCenterFault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = MethodBase.GetCurrentMethod().Name +
                                      " failed, on an unknown problem! Exception: " + exception
                };
                var faultReason = new FaultReason(exception.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(siplaceSetupCenterFault, faultReason);
            }
        }

        #endregion
    }
}