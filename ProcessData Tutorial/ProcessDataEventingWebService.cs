#region Copyright

// Siplace ProcessData OIB Tutorial - Copyright (C) ASM Assembly Systems 2017
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.

#endregion



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Xml.Linq;
using www.siplace.com.OIB._2012._03.Traceability.Contracts.Data;
using www.siplace.com.OIB._2012._03.Traceability.Contracts.Service;

#region DOC_ProcessDataEventingWebService

namespace ProcessDataTutorial
{

    /// <summary>
    /// The implementation if the IProcessDataDuplex service callback interface.
    /// Since the InstanceContextMode is single and the calls are all synchronized by default
    /// (ConcurrencyMode.Single), the NewProcessData() method is always run by one thread at a time.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class ProcessDataEventingWebService : IProcessDataDuplex
    {

        #endregion

        #region Fields

        #region DOC_DataCacheCompleted
        // This contains all batches that are complete and ready to be processed (xml file written)
        private readonly Queue<LoctionSpecificProductiondataBatch> _productionDataCacheCompleted =
            new Queue< LoctionSpecificProductiondataBatch>();
        #endregion

        #region DOC_DataCacheProcessing
        // This cache collects all events from started until ended/aborted for one board individual per location at a station
        // The key is the ContextId
        private readonly Dictionary<string, LoctionSpecificProductiondataBatch> _productionDataCacheProcessing =
            new Dictionary<string, LoctionSpecificProductiondataBatch>();
        #endregion

        private readonly Timer _timer = new Timer(5000);

        #endregion

        #region Constructors
        public ProcessDataEventingWebService()
        {
            _timer.Elapsed += ProcessDataCacheCompleted;
            _timer.Enabled = true;
        }

        #endregion

        #region Methods

        #region DOC_NewProcessData
        public NewProcessDataResponse NewProcessData(ProcessDataRequest request)
        {
            try
            {
                if (request.ProductionStartedData != null)
                {
                    HandleProcessDataProductionStarted(request);
                }
                else if (request.BoardMeasuredData != null)
                {
                    // We should find the context in our cache. If not, we just got an event that cannot be processed since we missed the started event
                    if (!_productionDataCacheProcessing.ContainsKey(request.ContextId))
                    {
                        Console.WriteLine(
                            "Received BoardMeasuredData but no ProductionStartedData received for this context id: " +
                            request.ContextId);
                        return new NewProcessDataResponse();
                    }
                    HandleProcessDataBoardMeasured(request);
                }
                else if (request.ProductionProgressData != null)
                {
                    // We should find the context in our cache. If not, we just got an event that cannot be processed since we missed the started event
                    if (!_productionDataCacheProcessing.ContainsKey(request.ContextId))
                    {
                        Console.WriteLine(
                            "Received ProductionProgressData but no ProductionStartedData received for this context id: " +
                            request.ContextId);
                        return new NewProcessDataResponse();
                    }
                    HandleProcessDataProductionProgress(request);
                }
                else if (request.ProductionEndedData != null)
                {
                    // We should find the context in our cache. If not, we just got an event that cannot be processed since we missed the started event
                    if (!_productionDataCacheProcessing.ContainsKey(request.ContextId))
                    {
                        Console.WriteLine(
                            "Received ProductionEndedData but no ProductionStartedData received for this context id: " +
                            request.ContextId);
                        return new NewProcessDataResponse();
                    }
                    HandleProcessDataProductionEnded(request);
                }
                else if (request.ProductionAbortedData != null)
                {
                    // We should find the context in our cache. If not, we just got an event that cannot be processed since we missed the started event
                    if (!_productionDataCacheProcessing.ContainsKey(request.ContextId))
                    {
                        Console.WriteLine(
                            "Received ProductionAbortedData but no ProductionStartedData received for this context id: " +
                            request.ContextId);
                        return new NewProcessDataResponse();
                    }
                    HandleProcessDataProductionAborted(request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Got exception: " + ex);
            }
            return new NewProcessDataResponse();
        }

        #endregion

        private void HandleProcessDataProductionStarted(ProcessDataRequest request)
        {
            if (request.ProductionStartedData.StartedData != null)
            {
                var productionData = request.ProductionStartedData.StartedData;

                if (!_productionDataCacheProcessing.ContainsKey(request.ContextId))
                {
                    _productionDataCacheProcessing.Add(request.ContextId, new LoctionSpecificProductiondataBatch(request));
                }

                var builder = new StringBuilder("Received ProductionStartedData: ");
                builder.AppendFormat("ContextId : '{0}'\n", request.ContextId);
                builder.AppendFormat("Line: '{0}', Station: '{1}'\n", request.LinePath, request.StationPath);
                builder.AppendFormat("StationTime: '{0}', TimeStampSinceStationStarted: '{1}'\n",
                    productionData.StationTime, productionData.TimeStampSinceStationStarted);
                builder.AppendFormat("Setup: '{0}', ProcessingAreaPosition: '{1}'\n", productionData.SetupPath,
                    productionData.ProcessingAreaPosition);
                builder.AppendFormat("Conveyor Lane Position: '{0}', Conveyor Lane Stopper Position: '{1}'\n",
                    productionData.ConveyorLanePosition, productionData.ConveyorStopperPosition);
                if (productionData.Pcb != null)
                {
                    PCB pcb = productionData.Pcb;
                    builder.AppendFormat(
                        "\t Board Name: '{0}', BoardSide: {1}, Recipe Name: '{2}, IndividualGUID : '{3}''\n",
                        pcb.Path, pcb.BoardSide, pcb.RecipePath, pcb.IndividualGUID);
                    builder.AppendFormat(
                        "\t LineJobGUID: '{0}', IndividualID: '{1}', Barcode: '{2}, BarcodeReaderPosition: {3}\n",
                        pcb.LineJobGUID, pcb.PcbIndividualID, pcb.Barcode, pcb.BarcodeReaderPosition);
                    builder.AppendFormat("\t OrderNumber: '{0}',\n", pcb.OrderNumber);
                    builder.AppendLine("");
                }

                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine(
                    "Received ProductionStartedData but contents are unreadable since License is unavailable.");
            }
        }

        private void HandleProcessDataBoardMeasured(ProcessDataRequest request)
        {
            var boardMeasured = request.BoardMeasuredData;


            if (boardMeasured.BoardPositions != null)
            {
                var builder = new StringBuilder("Received BoardMeasuredData: ");
                builder.AppendFormat("ContextId : '{0}'\n", request.ContextId);
                builder.AppendFormat("Line: '{0}', Station: '{1}'\n", request.LinePath, request.StationPath);
                if (boardMeasured.BoardPositions != null)
                {
                    _productionDataCacheProcessing[request.ContextId].AddBoardPositions(
                        boardMeasured.BoardPositions);

                    if (boardMeasured.BoardPositions.NominalPosition != null)
                    {
                        var nominal = boardMeasured.BoardPositions.NominalPosition;
                        builder.AppendFormat("NominalPosition: X: {0} Y: {1}: Z:{2} Angle:{3}\n", nominal.X,
                            nominal.Y, nominal.Z, nominal.Angle);
                    }
                    if (boardMeasured.BoardPositions.ActualPosition != null)
                    {
                        var actual = boardMeasured.BoardPositions.ActualPosition;
                        builder.AppendFormat("ActualPosition: X: {0} Y: {1}: Z:{2} Angle:{3}\n", actual.X,
                            actual.Y, actual.Z, actual.Angle);
                    }
                    if (boardMeasured.BoardPositions.FiducialPositions != null)
                    {
                        var i = 1;
                        foreach (var fiducialPosition in boardMeasured.BoardPositions.FiducialPositions)
                        {
                            builder.AppendFormat(
                                "FiducialPosition#{4} : Path: '{0}' MeasurementResult: {1}: CameraIndividualId:{2} GantryId:{3}\n",
                                fiducialPosition.Path, fiducialPosition.MeasurementResult,
                                fiducialPosition.CameraIndividualId, fiducialPosition.GantryId, i);
                            builder.AppendFormat("StationTime: '{0}', TimeStampSinceStationStarted: '{1}'\n",
                                fiducialPosition.StationTime, fiducialPosition.TimeStampSinceStationStarted);
                            builder.AppendFormat("FiducialFunction: '{0}' VisionDumpPath: '{1}'\n",
                                fiducialPosition.FiducialFunction, fiducialPosition.VisionDumpPath);
                            if (fiducialPosition.NominalPosition != null)
                            {
                                builder.AppendFormat("\tNominalPosition: X: {0} Y: {1}: Angle:{2}\n",
                                    fiducialPosition.NominalPosition.X, fiducialPosition.NominalPosition.Y,
                                    fiducialPosition.NominalPosition.Angle);
                            }
                            if (fiducialPosition.ActualPosition != null)
                            {
                                builder.AppendFormat("\tActualPosition: X: {0} Y: {1}: Angle:{2}\n",
                                    fiducialPosition.ActualPosition.X, fiducialPosition.ActualPosition.Y,
                                    fiducialPosition.ActualPosition.Angle);
                            }
                            i++;
                        }
                    }
                }
                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine(
                    "Received BoardMeasuredData but contents are unreadable since License is unavailable.");
            }
        }

        private void HandleProcessDataProductionProgress(ProcessDataRequest request)
        {
            var progressData = request.ProductionProgressData;

            var builder = new StringBuilder("Received ProductionProgressData: ");
            builder.AppendFormat("ContextId : '{0}'\n", request.ContextId);
            builder.AppendFormat("Line: '{0}', Station: '{1}'\n", request.LinePath, request.StationPath);
            if (progressData.ProgressData != null)
            {
                var productionProgressData = progressData.ProgressData;
                _productionDataCacheProcessing[request.ContextId].AddProgressData(productionProgressData);

                if (productionProgressData.BoardContext != null &&
                    productionProgressData.BoardContext.BoardPanels != null)
                {
                    builder.AppendFormat("BoardContext contains {0} Panels:'\n",
                        productionProgressData.BoardContext.BoardPanels.Length);
                    foreach (var panel in productionProgressData.BoardContext.BoardPanels)
                    {
                        builder.AppendFormat("Panel ID: {0}, PanelPath: {1}'\n", panel.Id, panel.PanelPath);
                    }
                }
                if (productionProgressData.SetupContext != null &&
                    productionProgressData.SetupContext.PickupLocations != null)
                {
                    builder.AppendFormat("SetupContext contains {0} PickupLocations:'\n",
                        productionProgressData.SetupContext.PickupLocations.Length);
                    foreach (var pickupLocation in productionProgressData.SetupContext.PickupLocations)
                    {
                        builder.AppendFormat("PickupLocation ID: {0}, Location: {1}, SubLocation: {2}'\n",
                            pickupLocation.Id, pickupLocation.Location, pickupLocation.SubLocation);
                        builder.AppendFormat("StartTrackNumber: {0}, Division: {1}, FeederType: {2}'\n",
                            pickupLocation.StartTrackNumber, pickupLocation.Division, pickupLocation.FeederType);
                        builder.AppendFormat("Tower: {0}, Level: {1}, FeederId: {2}, PackagingUid: {3} '\n", pickupLocation.Tower,
                            pickupLocation.Level, pickupLocation.FeederId, pickupLocation.PackagingUid);
                    }
                }
                if (productionProgressData.HeadContext != null &&
                    productionProgressData.HeadContext.HeadSegments != null)
                {
                    builder.AppendFormat("HeadContext contains {0} HeadSegments:'\n",
                        productionProgressData.HeadContext.HeadSegments.Length);
                    foreach (var headSegment in productionProgressData.HeadContext.HeadSegments)
                    {
                        builder.AppendFormat("HeadSegment ID: {0}, GantryId: {1}, Head: {2}'\n", headSegment.Id,
                            headSegment.GantryId, headSegment.Head);
                        builder.AppendFormat(
                            "HeadName: {0}, CameraName: {1}, SegmentNumber: {2}, NozzleTypeId: {3}\n",
                            headSegment.HeadName, headSegment.CameraName, headSegment.SegmentNumber,
                            headSegment.NozzleTypeId);
                    }
                }
                if (productionProgressData.ComponentIndividuals != null)
                {
                    builder.AppendFormat("ProductionProgressData contains {0} ComponentIndividuals:'\n",
                        productionProgressData.ComponentIndividuals.Length);
                    foreach (var component in productionProgressData.ComponentIndividuals)
                    {
                        builder.AppendFormat("HeadSegment ID: {0}'\n", component.HeadSegmentId);
                        if (component.Pick != null)
                        {
                            builder.AppendFormat(
                                "PickupLocationId: {0}, Result: {1}, TimeStampSinceStationStarted: {2}'\n",
                                component.Pick.PickupLocationId, component.Pick.Result,
                                component.Pick.TimeStampSinceStationStarted);
                            builder.AppendFormat(
                                "MeasuredComponentHeight: {0}, ZTargetPosition: {1}, ZEndPosition: {2}'\n",
                                component.Pick.MeasuredComponentHeight, component.Pick.ZTargetPosition,
                                component.Pick.ZEndPosition);
                            builder.AppendFormat("VacuumAbsolute: {0}, VacuumRelative: {1}\n",
                                component.Pick.VacuumAbsolute, component.Pick.VacuumRelative);
                            if (component.Pick.NominalPickPosition != null)
                            {
                                builder.AppendFormat("\tNominalPickPosition: X: {0} Y: {1}: Angle:{2}\n",
                                    component.Pick.NominalPickPosition.X, component.Pick.NominalPickPosition.Y,
                                    component.Pick.NominalPickPosition.Angle);
                            }
                            if (component.Pick.ActualPickPosition != null)
                            {
                                builder.AppendFormat("\tActualPickPosition: X: {0} Y: {1}: Angle:{2}\n",
                                    component.Pick.ActualPickPosition.X, component.Pick.ActualPickPosition.Y,
                                    component.Pick.ActualPickPosition.Angle);
                            }
                        }
                        if (component.Place != null)
                        {
                            builder.AppendFormat(
                                "Place ReferenceDesignator: {0}, Result: {1}, TimeStampSinceStationStarted: {2}'\n",
                                component.Place.ReferenceDesignator, component.Place.Result,
                                component.Place.TimeStampSinceStationStarted);
                            builder.AppendFormat(
                                "BoardPanelId: {0}, Result: {1}, TimeStampSinceStationStarted: {2}'\n",
                                component.Place.BoardPanelId, component.Place.Result,
                                component.Place.TimeStampSinceStationStarted);
                            builder.AppendFormat(
                                "AirkissPressure: {0}, HoldingCircuitPressure: {1}, ZTargetPosition: {2}, ZEndPosition: {3}'\n",
                                component.Place.AirkissPressure, component.Place.HoldingCircuitPressure,
                                component.Place.ZTargetPosition, component.Place.ZEndPosition);
                            builder.AppendFormat("VacuumAbsolute: {0}, VacuumRelative: {1}\n",
                                component.Place.VacuumAbsolute, component.Place.VacuumRelative);
                            if (component.Place.NominalPlacePosition != null)
                            {
                                builder.AppendFormat("\tNominalPlacePosition: X: {0} Y: {1}: Angle:{2}\n",
                                    component.Place.NominalPlacePosition.X,
                                    component.Place.NominalPlacePosition.Y,
                                    component.Place.NominalPlacePosition.Angle);
                            }
                            if (component.Place.ActualPlacePosition != null)
                            {
                                builder.AppendFormat("\tActualPlacePosition: X: {0} Y: {1}: Angle:{2}\n",
                                    component.Place.ActualPlacePosition.X, component.Place.ActualPlacePosition.Y,
                                    component.Place.ActualPlacePosition.Angle);
                            }
                        }
                        if (component.Dip != null)
                        {
                            builder.AppendFormat("Dip Result: {0}, TimeStampSinceStationStarted: {1}'\n",
                                component.Dip.Result, component.Dip.TimeStampSinceStationStarted);
                            builder.AppendFormat("ZTargetPosition: {0}, ZEndPosition: {1}'\n",
                                component.Dip.ZTargetPosition, component.Dip.ZEndPosition);
                        }

                        if (component.Measures != null)
                        {
                            builder.AppendFormat("Measures contains {0} Measures:'\n", component.Measures.Length);
                            foreach (var measure in component.Measures)
                            {
                                builder.AppendFormat(
                                    "CameraIndividualId: {0}, MeasurementResult: {1}, TimeStampSinceStationStarted: {2}'\n",
                                    measure.CameraIndividualId, measure.MeasurementResult,
                                    measure.TimeStampSinceStationStarted);
                                builder.AppendFormat(
                                    "XDeviation: {0}, YDeviation: {1}, PhiDeviation: {2}, VisionDumpPath: {3}\n",
                                    measure.XDeviation, measure.YDeviation, measure.PhiDeviation,
                                    measure.VisionDumpPath);
                            }
                        }
                        if (component.ElectricalMeasurement != null)
                        {
                            ElectricalMeasurement measurement = component.ElectricalMeasurement;
                            builder.AppendFormat("ElectricalMeasurement: DeviceSerialNumber:{0}, MeasuredValue={1}, NominalValue={2}, TimeStampSinceStationStarted={3}\n", 
                                measurement.DeviceSerialNumber, measurement.MeasuredValue, measurement.NominalValue, measurement.TimeStampSinceStationStarted);
                        }
                    }
                }

                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine(
                    "Received ProductionProgressData but contents are unreadable since License is unavailable.");
            }
        }

        #region DOC_HandleProcessDataProductionEnded
        private void HandleProcessDataProductionEnded(ProcessDataRequest request)
        {
            var productionEnded = request.ProductionEndedData;

            var builder = new StringBuilder("Received ProductionEndedData: ");
            builder.AppendFormat("ContextId : '{0}'\n", request.ContextId);
            builder.AppendFormat("Line: '{0}', Station: '{1}'\n", request.LinePath, request.StationPath);
            if (productionEnded.EndedData != null)
            {
                var productionEndedData = request.ProductionEndedData.EndedData;
                _productionDataCacheProcessing[request.ContextId].CompleteCycle(productionEndedData);

                // Move to the completed cache
                lock (_productionDataCacheCompleted)
                {
                    _productionDataCacheCompleted.Enqueue(_productionDataCacheProcessing[request.ContextId]);
                }
                _productionDataCacheProcessing.Remove(request.ContextId);
                builder.AppendFormat("StationTime: '{0}', TimeStampSinceStationStarted: '{1}'\n",
                    productionEndedData.StationTime, productionEndedData.TimeStampSinceStationStarted);

                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine(
                    "Received ProductionEndedData but contents are unreadable since License is unavailable.");
            }
        }
        #endregion

        private void HandleProcessDataProductionAborted(ProcessDataRequest request)
        {
            var productionAborted = request.ProductionAbortedData;
            var builder = new StringBuilder("Received ProductionAbortedData: ");
            builder.AppendFormat("ContextId : '{0}'\n", request.ContextId);
            builder.AppendFormat("Line: '{0}', Station: '{1}'\n", request.LinePath, request.StationPath);
            if (productionAborted.AbortedData != null)
            {
                var productionAbortedData = productionAborted.AbortedData;
                _productionDataCacheProcessing[request.ContextId].CompleteCycle(productionAbortedData);

                // Move to the completed cache
                lock (_productionDataCacheCompleted)
                {
                    _productionDataCacheCompleted.Enqueue(_productionDataCacheProcessing[request.ContextId]);
                }
                _productionDataCacheProcessing.Remove(request.ContextId);
                builder.AppendFormat("StationTime: '{0}', TimeStampSinceStationStarted: '{1}', Reason: {2} \n",
                    productionAbortedData.StationTime, productionAbortedData.TimeStampSinceStationStarted,
                    productionAbortedData.Reason);

                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine(
                    "Received ProductionAbortedData but contents are unreadable since License is unavailable.");
            }
        }


        #region DOC_ProcessQueue
        private void ProcessDataCacheCompleted(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                _timer.Enabled = false;

                Queue<LoctionSpecificProductiondataBatch> localQueue;

                // Copy over to a local queue so we hold the lock for a minimum of time
                lock (_productionDataCacheCompleted)
                {
                    localQueue = new Queue<LoctionSpecificProductiondataBatch>(_productionDataCacheCompleted);
                    _productionDataCacheCompleted.Clear();
                }

                foreach (LoctionSpecificProductiondataBatch batch in localQueue)
                {
                    try
                    {
                        using (var stream = new FileStream(ConstructXmlFileName(batch), FileMode.Create))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                writer.Write(batch.GetBatchContents());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }
        #endregion

        private string ConstructXmlFileName(LoctionSpecificProductiondataBatch batch)
        {
            StringBuilder filename = new StringBuilder(batch.LinePath.LastChars());
            filename.AppendFormat("_{0}", batch.StationPath.LastChars());
            filename.AppendFormat("_PA{0}", batch.StartedData.ProcessingAreaPosition);
            filename.AppendFormat("_CL{0}", batch.StartedData.ConveyorLanePosition);
            filename.AppendFormat("_S{0}", batch.StartedData.ConveyorStopperPosition);
            filename.AppendFormat("_{0}", batch.ContextId);
            filename.Append(".xml");
            return filename.ToString().ToFileName();
        }

        #endregion

        #region DOC_LoctionSpecificProductiondataBatch

        internal class LoctionSpecificProductiondataBatch
        {
            #region Fields

            private readonly List<BoardPositions> _boardPositionsList = new List<BoardPositions>();
            private readonly string _contextId;
            private readonly string _linePath;
            private readonly string _stationPath;

            private readonly List<ProductionProgressData> _progressDataList = new List<ProductionProgressData>();
            private bool _completed;

            #endregion

            #region Constructors

            internal LoctionSpecificProductiondataBatch(ProcessDataRequest request)
            {
                if (request == null)
                    throw new ArgumentNullException("request");
                if (request.ProductionStartedData == null)
                    throw new ArgumentNullException("request.ProductionStartedData");
                if (request.ProductionStartedData.StartedData == null)
                    throw new ArgumentNullException("request.ProductionStartedData.StartedData");

                StartedData = request.ProductionStartedData.StartedData;
                _contextId = request.ContextId;
                _linePath = request.LinePath;
                _stationPath = request.StationPath;
            }

            #endregion
            #endregion

            #region Properties
            internal string ContextId { get { return _contextId;} }
            internal string LinePath { get { return _linePath; } }
            internal string StationPath { get { return _stationPath; } }

            internal ProductionStartedData StartedData { get; }
            internal ProductionEndedData EndedData { get; private set; }
            internal ProductionAbortedData AbortedData { get; private set; }

            internal IEnumerable<ProductionProgressData> ProgressData
            {
                get { return _progressDataList; }
            }

            internal IEnumerable<BoardPositions> BoardPositions
            {
                get { return _boardPositionsList; }
            }

            internal bool WasAborted
            {
                get { return AbortedData != null; }
            }

            #endregion

            #region Methods

            internal void AddProgressData(ProductionProgressData progress)
            {
                if (progress == null)
                    throw new ArgumentNullException("progress");
                if (_completed)
                    throw new Exception("This cycle is already completed and cannot be modified any more");
                _progressDataList.Add(progress);
            }

            internal void AddBoardPositions(BoardPositions positions)
            {
                if (positions == null)
                    throw new ArgumentNullException("positions");
                if (_completed)
                    throw new Exception("This cycle is already completed and cannot be modified any more");
                _boardPositionsList.Add(positions);
            }

            internal void CompleteCycle(ProductionAbortedData abortedData)
            {
                if (abortedData == null)
                    throw new ArgumentNullException("abortedData");

                if (_completed)
                    throw new Exception("This cycle is already completed and cannot be modified any more");
                AbortedData = abortedData;
                _completed = true;
            }

            internal void CompleteCycle(ProductionEndedData endedData)
            {
                if (endedData == null)
                    throw new ArgumentNullException("endedData");

                if (_completed)
                    throw new Exception("This cycle is already completed and cannot be modified any more");
                EndedData = endedData;
                _completed = true;
            }

            internal string GetBatchContents()
            {
                if (!_completed)
                    throw new Exception("This cycle is NOT completed yet!");
                try
                {
                    var doc = new XDocument();
                    var declaration = new XDeclaration("1.0", Encoding.UTF8.EncodingName, "no");
                    doc.Declaration = declaration;
                    var rootElement = new XElement("ProductionDataBatch", _contextId, LinePath, StationPath);
                    doc.Add(rootElement);

                    // StartedData
                    {
                        var startedElement = new XElement("ProductionStartedData", ToXml(StartedData));
                        rootElement.Add(startedElement);
                    }
                    // BoardPositions
                    {
                        var boardPositionsListElement = new XElement("BoardPositionsList");
                        rootElement.Add(boardPositionsListElement);
                        foreach (var positions in BoardPositions)
                        {
                            var boardPositionsElement = new XElement("BoardPositions", ToXml(positions));
                            boardPositionsListElement.Add(boardPositionsElement);
                        }
                    }
                    // ProgressData
                    {
                        var progressDataListElement = new XElement("ProgressDataList");
                        rootElement.Add(progressDataListElement);
                        foreach (var progressData in ProgressData)
                        {
                            var progressDataElement = new XElement("ProductionProgressData", ToXml(progressData));
                            progressDataListElement.Add(progressDataElement);
                        }
                    }
                    if (WasAborted)
                    {
                        // AbortedData
                        {
                            var abortedElement = new XElement("ProductionAbortedData", ToXml(AbortedData));
                            rootElement.Add(abortedElement);
                        }
                    }
                    else
                    {
                        // EndedData
                        {
                            var endedElement = new XElement("ProductionEndedData", ToXml(EndedData));
                            rootElement.Add(endedElement);
                        }
                    }
                    return doc.ToString();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Got exception: " + ex);
                }
                return string.Empty;
            }

            internal static string ToXml<T>(T siplaceData)
            {
                string strXml;
                var dataContractSerializer = new DataContractSerializer(typeof (T));

                using (var memoryStream = new MemoryStream())
                {
                    dataContractSerializer.WriteObject(memoryStream, siplaceData);
                    var data = new byte[memoryStream.Length];
                    Array.Copy(memoryStream.GetBuffer(), data, data.Length);
                    strXml = Encoding.UTF8.GetString(data);
                }
                return strXml;
            }

            #endregion
        }

   
    }

    #region Extensions

    public static class Extensions
    {
        public static string LastChars(this string theString, int numberCharachters = 20)
        {
            return theString.Substring(Math.Max(0, theString.Length - numberCharachters));
        }
        public static string ToFileName(this string theString)
        {
            string correctedName;
            if (string.IsNullOrWhiteSpace(theString))
            {
                correctedName = "EMPTY";
            }
            else
            {
                correctedName = theString;
            }

            correctedName = Path.GetInvalidFileNameChars().Aggregate(correctedName, (current, c) => current.Replace(c, '_'));
            correctedName = correctedName.Replace("\\", "_");
            return correctedName;
        }

    }

    #endregion
}

