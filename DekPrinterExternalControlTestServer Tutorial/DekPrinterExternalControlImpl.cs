#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using MesModel = www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;

#endregion

namespace DekPrinterExternalControlTestServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DekPrinterExternalControlImpl : IDekPrinterExternalControl
    {
        #region Fields

        private readonly DekPrinterExternalControlTestForm _form;

        #endregion

        #region Constructors

        public DekPrinterExternalControlImpl(DekPrinterExternalControlTestForm form)
        {
            _form = form;
        }

        #endregion

        #region Helper methods

        private void HandleTestException()
        {
            if(_form.IsMesThrowException)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed. The Dek Printer External Control TestServer was configured to throw this exception!");
                var fault = new SiplaceSetupCenterFault();
                fault.ErrorCode = 10000;
                fault.ExtendedMessage = MethodBase.GetCurrentMethod().Name + " failed. The Dek Printer External Control TestServer was configured to throw this exception!";
                var reason = new FaultReason("Dek Printer External Control TestServer application exception occured");
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
        }

        private void HandleTestTimeout()
        {
            if(_form.IsMesTimeout > 0)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " sleeping...");
                Thread.Sleep(_form.IsMesTimeout*1000);
            }
        }

        #endregion

        #region DOC_IDekPrinterExternalControl

        /// <summary>
        ///     Pings this instance.
        /// </summary>
        /// <returns></returns>
        public MesModel.PingResponse DekPing(MesModel.PingRequest pingRequest)
        {
            _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");

            #region Test exception

            HandleTestException();

            #endregion

            #region Test timeout

            HandleTestTimeout();

            #endregion

            _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received finished");

            return new MesModel.PingResponse();
        }

        public MesModel.VerifyPrinterToolResponse VerifyPrinterTool(MesModel.VerifyPrinterToolRequest request)
        {
            try
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");

                #region Test exception

                HandleTestException();

                #endregion

                #region Test timeout

                HandleTestTimeout();

                #endregion

                var verifyToolValues = _form.VerifyToolValues;

                MesModel.PrinterTool printerTool = new MesModel.PrinterTool();
                printerTool.Message = verifyToolValues.Message;
                printerTool.PrinterValidationStatus = verifyToolValues.VerificationStatus;
                var verifyToolResponse = new MesModel.VerifyPrinterToolResponse
                {
                    PrinterTool = printerTool
                };

                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received finished");

                return verifyToolResponse;
            }
            catch(Exception ex)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem!");
                var fault = new SiplaceSetupCenterFault();
                fault.ErrorCode = 20000;
                fault.ExtendedMessage = MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem! Exception: " + ex;
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
        }

        public MesModel.VerifyPrinterMaterialResponse VerifyPrinterMaterial(MesModel.VerifyPrinterMaterialRequest request)
        {
            try
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received");

                #region Test exception

                HandleTestException();

                #endregion

                #region Test timeout

                HandleTestTimeout();

                #endregion

                var verifyMaterialValues = _form.VerifyMaterialValues;

                MesModel.PrinterMaterial printerMaterial = new MesModel.PrinterMaterial();
                printerMaterial.PrinterValidationStatus = verifyMaterialValues.VerificationStatus;
                printerMaterial.Message = verifyMaterialValues.Message;
                var verifyMaterialResponse = new MesModel.VerifyPrinterMaterialResponse
                {
                    PrinterMaterial = printerMaterial
                };

                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " received finished");

                return verifyMaterialResponse;
            }
            catch(Exception ex)
            {
                _form.MsgOut(MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem!");
                var fault = new SiplaceSetupCenterFault();
                fault.ErrorCode = 20000;
                fault.ExtendedMessage = MethodBase.GetCurrentMethod().Name + " failed, on an unknown problem! Exception: " + ex;
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
        }

        #endregion
    }
}