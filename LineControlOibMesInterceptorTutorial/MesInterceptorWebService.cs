#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.ServiceModel;
using System.Threading;
using www.siplace.com.OIB._2015._10.LineControlServer.Contracts.Data;
using www.siplace.com.OIB._2015._10.LineControlServer.Contracts.Fault;
using www.siplace.com.OIB._2015._10.LineControlServer.Contracts.MESService;

#endregion

namespace LineControlOibMesInterceptorTutorial
{
    /// <summary>
    /// The implementation of IBoardNotification interface
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class MesInterceptorWebService : ILineControlMesInterceptor
    {
        private const string m_cReturnNullForVerificationresult = "<return null for verification result>";

        public bool Ping()
        {
            try
            {
                MesSimForm.AddTrace("Ping called");
                Thread.Sleep(MesSimForm.BlockCallbackSec*1000);
            }
            catch
            {
                // ignored
            }

            return MesSimForm.InitializedPingOK;
        }

        public MesVerificationResult VerifyRecipeActivationOnDekPrinter(VerifyRecipeActivationOnDekPrinterRequest request)
        {
            MesVerificationResult result = new MesVerificationResult();
            try
            {
                MesSimForm.AddTrace(String.Format("VerifyRecipeActivationOnDekPrinter called for printer '{0}' in line '{1}': MachineId={2}, MachineType='{3}', Lane={4}, ProductId={5}, ProductName='{6}', ProductLastModified={7}",
                request.DeviceLocator.MachineName, request.DeviceLocator.LineName, request.DeviceLocator.MachineId, request.DeviceLocator.MachineType,
                request.DekPrinterRecipeVerificationData.ConveyorLane, request.DekPrinterRecipeVerificationData.ProductId, request.DekPrinterRecipeVerificationData.ProductName, request.DekPrinterRecipeVerificationData.DateModified));

                result.VerificationResult = (MesSimForm.VerificationResult_Continue) ? (int)MesDecision.OK : (int)MesDecision.Rejected;
                result.Reason = MesSimForm.VerificationResult_Reason;

                if (MesSimForm.VerificationResult_Reason.Equals(m_cReturnNullForVerificationresult))
                    result = null;

                Thread.Sleep(MesSimForm.BlockCallbackSec * 1000);
            }
            catch (Exception ex)
            {
                // Error handling goes here and possible throw of a fault exception
                throw new FaultException<LineControlServerFault>(new LineControlServerFault { ErrorCode = 0, ExtendedMessage = ex.Message });
            }
            return result;
        }

        public MesVerificationResult VerifyRecipeDownload(VerifyRecipeDownloadRequest request)
        {
            MesVerificationResult result = new MesVerificationResult();
            try
            {
                MesSimForm.AddTrace(String.Format("VerifyRecipeDownload called for line {0}", request.LineName));
                DownloadVerificationData[] data = request.DownloadVerificationData;
                foreach (DownloadVerificationData laneData in data)
                {
                    MesSimForm.AddTrace(String.Format("\t for Lane {0}: ProductionSchedule='{1}', Recipe='{2}', PS-ElementId='{3}', OrderId='{4}'", laneData.Lane, laneData.ProductionSchedule, laneData.Recipe, laneData.ProductionScheduleElementId, laneData.OrderId));
                }

                result.VerificationResult = (MesSimForm.VerificationResult_Continue) ? (int)MesDecision.OK : (int)MesDecision.Rejected;
                result.Reason = MesSimForm.VerificationResult_Reason;

                if (MesSimForm.VerificationResult_Reason.Equals(m_cReturnNullForVerificationresult))
                    result = null;

                Thread.Sleep(MesSimForm.BlockCallbackSec * 1000);
            }
            catch (Exception ex)
            {
                // Error handling goes here and possible throw of a fault exception
                throw new FaultException<LineControlServerFault>(new LineControlServerFault { ErrorCode = 0, ExtendedMessage = ex.Message });
            }
            return result;
        }

        public MesVerificationResult VerifyStartAutoProgramDownload(VerifyRecipeForAutoProgramDownloadRequest request)
        {
            MesVerificationResult result = new MesVerificationResult();
            try
            {
                AutoProgramDownloadRecipeVerificationData data = request.AutoProgramDownloadVerificationData;
                MesSimForm.AddTrace(String.Format("VerifyStartAutoProgramDownload called for line {0}: Job='{1}', Recipe='{2}'", request.LineName, data.Job, data.Recipe));

                result.VerificationResult = (MesSimForm.VerificationResult_Continue) ? (int)MesDecision.OK : (int)MesDecision.Rejected;
                result.Reason = MesSimForm.VerificationResult_Reason;

                if (MesSimForm.VerificationResult_Reason.Equals(m_cReturnNullForVerificationresult))
                    result = null;

                Thread.Sleep(MesSimForm.BlockCallbackSec * 1000);
            }
            catch (Exception ex)
            {
                // Error handling goes here and possible throw of a fault exception
                throw new FaultException<LineControlServerFault>(new LineControlServerFault
                {
                    ErrorCode = 0,
                    ExtendedMessage = ex.Message
                });
            }
            return result;
        }

        public MesVerificationResult VerifyRecipeForAutoProgramDownload(VerifyRecipeForAutoProgramDownloadRequest request)
        {
            MesVerificationResult result = new MesVerificationResult();
            try
            {

                AutoProgramDownloadRecipeVerificationData data = request.AutoProgramDownloadVerificationData;
                MesSimForm.AddTrace(String.Format("VerifyRecipeForAutoProgramDownload called for line {0}: Job='{1}', Recipe='{2}'", request.LineName, data.Job, data.Recipe));

                result.VerificationResult = (MesSimForm.VerificationResult_Continue) ? (int)MesDecision.OK : (int)MesDecision.Rejected;
                result.Reason = MesSimForm.VerificationResult_Reason;

                if (MesSimForm.VerificationResult_Reason.Equals(m_cReturnNullForVerificationresult))
                    result = null;

                Thread.Sleep(MesSimForm.BlockCallbackSec * 1000);
            }
            catch (Exception ex)
            {
                // Error handling goes here and possible throw of a fault exception
                throw new FaultException<LineControlServerFault>(new LineControlServerFault
                {
                    ErrorCode = 0,
                    ExtendedMessage = ex.Message
                });
            }
            return result;
        }

        public MesVerificationResult VerifyPcbForAutoProgramDownload(VerifyPcbForAutoProgramDownloadRequest request)
        {
            MesVerificationResult result;
            try
            {
                MesSimForm.AddTrace(String.Format("VerifyPcbForAutoProgramDownload called for line {0} and station {1} (MachineId={2}, MachineType='{3}')",
                                request.DeviceLocator.LineName, request.DeviceLocator.MachineName, request.DeviceLocator.MachineId, request.DeviceLocator.MachineType));
                AutoProgramDownloadPcbVerificationData data = request.AutoProgramDownloadPcbVerificationData;
                MesSimForm.AddTrace(String.Format("\t for Lane {0}: Barcode='{1}'(pos={2}), Barcode='{3}'(pos={4}), Recipe='{5}', LineJobGuid='{6}'",
                                                    data.Lane, data.PcbBarcode, data.BarcodePosition, data.PcbBarcode2, data.BarcodePosition2, data.Recipe, data.LineJobGUID));

                Thread.Sleep(MesSimForm.BlockCallbackSec * 1000);

                if (MesSimForm.VerificationResult_Reason.Equals(m_cReturnNullForVerificationresult))
                {
                    return null;
                }

                result = new MesVerificationResult()
                {
                    VerificationResult = (MesSimForm.VerificationResult_Continue) ? (int)MesDecision.OK : (int)MesDecision.Rejected,
                    Reason = MesSimForm.VerificationResult_Reason
                };
            }
            catch (Exception ex)
            {
                // Error handling goes here and possible throw of a fault exception
                throw new FaultException<LineControlServerFault>(new LineControlServerFault
                {
                    ErrorCode = 0,
                    ExtendedMessage = ex.Message
                });
            }
            return result;
        }
    }
}