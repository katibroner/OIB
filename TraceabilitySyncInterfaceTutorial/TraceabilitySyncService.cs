#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.Diagnostics;
using System.ServiceModel;
using www.siplace.com.OIB._2012._03.Traceability.Contracts.Data;
using www.siplace.com.OIB._2012._03.Traceability.Contracts.Service;
using www.siplace.com.OIB._2016._05.Traceability.Contracts.CallbackService;

#endregion

namespace TraceabilitySyncInterfaceTutorial
{
    /// <summary>
    /// The implementation if the Traceability OIB Contract
    ///     The WCF contract for the synchronous communication between the TraceService Plug-in DLL and the MES system<br></br>
    ///     This interface has to be implemented by the MES-System which acts as "Host"-Application. The traceability data
    ///     collection and delivery is realized by module "TraceService" which acts as "Client" for the ITraceabilityDataCallback
    ///     interface.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class TraceabilitySyncService : ITraceabilityDataCallback
    {
        #region private Fields

        private readonly object _lockObject = new object();
        private int _boardValidationResult;
        private string _boardValidationReason;
        private MainForm _owningForm;

        #endregion

        #region Construction

        public TraceabilitySyncService(MainForm owningForm, int boardValidationResult, string boardValidationReason)
        {
            _boardValidationResult = boardValidationResult;
            _boardValidationReason = boardValidationReason;
            _owningForm = owningForm;
        }
     
        #endregion

        #region public Properties

        public int BoardValidationResult
        {
            get
            {
                lock(_lockObject)
                {
                    return _boardValidationResult;
                }
            }
            set
            {
                lock(_lockObject)
                {
                    if(value >= 1 && value <= 4)
                    {
                        _boardValidationResult = value;
                    }
                }
            }
        }

        public string BoardValidationReason
        {
            get
            {
                lock(_lockObject)
                {
                    return _boardValidationReason;
                }
            }
            set
            {
                lock(_lockObject)
                {
                    _boardValidationReason = value;
                }
            }
        }

        #endregion

        #region  DOC_ITraceabilityDataCallback

        /// <summary>
        /// Checks if the MES system is reachable. Behaves as follows:<br></br>
        /// Communication layer throws CommunicationException if the target system is not available.<br></br>
        /// Throws FaultException &lt;TraceabilityFault&gt; if MES is available, but not initialized.<br></br>
        /// </summary>
        /// <returns>The Return Code</returns>
        /// <exception cref="TraceabilityFault"></exception>
        public PingResponse Ping(PingRequest request)
        {
            try
            {
                Trace.WriteLine("Ping");
            }
            catch(Exception ex)
            {
                throw new FaultException<TraceabilityFault>(new TraceabilityFault {ExtendedMessage = "Ping Method threw exception"}, ex.Message);
            }
            return new PingResponse();
        }

        /// <summary>
        /// Informs the MES that a board was produced by a station (station based traceability) or by a line (combine mode or line based mode). Includes the Traceability data of the board and the current processing state (locked or not)<br/>
        /// If interlocking mode is activated, the MES is able to decide if the board is kept locked in the conveyor or not by the result structure. 
        /// </summary>
        /// <param name="boardRequest">the board data</param>
        /// <returns>The result of the MES validation of the board</returns>
        /// <exception cref="TraceabilityFault"></exception>
        public BoardProducedResponse BoardProduced(BoardProducedRequest boardRequest)
        {
            try
            {
                _owningForm.AddMessageToListbox("BoardProduced() called for board: " + boardRequest.TraceabilityData.BoardID);
                BoardProducedResponse boardProducedResult = new BoardProducedResponse
                {
                    // BoardValidationResult
                    // 1: 
                    // ERROR - An internal error occurred in MES, there is no decision about the board possible. The board is not
                    // transported. The operator has to check the log filed of MES
                    // 2: 
                    // CONFIRMED - MES confirmed the board, the station unlocks the board and it will be transported to the next
                    // processing step
                    // 3:
                    // REJECTED_HOLD - MES rejected the board, the board is kept in the conveyor and must be removed manually by
                    // the operator.
                    // 4:
                    // REJECTED_PASS - MES rejected the board but it will be transported through the line. In that case, the
                    // operator will not get any information by TraceService that the board was rejected. So in that case, the customer has to
                    // implement a solution how to proceed with the board, e.g. the MES implements a escalation path to inform the operator
                    // asynchronous

                    BoardValidationResult = BoardValidationResult,

                    // This text is optional
                    BoardValidationReason = BoardValidationReason
                };

                _owningForm.AddMessageToListbox("BoardProduced() called for board: " + boardRequest.TraceabilityData.BoardID + " , returning BoardValidationResult: " +
                                                boardProducedResult.BoardValidationResult + ",  BoardValidationReason: " + boardProducedResult.BoardValidationReason);
                return boardProducedResult;
            }
            catch(Exception ex)
            {
                throw new FaultException<TraceabilityFault>(new TraceabilityFault {ExtendedMessage = "BoardProduced Method threw exception"}, ex.Message);
            }
        }

        public BoardCheckInResponse BoardCheckin(BoardCheckInRequest boardRequest)
        {
            try
            {
                Trace.WriteLine("BoardCheckin request for Board: " + boardRequest.BoardId);
            }
            catch (Exception ex)
            {
                throw new FaultException<TraceabilityFault>(new TraceabilityFault { ExtendedMessage = "BoardCheckin Method threw exception" }, ex.Message);
            }
            return new BoardCheckInResponse
            {
                BoardValidationResult = 0
            };
        }

        #endregion
    }
}