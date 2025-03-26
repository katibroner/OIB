#region Copyright
//-----------------------------------------------------------------------
// <copyright file="TestMesService.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using System.ServiceModel;
using System.Reflection;
using OIB.Tutorial.Common.Logging;
#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// The implementation of ISiplaceSetupCenterExternalControl interface
    /// </summary>
    public class TestMesService : ISiplaceSetupCenterExternalControl
    {
        #region Fields
        // This name is constant in SetupCenter and cannot be changed!!!
        public const string MesOibServiceNameDefaultValue = "SIPLACE.SetupCenter.ExternalControl";
        private readonly ILog _log;
        #endregion

        #region Constructor
        public TestMesService(ILog log)
        {
            _log = log;
        }
        #endregion

        #region Implementation of ISiplaceSetupCenterExternalControl

        /// <summary>
        /// The ping function performs a check, if the connection to the underlying system components is alive or not
        /// </summary>
        /// <returns>true, if all system components are operational false, if any component is not operational.</returns>
        #region DOC_Ping
        public bool Ping()
        {
            _log.InfoMessage("ISiplaceSetupCenterExternalControl.Ping() begin");

            CheckAndThrowException();
            CheckForSleep();

            _log.InfoMessage("ISiplaceSetupCenterExternalControl.Ping() end");
            return true;
        }
        #endregion

        /// <summary>
        /// Request the packaging Unit data from an external system (like MES System) during the verification process. 
        /// This is enables the external system to control the data for the verification process. 
        /// The ExternalControlResult object describes the data returned from the external system.
        /// </summary>
        /// <param name="packagingUnitLocations">The packaging unit locations.</param>
        /// <returns>The list of result of the operation for these packaging units. 
        /// The value in ExternalControlResult indicates the values for the setup center packaging units. 
        /// When packaging Unit data is returned as 2=NotOk, then a internal Lock will be attached to the PackagingUnitData with Source = 9 and 
        /// Reason = 28 in the LockInfo data. This will be internally released in SetupCenter, when the external system is returning 1=Ok in this method call. 
        /// This internal lock can not the changed explicit from the external system. </returns>
        #region DOC_GetPackagingUnitControlStatus
        public ExternalControlResult[] GetPackagingUnitControlStatus(PackagingUnitLocation[] packagingUnitLocations)
        {
            var currentMethod = "ISiplaceSetupCenterExternalControl." + MethodBase.GetCurrentMethod().Name + "()";
            _log.InfoMessage(currentMethod + " - begin");
            _log.DebugMessage("Arguments : " + GetString(packagingUnitLocations));

            CheckAndThrowException();
            CheckForSleep();

            var returnValue = new ExternalControlResult[packagingUnitLocations.Length];
            try
            {
                //Adjust the data for each PU location
                for (var i = 0; i < packagingUnitLocations.Length; i++)
                {
                    var currentResult = new ExternalControlResult { PackagingUnit = packagingUnitLocations[i].PackagingUnit, ResultState = 1 };
                    if (MesContext.ModifyQuantityEnabled)
                        currentResult.PackagingUnit.Quantity = MesContext.NewQuantity;
                    if (MesContext.ModifyOriginalQuantityEnabled)
                        currentResult.PackagingUnit.OriginalQuantity = MesContext.NewOriginalQuantity;
                    if (MesContext.ModifyRohsEnabled)
                        currentResult.PackagingUnit.RoHS = MesContext.NewRohs;
                    if (MesContext.ModifyMsdEnabled)
                    {
                        currentResult.PackagingUnit.MsdLevel = MesContext.NewMsdLevel;
                        currentResult.PackagingUnit.MsdOpenDate = MesContext.NewMsdOpenDate;
                    }
                    if (MesContext.ModifySiplaceProComponentNameEnabled)
                    {
                        currentResult.PackagingUnit.ComponentName = MesContext.NewSiplaceProComponentName;
                        currentResult.PackagingUnit.ComponentBarcode = MesContext.NewSiplaceProComponentName;
                    }
                    returnValue[i] = currentResult;
                }
            }
            catch (FaultException fEx)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", fEx);
                throw;
            }
            catch (Exception ex)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", ex);
                var fault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = currentMethod + " failed, on an unknown problem! Exception: " + ex.Message
                };
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
            _log.InfoMessage(currentMethod + " - end");
            return returnValue;
        }
        #endregion

        /// <summary>
        ///     Request the packaging Unit data from an external system (like MES System) during the verification
        ///     process. This is enables the external system to control the data for the verification process.
        ///     The ExternalControlResult object describes the data returned from the external system.
        ///     <remarks>
        ///         When using this interface with SetupCenter Version 5.1 and higher it would be possible to split
        ///         existing packaging unit chains by returning a reduced packaging unit chain back to SetupCenter.
        ///     </remarks>
        /// </summary>
        /// <param name="request">The request object containing a list of packaging unit locations and a reason of why the method is called.</param>
        /// <returns>
        ///     The response object containing the list of result of the operation for these packaging units. The value in ExternalControlResult
        ///     indicates the values for the setup center packaging units.
        ///     When packaging Unit data is returned as 2=NotOk, then a internal Lock will be attached to the PackagingUnitData
        ///     with Source = 9 and Reason = 28 in the LockInfo data. This will be internally released in SetupCenter, when the
        ///     external system is returning 1=Ok in this method call. This internal lock can not the changed explicit from the
        ///     external system.
        /// </returns>
        /// <remarks>
        /// This method will only be called from SetupCenter 9.4 and higher.
        /// </remarks>
        #region DOC_GetPackagingUnitControlStatusExtended
        public GetPackagingUnitControlStatusExtendedResponse GetPackagingUnitControlStatusExtended(GetPackagingUnitControlStatusExtendedRequest request)
        {
            var currentMethod = "ISiplaceSetupCenterExternalControl." + MethodBase.GetCurrentMethod().Name + "()";
            _log.InfoMessage(currentMethod + " - begin");
            _log.DebugMessage("Arguments : Reason=" + request.Reason + ", " + GetString(request.PackagingUnitLocations));

            CheckAndThrowException();
            CheckForSleep();

            var returnValue = new GetPackagingUnitControlStatusExtendedResponse();
            returnValue.ExternalControlResults = new ExternalControlResult[request.PackagingUnitLocations.Length];
            try
            {
                //Adjust the data for each PU location
                for (var i = 0; i < request.PackagingUnitLocations.Length; i++)
                {
                    var currentResult = new ExternalControlResult { PackagingUnit = request.PackagingUnitLocations[i].PackagingUnit, ResultState = 1 };
                    if (MesContext.ModifyQuantityEnabled)
                        currentResult.PackagingUnit.Quantity = MesContext.NewQuantity;
                    if (MesContext.ModifyOriginalQuantityEnabled)
                        currentResult.PackagingUnit.OriginalQuantity = MesContext.NewOriginalQuantity;
                    if (MesContext.ModifyRohsEnabled)
                        currentResult.PackagingUnit.RoHS = MesContext.NewRohs;
                    if (MesContext.ModifyMsdEnabled)
                    {
                        currentResult.PackagingUnit.MsdLevel = MesContext.NewMsdLevel;
                        currentResult.PackagingUnit.MsdOpenDate = MesContext.NewMsdOpenDate;
                    }
                    if (MesContext.ModifySiplaceProComponentNameEnabled)
                    {
                        currentResult.PackagingUnit.ComponentName = MesContext.NewSiplaceProComponentName;
                        currentResult.PackagingUnit.ComponentBarcode = MesContext.NewSiplaceProComponentName;
                    }
                    returnValue.ExternalControlResults[i] = currentResult;
                }
            }
            catch (FaultException fEx)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", fEx);
                throw;
            }
            catch (Exception ex)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", ex);
                var fault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = currentMethod + " failed, on an unknown problem! Exception: " + ex.Message
                };
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
            _log.InfoMessage(currentMethod + " - end");
            return returnValue;
        }

        #endregion

        /// <summary>
        /// This method is suitable for tray material on MTC / WPC etc. If the option is enabled then the station
        /// will scan the barcode of all components on a tray carrier and request MES permission for placement.
        /// The scanning will be done by the station after Setup Center verification of the tray carrier level and 
        /// after receiving the packaging unit data from Setup Center.
        /// This enables the external MES system to control whether a component should be placed or not. 
        /// </summary>
        /// <param name="request">list of components</param>
        /// <returns> components with the status</returns>
        #region DOC_GetComponentsStatus

        public ComponentStatusResponse GetComponentsStatus(ComponentStatusRequest request)
        {
            var currentMethod = "ISiplaceSetupCenterExternalControl." + MethodBase.GetCurrentMethod().Name + "()";
            _log.InfoMessage(currentMethod + " - begin");

            CheckAndThrowException();
            CheckForSleep();

            var componentStatusResponse = new ComponentStatusResponse();

            try
            {
                var componentStatusDataTable = MesContext.ComponentStatusDataSet.Tables[0];
                var componentResult = new List<Component>();

                for (var i = 0; i < request.Components.Length; i++)
                {
                    var component = request.Components[i];
                    var components = new List<ComponentStatus>();

                    for (var j = 0; j < component.ComponentStatus.Length; j++)
                    {
                        var componentStatus = component.ComponentStatus[j];
                        var dtFoundComponent = componentStatusDataTable.Rows.Find(new[] { component.Name, componentStatus.Barcode });

                        if (dtFoundComponent != null)
                        {
                            componentStatus.VerificationStatus = Int32.Parse(dtFoundComponent["VerificationStatus"].ToString());
                            components.Add(componentStatus);
                        }
                    }

                    if (components.Count > 0)
                    {
                        var result = new Component
                        {
                            Name = component.Name,
                            ComponentStatus = components.ToArray()
                        };

                        componentResult.Add(result);
                    }
                }

                componentStatusResponse.Components = componentResult.ToArray();
            }
            catch (FaultException fEx)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", fEx);
                throw;
            }
            catch (Exception ex)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", ex);
                var fault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = currentMethod + " failed, on an unknown problem! Exception: " + ex.Message
                };
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }

            _log.InfoMessage(currentMethod + " - end");
            return componentStatusResponse;
        }

        #endregion

        /// <summary>
        /// This is an integration point, where SetupCenter will call an external system, when a barcode is been scanned in unique id mode with an unique barcode fragment, 
        /// where no packaging unit data is existing in the SetupCenter database. 
        /// This enables the customer to configure a integration scenario, where the data is been request by SetupCenter when the barcode is been scanned. 
        /// This is just in time data transfer and it must not be done in advance. 
        /// The packaging unit data is containing all properties, which have been parsed from the barcode with all containing barcode fragment information found on the reel. 
        /// The packaging unit data object is filled with all properties found during the scan operations based on the barcode configuration and scanned barcodes.
        /// </summary>
        /// <param name="packagingUnitLocations">list of component locations where the scan happens and which data was scanned.</param>
        /// <returns>The list of result of the operation for these packaging units. 
        /// The value in PackagingUnitResult indicates the values for the setup center packaging units.</returns>
        #region DOC_GetNewPackagingUnitData
        public ExternalControlResult[] GetNewPackagingUnitData(PackagingUnitLocation[] packagingUnitLocations)
        {
            var currentMethod = "ISiplaceSetupCenterExternalControl." + MethodBase.GetCurrentMethod().Name + "()";
            _log.InfoMessage(currentMethod + " - begin");
            _log.DebugMessage("Arguments: " + GetString(packagingUnitLocations));

            var returnValue = new ExternalControlResult[packagingUnitLocations.Length];
            CheckAndThrowException();
            CheckForSleep();

            try
            {
                var dtPackagingUnits = MesContext.PackagingUnitsDataSet.Tables[0];

                // Search for known PU for each ID in PU locations array
                for (var i = 0; i < packagingUnitLocations.Length; i++)
                {
                    var currentResult = new ExternalControlResult { PackagingUnit = new PackagingUnit() };
                    var packagingUnitId = packagingUnitLocations[i].PackagingUnit.UID;

                    var foundPuRow = dtPackagingUnits.Rows.Find(packagingUnitId);

                    if (foundPuRow != null)
                    {
                        // The PU ID was found - fill the packaging unit data from the found PU row
                        currentResult.PackagingUnit.BatchId = foundPuRow["BatchId"].ToString();
                        currentResult.PackagingUnit.Comment = foundPuRow["Comment"].ToString();
                        currentResult.PackagingUnit.ComponentBarcode = foundPuRow["ComponentBarcode"].ToString();
                        currentResult.PackagingUnit.ComponentName = foundPuRow["ComponentName"].ToString();
                        currentResult.PackagingUnit.ExpiryDate = DateTime.Parse(foundPuRow["ExpiryDate"].ToString(), new CultureInfo("de-DE")).ToUniversalTime();
                        currentResult.PackagingUnit.Extra1 = foundPuRow["Extra1"].ToString();
                        currentResult.PackagingUnit.Extra2 = foundPuRow["Extra2"].ToString();
                        currentResult.PackagingUnit.Extra3 = foundPuRow["Extra3"].ToString();
                        currentResult.PackagingUnit.Manufacturer = foundPuRow["Manufacturer"].ToString();
                        currentResult.PackagingUnit.ManufacturerDate = DateTime.Parse(foundPuRow["ManufactureDate"].ToString(), new CultureInfo("de-DE")).ToUniversalTime();
                        currentResult.PackagingUnit.MsdLevel = Int32.Parse(foundPuRow["MsdLevel"].ToString());
                        currentResult.PackagingUnit.MsdOpenDate = DateTime.Parse(foundPuRow["MsdOpenDate"].ToString(), new CultureInfo("de-DE")).ToUniversalTime();
                        currentResult.PackagingUnit.OriginalQuantity = Int32.Parse(foundPuRow["OriginalQuantity"].ToString());
                        currentResult.PackagingUnit.Quantity = Int32.Parse(foundPuRow["Quantity"].ToString());
                        currentResult.PackagingUnit.Supplier = foundPuRow["Supplier"].ToString();
                        currentResult.PackagingUnit.UID = packagingUnitId;
                        currentResult.PackagingUnit.GreyZone = Int32.Parse(foundPuRow["GreyZone"].ToString());
                        currentResult.PackagingUnit.LastProductionDate = DateTime.Parse(foundPuRow["LastProductionDate"].ToString(), new CultureInfo("de-DE")).ToUniversalTime();
                        currentResult.PackagingUnit.AdditionalPartInformation = foundPuRow["AdditionalPartInformation"].ToString();
                        currentResult.PackagingUnit.Batch2 = foundPuRow["Batch2"].ToString();
                        currentResult.PackagingUnit.BrightnessClass = foundPuRow["BrightnessClass"].ToString();
                        currentResult.PackagingUnit.ManufactureLocation = foundPuRow["ManufacturerLocation"].ToString();
                        currentResult.PackagingUnit.ManufacturePartNumber = foundPuRow["ManufacturerPartNumber"].ToString();
                        currentResult.PackagingUnit.OrderingCode = foundPuRow["OrderingCode"].ToString();
                        currentResult.PackagingUnit.PurchaseOrderNumber = foundPuRow["PurchaseOrderNumber"].ToString();
                        currentResult.PackagingUnit.RevisionLevel = foundPuRow["RevisionLevel"].ToString();
                        currentResult.PackagingUnit.RoHS = Int32.Parse(foundPuRow["RoHS"].ToString());
                        currentResult.PackagingUnit.Serial = foundPuRow["Serial"].ToString();
                        currentResult.PackagingUnit.ShippingNoteNumber = foundPuRow["ShippingNoteNumber"].ToString();
                        currentResult.PackagingUnit.SupplierData = foundPuRow["SupplierData"].ToString();

                        currentResult.ResultState = 1; // OK
                    }
                    else
                    {
                        // PU ID was not found - set packaging unit to null
                        currentResult.PackagingUnit = null;
                        currentResult.ResultState = 0; // unknown
                        var msg = new ExternalControlResultMessage
                        {
                            Message = "Packaging unit for id='" + packagingUnitId + "' was not found in the database!"
                        };
                        currentResult.Messages = new[] { msg };
                    }

                    returnValue[i] = currentResult;
                }

            }
            catch (FaultException fEx)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", fEx);
                throw;
            }
            catch (Exception ex)
            {
                _log.ErrorMessage(currentMethod + " failed, on an unknown problem!", ex);
                var fault = new SiplaceSetupCenterFault
                {
                    ErrorCode = 20000,
                    ExtendedMessage = currentMethod + " failed, on an unknown problem! Exception: " + ex.Message
                };
                var reason = new FaultReason(ex.ToString());
                throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
            }
            _log.InfoMessage(currentMethod + " - end");
            return returnValue;
        }
        #endregion
        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets a string that represents an array of PackagingUnitLocation.
        /// </summary>
        /// <param name="packagingUnitLocations">The packaging unit location array.</param>
        /// <returns>The string representation of the array instance.</returns>
        public static string GetString(PackagingUnitLocation[] packagingUnitLocations)
        {
            var sb = new StringBuilder();
            if (packagingUnitLocations != null)
            {
                sb.Append("[");
                foreach (var pul in packagingUnitLocations)
                {
                    sb.AppendLine();
                    sb.Append(GetString(pul));
                }
                sb.AppendLine();
                sb.Append("]");
                sb.AppendLine();
            }
            else
            {
                sb.Append("NULL");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets a string that represents a PackagingUnitLocation.
        /// </summary>
        /// <param name="packagingUnitLocation">The packaging unit location.</param>
        /// <returns>The string representation of the packaging unit location instance.</returns>
        public static string GetString(PackagingUnitLocation packagingUnitLocation)
        {
            var sb = new StringBuilder();
            if (packagingUnitLocation != null)
            {
                sb.Append("Location: ");
                sb.AppendLine();
                sb.Append(GetString(packagingUnitLocation.ComponentLocation));
                sb.AppendLine();
                sb.Append("Packaging Unit: ");
                sb.AppendLine();
                sb.Append(GetString(packagingUnitLocation.PackagingUnit));
            }
            else
            {
                sb.Append("NULL");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets a string that represents a ComponentLocation
        /// </summary>
        /// <param name="componentLocation">The component location.</param>
        /// <returns>The string representation of the component location instance.</returns>
        public static string GetString(ComponentLocation componentLocation)
        {
            var sb = new StringBuilder();
            sb.Append("(");
            if (componentLocation != null)
            {
                sb.AppendFormat("Line: '{0}'", componentLocation.Machine.LineName);
                sb.Append(", ");
                sb.AppendFormat("Machine: '{0}'", (componentLocation.Machine != null ? componentLocation.Machine.MachineName : "NULL"));
                sb.Append(", ");
                sb.AppendFormat("Docking Station: '{0}'", (componentLocation.DockingStation != null ? componentLocation.DockingStation.DockingStationId : "NULL"));
                sb.Append(", ");
                sb.AppendFormat("Location: '{0}'", componentLocation.Location);
                sb.Append(", ");
                sb.AppendFormat("Device: '{0}'", componentLocation.Device);
                sb.Append(", ");
                sb.AppendFormat("TableId: '{0}'", componentLocation.TableId);
                sb.Append(", ");
                switch (componentLocation.TableState)
                {
                    case 1:
                        sb.AppendLine("TableState: Online");
                        break;
                    case 2:
                        sb.AppendLine("TableState: Offline");
                        break;
                    default:
                        sb.AppendLine("TableState: Unknown");
                        break;
                }
                sb.Append(", ");
                sb.AppendFormat("Feeder Start Track: '{0}'", componentLocation.Track);
                sb.Append(", ");
                sb.AppendFormat("Feeder ID: '{0}'", componentLocation.FeederId);
                sb.Append(", ");
                sb.AppendFormat("Feeder Type: '{0}'", componentLocation.FeederType);
                sb.Append(", ");
                sb.AppendFormat("Tower: '{0}'", componentLocation.Tower);
                sb.Append(", ");
                sb.AppendFormat("Level: '{0}'", componentLocation.Level);
                sb.Append(", ");
                sb.AppendFormat("Division: '{0}'", componentLocation.Division);
                sb.Append(", ");
                sb.AppendFormat("StationTrack: '{0}'", componentLocation.StationTrack);
            }
            else
            {
                sb.Append("NULL");
            }
            sb.Append(")");

            return sb.ToString();
        }

        /// <summary>
        /// Gets a string that represents an array of PackagingUnits.
        /// </summary>
        /// <param name="packagingUnits">The packaging unit array.</param>
        /// <returns>The string representation of the array instance.</returns>
        public static string GetString(PackagingUnit[] packagingUnits)
        {
            var sb = new StringBuilder();
            if (packagingUnits != null)
            {
                sb.Append("[");
                foreach (var pu in packagingUnits)
                {
                    sb.AppendLine();
                    sb.Append(GetString(pu));
                }
                sb.AppendLine();
                sb.Append("]");
                sb.AppendLine();
            }
            else
            {
                sb.Append("NULL");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets a string that represents a PackagingUnit/>.
        /// </summary>
        /// <param name="pu">The PackagingUnit.</param>
        /// <returns>The string representation of the PackagingUnit/> instance.</returns>
        public static string GetString(PackagingUnit pu)
        {
            var sb = new StringBuilder();
            sb.Append("(");

            if (pu != null)
            {
                sb.AppendFormat("UID: '{0}'", pu.UID);
                sb.Append(", ");
                sb.AppendFormat("Component: '{0}'", pu.ComponentName);
                sb.Append(", ");
                sb.AppendFormat("Comp BC: '{0}'", pu.ComponentBarcode);
                sb.Append(", ");
                sb.AppendFormat("Original quantity: '{0}'", pu.OriginalQuantity);
                sb.Append(", ");
                sb.AppendFormat("Quantity: '{0}'", pu.Quantity);
                sb.Append(", ");
                sb.AppendFormat("Greyzone: '{0}'", pu.GreyZone);
                sb.Append(", ");
                sb.AppendFormat("MSD level: '{0}'", pu.MsdLevel);
                sb.Append(", ");
                sb.AppendFormat("MSD open date: '{0}'", pu.MsdOpenDate.ToString("o"));
                sb.Append(", ");
                sb.AppendFormat("Expiry date: '{0}'", pu.ExpiryDate.ToString("o"));
                sb.Append(", ");
                sb.AppendFormat("Extra1: '{0}'", pu.Extra1);
                sb.Append(", ");
                sb.AppendFormat("Extra2: '{0}'", pu.Extra2);
                sb.Append(", ");
                sb.AppendFormat("Extra3: '{0}'", pu.Extra3);
                sb.Append(", ");
                sb.AppendFormat("Supplier: '{0}'", pu.Supplier);
                sb.Append(", ");
                sb.AppendFormat("Manufacturer: '{0}'", pu.Manufacturer);
                sb.Append(", ");
                sb.AppendFormat("Manufacture date: '{0}'", pu.ManufacturerDate.ToString("o"));
                sb.Append(", ");
                sb.AppendFormat("Consumption date: '{0}'", pu.ConsumptionDate.ToString("o"));
                sb.Append(", ");
                sb.AppendFormat("LastProduction date: '{0}'", pu.LastProductionDate.ToString("o"));
                sb.Append(", ");
                sb.AppendFormat("Batch: '{0}'", pu.BatchId);
                sb.Append(", ");
                sb.AppendFormat("BatchPU (UID/Q): '{0}/{1}'", (pu.BatchPackagingUnit != null ? pu.BatchId : "<NULL>"), (pu.BatchPackagingUnit != null ? pu.BatchPackagingUnit.Quantity.ToString(CultureInfo.InvariantCulture) : "-"));
                sb.Append(", ");

                sb.AppendFormat("AdditionalPartInformation: '{0}'", pu.AdditionalPartInformation);
                sb.Append(", ");
                sb.AppendFormat("Batch2: '{0}'", pu.Batch2);
                sb.Append(", ");
                sb.AppendFormat("BrightnessClass: '{0}'", pu.BrightnessClass);
                sb.Append(", ");
                sb.AppendFormat("ManufactureLocation: '{0}'", pu.ManufactureLocation);
                sb.Append(", ");
                sb.AppendFormat("ManufacturePartNumber: '{0}'", pu.ManufacturePartNumber);
                sb.Append(", ");
                sb.AppendFormat("OrderingCode: '{0}'", pu.OrderingCode);
                sb.Append(", ");
                sb.AppendFormat("PurchaseOrderNumber: '{0}'", pu.PurchaseOrderNumber);
                sb.Append(", ");
                sb.AppendFormat("RevisionLevel: '{0}'", pu.RevisionLevel);
                sb.Append(", ");
                sb.AppendFormat("RoHS: '{0}'", pu.RoHS);
                sb.Append(", ");
                sb.AppendFormat("Serial: '{0}'", pu.Serial);
                sb.Append(", ");
                sb.AppendFormat("ShippingNoteNumber: '{0}'", pu.ShippingNoteNumber);
                sb.Append(", ");
                sb.AppendFormat("SupplierData: '{0}'", pu.SupplierData);
                sb.Append(", ");

                if (pu.LockInfos != null)
                {
                    foreach (LockInfo li in pu.LockInfos)
                    {
                        sb.AppendFormat("	Date: '{0}'", li.Date);
                        sb.Append(", ");
                        sb.AppendFormat("Source: '{0}'", li.Source);
                        sb.Append(", ");
                        sb.AppendFormat("Reason: '{0}'", li.Reason);
                        sb.Append(", ");
                        sb.AppendFormat("Message: '{0}'", li.Message);
                        sb.Append(", ");
                    }
                }

                sb.AppendFormat("Comment: '{0}'", pu.Comment);

                if (pu.SplicedPackagingUnit != null)
                {
                    sb.Append(" -> " + GetString(pu.SplicedPackagingUnit));
                }
            }
            else
            {
                sb.Append("NULL");
            }
            sb.Append(")");

            return sb.ToString();
        }
        #endregion

        #region Private methdos
        /// <summary>
        /// Checks if an exception must be thrown during the execution
        /// </summary>
        private void CheckAndThrowException()
        {
            if (!MesContext.ThrowExceptionEnabled)
                return;

            _log.ErrorMessage("The MES Tutorial was configured to throw this exception!");
            var fault = new SiplaceSetupCenterFault
            {
                ErrorCode = 10000,
                ExtendedMessage = "The MES Tutorial was configured to throw this exception!"
            };
            var reason = new FaultReason("MES Tutorial application exception occured");
            throw new FaultException<SiplaceSetupCenterFault>(fault, reason);
        }

        /// <summary>
        /// Checks if the execution must be delayed
        /// </summary>
        private void CheckForSleep()
        {
            if (!MesContext.SleepEnabled)
                return;

            _log.InfoMessage(string.Format("The MES Tutorial was confgured to sleep '{0}' seconds on each ISiplaceSetupCenterExternalControl call ...", MesContext.Sleep));
            Thread.Sleep(MesContext.Sleep * 1000);
        }
        #endregion
    }
}
