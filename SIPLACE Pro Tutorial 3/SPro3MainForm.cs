//-----------------------------------------------------------------------
// <copyright file="SPro3MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespaces
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using OIB.Tutorial.Common.Logging;

using ArchObj = Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using BizObj = Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using ProxyTypes = Asm.As.Oib.SiplacePro.Proxy.Types;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Notifications.SubscriptionService;
using BizTypes = Asm.As.Oib.SiplacePro.Proxy.Business.Types;
using BizInt = Asm.As.Oib.SiplacePro.Proxy.Business.Interfaces;
#endregion

namespace OIB.Tutorial.SiplacePro._3
{
    public partial class SPro3MainForm : Form, ILog
    {
        #region Fields
        // Constants
        private const string SproFolderPath = @"OIB\Tutorial\SPI\3";
        private const string ComponentShapePath = @"System\100";
        private const string BoardName = "Board01";
        private const string ComponentNameFormat = "BE{0}";
        private const string PlacementListName = "PlacementList01";
        private const int ComponentCount = 10;

        private bool _connected;

        private ArchObj.Session _session;
        #endregion

        #region Constructor
        public SPro3MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Implementation of ILog
        public void InfoMessage(string message)
        {
            loggerListView.InfoMessage(message);
        }

        private void InfoMessage(string message, params object[] formatArgs)
        {
            InfoMessage(string.Format(message, formatArgs));
        }

        public void ErrorMessage(string message, Exception ex = null)
        {
            loggerListView.ErrorMessage(message, ex);
        }

        public void WarnMessage(string message, Exception ex = null)
        {
            loggerListView.WarnMessage(message, ex);
        }

        public void DebugMessage(string message, Exception ex = null)
        {
            loggerListView.DebugMessage(message, ex);
        }
        #endregion

        #region Event Handlers
        private void connectSProButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Set the address as the current session endpoint 
                ArchObj.SessionManager.CurrentSessionEndpoint = new EndpointAddress(new Uri(SProEndpointTextBox.Text));

                // Set the base address for callbacks (events) of the SPI adapter
                ArchObj.SessionManager.CurrentCallbackEndpointBase = string.Format("net.tcp://{0}:1406/OibSpiTutorial_3", Environment.MachineName);

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                _session = ArchObj.Session.CurrentSession;

                InfoMessage(string.Format("Successfully connected to SIPLACE Pro Adapter – '{0}'", SProEndpointTextBox.Text));
                InfoMessage(string.Format("Session ID: {0}, Database ID: '{1}'", _session.SessionId, _session.DatabaseId));
                InfoMessage(string.Format("SPI Version: '{0}', Model Version: '{1}', Product Version: '{2}'", 
                                          _session.SPIVersion, _session.ModelVersion, _session.ProductVersion));
                InfoMessage(string.Format("OIB Adapter Version: '{0}', OPIB Proxy version: '{1}'", 
                                          _session.OibSiplaceProAdapterVersion, _session.OibSiplaceProProxyVersion));
                _connected = true;
            }
            catch (Exception ex)
            {
                ErrorMessage("Error while connecting SIPLACE Pro!", ex);
                _connected = false;
                _session = null;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            connectSProButton.BackColor = _connected ? Color.Green : Color.Red;
        }

        private void subscribePropertiesUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_SubscribedPropertyUpdate
                _session.SubscriptionService.SubscribedPropertyUpdate += SubscriptionService_SubscribedPropertyUpdate;
                if (listenDownloadEventCheckBox.Checked)
                {
                    _session.SubscriptionService.SubscribeToPropertyUpdate
                            (ProxyTypes.PropertyId.LastDownloadRecipeData_RecipeTimeStamp);
                }
                else
                {
                    foreach (var propertyid in
                        Enum.GetValues(
                            typeof(ProxyTypes.PropertyId)).Cast<ProxyTypes.PropertyId>().Where(
                                propertyid => 
                                    _session.SubscriptionService.IsValidPropertyUpdate(propertyid)))
                    {
                        _session.SubscriptionService.SubscribeToPropertyUpdate(propertyid);
                    }
                }
                #endregion

                unsubscribePropertiesUpdateButton.Enabled = true;
                subscribePropertiesUpdateButton.Enabled = false;

                InfoMessage("Successfully subscribed to property updates");
            }
            catch (Exception ex)
            {
                ErrorMessage("Error subscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SubscriptionService_SubscribedPropertyUpdate(object sender, PropertyUpdateArgs args)
        {
            try
            {
                InfoMessage(string.Format("Property Update: '{0}', new value: '{1}', Identity: '{2}'", args.PropertyId, args.Value, args.Identity.FullPath));

                #region DOC_EvaluateDownloadEvent
                // This property is used to detect a download on a station
                if (args.PropertyId == ProxyTypes.PropertyId.LastDownloadRecipeData_RecipeTimeStamp)
                {
                    #region Use Client Authentication

                    // Usage of client authentication
                    if (_cbUseClientAuthentication.Checked)
                    {
                        // Add client certificate to authenticate to service

                        // **** IMPORTANT ****
                        //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                        //- uncomment to use client authentication, but consider these values overrides app.config settings
                        //ArchObj.Session.SetCertificateForClientAuthentication(
                        //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                    }

                    #endregion

                    var session = ArchObj.Session.CurrentSession;

                    // Get the changed object
                    var lcObject = session.ProductionDataManager.LineControlData.FindObject(args.OID_Long);
                    if (lcObject != null)
                    {
                        // Cast the object to the expected LastDownloadRecipeData type.
                        // LastDownloadRecipeData contains download data of one station on one conveyor
                        // such as name of the recipe and the timestamp of the download.
                        var ldRecipeData = (BizObj.LastDownloadRecipeData)lcObject;
                        // Get the parent object which contains data of 
                        // the last setup download for the specific station.
                        var ldDownloadData = (BizObj.DownloadData)lcObject.Parent;

                        // Find out the station this download event belongs to:
                        // the ProductionDataManager.LineControlData contains 
                        // a dictionary with stationId - DownloadData pairs.
                        // Lets find the pair for our own download data object
                        var stationId =
                            (
                            from kvp 
                            in session.ProductionDataManager.LineControlData.DownloadData 
                            where kvp.Value.Equals(ldDownloadData) 
                            select kvp.Key
                            ).FirstOrDefault();
                        // you need to check for null, just in case the station is not found
                        var stationFullPath = stationId != null ? stationId.FullPath : string.Empty;
                        // determine the conveyor
                        var conveyor = ldDownloadData.LastDownloadRecipeData1 == ldRecipeData ? "Right" : "Left";

                        InfoMessage(
                            string.Format("DOWNLOAD DETECTED: Recipe '{0}' on Station: '{1}', Conveyor: '{2}'", 
                                          ldRecipeData.RecipeDisplayPath, stationFullPath, conveyor));
                    }
                    else
                    {
                        ErrorMessage(string.Format("Can't find the object ID '{0}' in LineControlData.", args.OID_Long));
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ErrorMessage("PropertyUpdate handler failed!", ex);
            }
        }

        private void unsubscribePropertiesUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_UNSUBSCRIBE_PROPERTY_UPDATE
                // just unsubscribe all property updates
                foreach (var propertyid in
                    Enum.GetValues(typeof(ProxyTypes.PropertyId)).Cast<ProxyTypes.PropertyId>().Where(propertyid => 
                                                                                                _session.SubscriptionService.IsValidPropertyUpdate(propertyid)))
                {
                    _session.SubscriptionService.UnsubscribeToPropertyUpdate(propertyid);
                }

                _session.SubscriptionService.SubscribedPropertyUpdate -= SubscriptionService_SubscribedPropertyUpdate;
                #endregion

                unsubscribePropertiesUpdateButton.Enabled = false;
                subscribePropertiesUpdateButton.Enabled = true;

                InfoMessage("Successfully unsubscribed to property updates");
            }
            catch (Exception ex)
            {
                ErrorMessage("Error unsubscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void createComponentsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                for (var i = 0; i < ComponentCount; i++)
                {
                    var currentComponentName = string.Format(ComponentNameFormat, i);
                    GetOrCreateComponent(_session, currentComponentName);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage("Error creating components!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void createBoardButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                CreateBoard(_session, BoardName);
            }
            catch (Exception ex)
            {
                ErrorMessage("Error creating the board!", ex);
                //_session.Rollback();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void deleteAllSpiObjectsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                DeleteAllSpiObjects(_session);
            }
            catch (Exception ex)
            {
                ErrorMessage("Error creating the board!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SPro3MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }
        }

        private void SPro3MainForm_Resize(object sender, EventArgs e)
        {
            AdjustSplitterPosition();    
        }

        private void SPro3MainForm_Shown(object sender, EventArgs e)
        {
            AdjustSplitterPosition();
        }

        private void readSetupButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_connected)
                {
                    ErrorMessage("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                ReadSetup(_session);
            }
            catch (Exception ex)
            {
                ErrorMessage("Error reading setup!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Private Methods
        private BizObj.Component GetOrCreateComponent(ArchObj.Session session, string componentName)
        {
            // Create or get the folder
            const string componentFolderFullTypePath = "Component:" + SproFolderPath;
            // Create folder only in case it does not already exist
            var folder = session.GetFolder(componentFolderFullTypePath) ?? session.CreateFolder(componentFolderFullTypePath);

            // Create component only in case object does not already exist
            var componentFullName = string.Format(@"{0}\{1}", SproFolderPath, componentName);
            BizObj.Component component;
            var id = session.GetIdentity(componentFullName, ProxyTypes.ObjectServerType.Component);
            if (id == null)
            {
                component = (BizObj.Component)session.CreateObject(folder, componentName);
                var componentShapeId = session.GetIdentity(ComponentShapePath, ProxyTypes.ObjectServerType.ComponentShape);
                if (componentShapeId != null)
                    component.ComponentShape = (BizObj.ComponentShape)componentShapeId.Object;
                else
                    WarnMessage(string.Format("The Component Shape '{0}' was not found! Please Import the 'Standard Shape Library'", ComponentShapePath));

                InfoMessage(string.Format("Component '{0}' created", component.FullPath));
            }
            else
            {
                InfoMessage(string.Format("Component '{0}' already exists", id.FullPath));
                component = (BizObj.Component) id.Object;
            }
            return component;
        }

        #region DOC_CreateBoard
        private void CreateBoard(ArchObj.Session session, string boardName)
        {
            // Create or get the folder
            const string boardFolderFullTypePath = "Board:" + SproFolderPath;
            // Create folder only in case it does not already exist
            var folder = 
                session.GetFolder(boardFolderFullTypePath) ?? 
                        session.CreateFolder(boardFolderFullTypePath);

            // Create board only in case object does not already exist
            var boardFullName = string.Format(@"{0}\{1}", SproFolderPath, boardName);
            var id = session.GetIdentity(boardFullName, ProxyTypes.ObjectServerType.Board);
            if (id == null)
            {
                var board = (BizObj.Board)session.CreateObject(folder, boardName);
                board.Length = 55;
                board.Width = 55;
                board.TopSide.OffsetZeroPointX = 0.1;
                board.TopSide.OffsetZeroPointY = 0.1;
                board.TopSide.Name = "New Top";
                board.TopSide.PlacementList = GetOrCreatePlacementList(session, PlacementListName);

                InfoMessage(string.Format("Board '{0}' created", board.FullPath));
            }
            else
                InfoMessage(string.Format("Board '{0}' already exists", id.FullPath));    
        }
        #endregion

        #region DOC_GetOrCreatePlacementList
        private BizObj.PlacementList GetOrCreatePlacementList(ArchObj.Session session, string placementListName)
        {
            // Create or get the folder
            const string placementListFolderFullTypePath = "PlacementList:" + SproFolderPath;
            // Create folder only in case it does not already exist
            var folder = session.GetFolder(placementListFolderFullTypePath) ?? 
                            session.CreateFolder(placementListFolderFullTypePath);

            // Create placement list only in case object does not already exist
            var placementListFullName = string.Format(@"{0}\{1}", SproFolderPath, placementListName);
            var id = 
                session.GetIdentity(
                                placementListFullName,
                                ProxyTypes.ObjectServerType.PlacementList);
            if (id == null)
            {
                var placementList = 
                    (BizObj.PlacementList)session.CreateObject(folder, placementListName);
                id = placementList.Identity;

                for (var i = 0; i < ComponentCount; i++)
                {
                    var placement = new BizObj.ComponentPlacement(session);
                    var componentName = string.Format(ComponentNameFormat, i);
                    placement.Component = GetOrCreateComponent(session, componentName);
                    placement.OffsetX = i+1;
                    placement.OffsetY = i+1;
                    placementList.ComponentPlacements.Add(string.Format("POS_{0}", i), placement);
                }

                InfoMessage(string.Format("Placement List '{0}' created", placementList.FullPath));
            }
            return (BizObj.PlacementList)id.Object;
        }
        #endregion

        private void DeleteAllSpiObjects(ArchObj.Session session)
        {
            // Get the folders
            const string boardFolderFullTypePath = "Board:" + SproFolderPath;
            var boardFolder = session.GetFolder(boardFolderFullTypePath);
            
            const string placementListFolderFullTypePath = "PlacementList:" + SproFolderPath;
            var placementListFolder = session.GetFolder(placementListFolderFullTypePath);

            const string componentFolderFullTypePath = "Component:" + SproFolderPath;
            var componentFolder = session.GetFolder(componentFolderFullTypePath);

            if (boardFolder != null)
            {
                DeleteObjects(session, session.GetIdentities(boardFolderFullTypePath + @"\*"));
                DeleteFolderAndAllParentsFolder(session, boardFolder);
            }

            if (placementListFolder != null)
            {
                DeleteObjects(session, session.GetIdentities(placementListFolderFullTypePath + @"\*"));
                DeleteFolderAndAllParentsFolder(session, placementListFolder);
            }

            if (componentFolder != null)
            {
                DeleteObjects(session, session.GetIdentities(componentFolderFullTypePath + @"\*"));
                DeleteFolderAndAllParentsFolder(session, componentFolder);
            }
        }

        private void DeleteObjects(ArchObj.Session session, List<ArchObj.Identity> identities)
        {
            identities.ForEach(i =>
                                   {
                                       var identityFullPath = i.FullPath;
                                       var identityType = i.Object.ObjectType;
                                       try
                                       {
                                           session.DeleteObject(i);
                                           InfoMessage(string.Format("The SPI Object '{0}: {1}' deleted", identityType, identityFullPath));
                                       }
                                       catch (Exception ex)
                                       {
                                           ErrorMessage(string.Format("Failed to delete the SPI Object '{0}: {1}'!", i.Object.ObjectType, i.FullPath), ex);
                                       }
                                   });
        }

        private void DeleteFolderAndAllParentsFolder(ArchObj.Session session, BizObj.Folder folder)
        {
            if (folder == null)
                throw new ArgumentNullException("folder");

            var foldersToBeDeleted = new List<BizObj.Folder>();

            var currentFolder = folder;
            while (currentFolder.Name != string.Empty && currentFolder.ParentFolder != null)
            {
                foldersToBeDeleted.Add(currentFolder);
                currentFolder = currentFolder.ParentFolder;
            }
            foldersToBeDeleted.ForEach(f =>
                                           {
                                               var folderType = f.FolderType;
                                               var folderFullPath = f.FullPath;
                                               try
                                               {
                                                   session.DeleteFolder(f);
                                                   InfoMessage(string.Format("Folder '{0}: {1}' deleted", folderType, folderFullPath));
                                               }
                                               catch (Exception ex)
                                               {
                                                   WarnMessage(string.Format("Failed to delete folder '{0}: {1}'", folderType, folderFullPath), ex);
                                               }
                                           });
        }

        #region DOC_ReadSetup
        private void ReadSetup(ArchObj.Session session)
        {
            var setupPath = setupPathTextBox.Text;
            var setup = (BizObj.Setup)session.GetObject(setupPath, ProxyTypes.ObjectServerType.Setup);
            if (setup == null)
            {
                ErrorMessage(
                    string.Format("Couldn't load the Setup '{0}' from SIPLACE Pro. Please check if the Setup exists.", 
                                  setupPath));
                return;
            }

            loggerListView.SuspendLayout();
            InfoMessage("------------- Begin Setup '{0}' -------------", setup.FullPath);
            InfoMessage("Line: '{0}'", setup.Line.FullPath);
            InfoMessage("Whisper down the line modes - Inkspots and Panel Fiducials: '{0}', PCB Barcodes: '{1}', WDTL Capable: '{2}'",
                        setup.WhisperDownTheLineMode, setup.WhisperPCBBarcodes, setup.IsWDTLCapable);
            foreach (var stationSetup in setup.StationSetups.ValueList)
            {
                InfoMessage("=== Begin Station '{0}' ===", stationSetup.Station.FullPath);
                var stationInLine = 
                    setup.Line.StationInLines.FirstOrDefault(
                            stInLn => stationSetup.Station.FullPath.Equals(stInLn.Station.FullPath));
                if (stationInLine == null)
                {
                    ErrorMessage(
                        string.Format("Couldn't find the StationInLine object for station '{0}'", 
                                      stationSetup.Station.FullPath));
                    continue;
                }

                #region Read Station Tab
                // Station Tab
                InfoMessage("Station Configuration: '{0}'", 
                            stationSetup.StationTopologyConfiguration);
                InfoMessage("PCB Camera '{0}'", stationSetup.PCBCamera);
                InfoMessage("Setup Verification System: '{0}'", stationSetup.BarcodeSystemTypeName);
                InfoMessage("Random Setup: '{0}'", stationSetup.ArbitrarySetupMode);
                InfoMessage(
                    stationSetup.EndOfCluster ? 
                        "This machine is at the end of a cluster OR not in a cluster." : 
                        "This machine is NOT at the end of a cluster.");
                InfoMessage("Whisper down the machine: '{0}'", stationSetup.WhisperDownTheMachine);
                InfoMessage("PCB pass-through mode: '{0}'", stationSetup.OmitStation);
                InfoMessage("Smart Pin Support - Hardware Configuration: '{0}', Enabled: '{1}'", 
                            stationSetup.SupportPinModuleType, 
                            stationSetup.IsSupportPinModuleActive);
                #endregion

                #region Read Conveyor Tab
                // Conveyor Tab
                InfoMessage("== Conveyor Begin ==");
                InfoMessage("PCB Transport - Transport: '{0}', Long Board: '{1}', Heavy Board: '{2}',"+
                            " Clearance under Board: '{3}'",
                            stationSetup.PCBTransport, stationSetup.SlowConveyorMode, 
                            stationSetup.HeavyBoardConveyorMode,
                            stationSetup.StationConveyorSystemSetup.ConveyorClearanceUnderBoard);
                InfoMessage("Fixed rail orientation: '{0}'", stationInLine.StationConveyorSystem.FixedRailOrientation);
                InfoMessage("Fixed Rail Position - Left Lane: Enabled '{0}', Lane '{1}' mm , Distance: '{2}' mm;"+
                            " Right Lane: Enabled '{3}', Lane '{4}' mm",
                            stationSetup.FixedRailPositionRightConveyorLaneEnabled,
                            stationSetup.FixedRailPositionLeftConveyorLane, 
                            stationSetup.FixedConveyorRailDistance,
                            stationSetup.FixedRailPositionRightConveyorLaneEnabled, 
                            stationSetup.FixedRailPositionRightConveyorLane);
                InfoMessage("== Conveyor End ==");
                #endregion

                #region Read Placement Mode Tab
                // Placement Mode Tab
                InfoMessage("== Placement Mode Begin ==");
                InfoMessage("Conveyor mode: '{0}'", 
                            stationInLine.StationConveyorSystem.ConveyorMode);
                InfoMessage("Placement Mode - Entire Machine: '{0}', Front: '{1}', Back: '{2}'",
                            stationSetup.StationConveyorSystemSetup.PlacementMode,
                            stationSetup.StationConveyorSystemSetup.FrontPlacingAreaPlacementMode,
                            stationSetup.StationConveyorSystemSetup.BackPlacingAreaPlacementMode);
                InfoMessage("== Placement Mode End ==");
                #endregion

                #region Read Long Board Tab
                // Long Board Tab
                InfoMessage("== Long Board Begin ==");

                var placementAreaNum = 1;
                foreach (var placementArea in stationSetup.PlacingAreas.Values)
                {
                    InfoMessage("Long Board - Placement Area {0}", placementAreaNum);
                    var stopperNum = 1;
                    foreach (var stopperInfo in placementArea.StopEdge.StopperInfos.Values)
                    {
                        InfoMessage(" -Stopper {0} at position {1} mm, Use for optimization: '{2}'", stopperNum++, stopperInfo.PositionX, stopperInfo.IsActive);
                    }
                    placementAreaNum++;
                }
                InfoMessage("== Long Board End ==");
                #endregion
                
                #region Read Custom Placement Area Ranges Tab
                // Custom Placement Area Ranges Tab
                InfoMessage("== Custom Placement Area Ranges Begin ==");
                InfoMessage("Enable custom placement area ranges: '{0}'", 
                            stationSetup.IsCustomPlacementRangeEnabled);
                if (stationSetup.IsCustomPlacementRangeEnabled)
                {
                    placementAreaNum = 1;
                    foreach (var placesingArea in stationSetup.PlacingAreas.Values)
                    {
                        InfoMessage("Placement Area {0} - Trailing edge limit: '{1}' mm, Leading edge limit: '{2}' mm", 
                                    placementAreaNum++, 
                                    placesingArea.CustomPlacementRangeLimit.TrailingEdgeOffsetPlacementOrigin,
                                    placesingArea.CustomPlacementRangeLimit.LeadingEdgeOffsetPlacementOrigin);
                    }
                }
                InfoMessage("== Custom Placement Area Ranges End ==");
                #endregion

                #region Read station locations
                foreach (var stationLocation in stationSetup.StationLocations)
                {
                    InfoMessage("== Location{0}, {1} Begin ==", 
                                stationLocation.Value.Number, stationLocation.Key.LocationType);

                    if (stationLocation.Value.AttachableHead != null)
                    {
                        InfoMessage("Head Configuration - By-Pass: '{0}', Head type: '{1}', Head height: '{2}'", 
                                    stationLocation.Value.AttachableHead.OmitHead,
                                    stationLocation.Value.AttachableHead.StaticAttachableHead.Type,
                                    stationLocation.Value.AttachableHead.StaticAttachableHead.HeadPositions[
                                        stationLocation.Value.AttachableHead.HeadPositionIndex].GlobalHeadPositionId);
                    }
                    else
                    {
                        InfoMessage("Head Configuration - Head type: '(None)'");
                    }
                    InfoMessage(
                        "Cameras: Head-mounted camera: '{0}', Stationary camera: '{1}', Flip chip camera: '{2}', Coplanarity mudule: '{3}', Component sensor: '{4}'",
                        stationLocation.Value.HeadMountedComponentCamera != null
                            ? stationLocation.Value.HeadMountedComponentCamera.StaticCamera.Type.ToString()
                            : "(None)",
                        stationLocation.Value.StationaryComponentCamera != null
                            ? stationLocation.Value.StationaryComponentCamera.StaticCamera.Type.ToString()
                            : "(None)",
                        stationLocation.Value.FlipChipCamera != null ? 
                            stationLocation.Value.FlipChipCamera.StaticCamera.Type.ToString() : 
                            "(None)",
                        stationLocation.Value.CoplanCamera != null ? 
                            stationLocation.Value.CoplanCamera.StaticCamera.Type.ToString() : 
                            "(None)",
                        stationLocation.Value.ComponentSensor != null ? 
                            stationLocation.Value.ComponentSensor.StaticCamera.Type.ToString() : 
                            "(None)");

                    InfoMessage("Table Configuration - Table Insert: '{0}', Table: '{1}', Table type: '{2}'", 
                                stationLocation.Value.TableInsert.TableInsertType, 
                                stationLocation.Value.Table.FullPath, 
                                stationLocation.Value.Table.Type);
                    
                    #region Read Nozzle Changers
                    InfoMessage("= Nozzle Changers: Begin =");
                    foreach (var nozzleChanger in stationLocation.Value.NozzleChangers)
                    {
                        InfoMessage("- Row {0}, Type: '{1}', Allocate per Pocket: '{2}' Begin -", 
                                    nozzleChanger.Value.StaticNozzleChanger.RowNumber,
                                    nozzleChanger.Value.StaticNozzleChanger.Type, 
                                    nozzleChanger.Value.NozzlePerReceptacle);
                        foreach (var nozzleMagazine in nozzleChanger.Value.AdjustableNozzleMagazines.Values)
                        {
                            InfoMessage("Nozzle Magazine {0} - Type{1}, Fixed: '{2}'",
                                        nozzleMagazine.MagazineTray, nozzleMagazine.MagazineType, 
                                        nozzleMagazine.Fixed);
                            foreach (var nozzle in nozzleMagazine.Nozzles)
                            {
                                InfoMessage("Pocket: '{0}', Nozzle Type: '{1}', Fixed: '{2}'", 
                                            nozzle.Key.SequenceNumber + 1, 
                                            nozzle.Value.NozzleType.Name, 
                                            nozzle.Value.Fixed);
                            }
                        }
                        InfoMessage("- Row {0}, Type: '{1}', Allocate per Pocket: '{2}' End -", 
                                    nozzleChanger.Value.StaticNozzleChanger.RowNumber,
                                    nozzleChanger.Value.StaticNozzleChanger.Type, 
                                    nozzleChanger.Value.NozzlePerReceptacle);
                    }
                    InfoMessage("= Nozzle Changers: End =");
                    #endregion

                    if (stationLocation.Value.AttachableHead != null)
                    {
                        InfoMessage("= Nozzle Configuration Begin =");
                        foreach (var segment in stationLocation.Value.AttachableHead.Segments)
                        {
                            InfoMessage("Segment: '{0}', Nozzle: '{1}', Segment Info: '{2}'", 
                                        segment.SegmentIndex + 1,
                                        segment.NozzleType != null ? 
                                            segment.NozzleType.Name : 
                                            "-", 
                                        segment.SegmentInfo);
                        }
                        InfoMessage("= Nozzle Configuration End =");
                    }
                    
                    // Read Table
                    var table = stationLocation.Value.TableInsert != null ? stationLocation.Value.TableInsert.Table : null;
                    ReadTableProperties(table);
                    ReadTableFeeders(table);
                    
                    InfoMessage("== Location{0}, {1} End ==", 
                                stationLocation.Value.Number, stationLocation.Key.LocationType);

                }
                #endregion

                InfoMessage("=== End Station '{0}' ===", stationSetup.Station.FullPath);
            }
            InfoMessage("------------- End Setup '{0}' -------------", setup.FullPath);
            loggerListView.ResumeLayout();
        }
        #endregion

        private void ReadTableProperties(BizObj.Table table)
        {
            if (table == null)
            {
                return;
            }

            InfoMessage("= Table '{0}' Properties Begin =", table.Name);
            InfoMessage("Freez Table: '{0}'", table.Frozen);
            InfoMessage("Constant Table: '{0}'", table.IsConstant);
            if (table.IsInSplitMode)
            {
                InfoMessage("Split Table: '{0}', Split Level: '{1}'", table.IsInSplitMode, table.SplitLevel);
            }
            else
            {
                InfoMessage("Split Table: '{0}'", table.IsInSplitMode);
            }
            InfoMessage("= Table '{0}' Properties End =", table.Name);
        }

        #region DOC_ReadTableFeeders
        private void ReadTableFeeders(BizObj.Table table)
        {
            if (table == null)
                return;

            InfoMessage("= Table '{0}' Feeders Begin =", table.Name);
            foreach (KeyValuePair<int, BizInt.IFeeder> keyValuePair in table.FeederList)
            {
                var feeder = keyValuePair.Value;
                int startTrack = keyValuePair.Key;
                switch (feeder.Species)
                {
                    case BizTypes.FeederSpecies.Tape:
                        var tapeFeeder = (BizObj.TapeFeeder)feeder;
                        InfoMessage("Track: '{0}', Feeder: '{1}'", startTrack, tapeFeeder.IFeederType);
                        foreach (var kvp in tapeFeeder.TapeReserves)
                        {
                            BizObj.TapeReserve reserve = kvp.Value;
                            BizObj.TapeFixedReserve fixedReserve = kvp.Key;
                            int index = tapeFeeder.Type.TapeFixedReserves.IndexOf(fixedReserve);

                            InfoMessage(" - Division: '{0}, Component: '{1}', Omitted: '{2}'",
                                        index + 1,
                                        reserve.Component != null ? reserve.Component.FullPath : "none",
                                        reserve.Component != null ? reserve.Component.Omit.ToString() : "none");
                        }
                        break;

                    case BizTypes.FeederSpecies.Linear:
                        var linearFeeder = (BizObj.LinearFeeder)feeder;
                        InfoMessage("Track: '{0}', Feeder: '{1}'", startTrack, linearFeeder.IFeederType);
                        foreach (var kvp in linearFeeder.LinearReserves)
                        {
                            BizObj.LinearReserve reserve = kvp.Value;
                            BizObj.LinearFixedReserve fixedReserve = kvp.Key;
                            int index = linearFeeder.Type.LinearFixedReserves.IndexOf(fixedReserve);

                            InfoMessage(" - Division: '{0}, Component: '{1}', Omitted: '{2}'",
                                        index + 1,
                                        reserve.Component != null ? reserve.Component.FullPath : "none",
                                        reserve.Component != null ? reserve.Component.Omit.ToString() : "none");
                        }
                        break;

                    case BizTypes.FeederSpecies.Tray:
                        var trayFeeder = (BizObj.TrayFeeder)feeder;
                        InfoMessage("Track: '{0}', Feeder: '{1}'", startTrack, trayFeeder.IFeederType);
                        ReadTowerCarrier(1, trayFeeder.Carriers);
                        break;

                    case BizTypes.FeederSpecies.MatrixTray:
                        var matrixFeeder = (BizObj.MatrixTrayFeeder)feeder;
                        InfoMessage("Track: '{0}', Feeder: '{1}'", startTrack, matrixFeeder.IFeederType);

                        ReadTowerCarrier(1, matrixFeeder.Tower1Carriers);
                        ReadTowerCarrier(2, matrixFeeder.Tower2Carriers);
                        break;

                    default:
                        WarnMessage(string.Format("Unknown feeder species - '{0}'", feeder.Species));
                        break;
                }
            }
            InfoMessage("= Table '{0}' Feeders End =", table.Name);
        }
        #endregion

        private void ReadTowerCarrier(int tower, IEnumerable<BizObj.Carrier> carriers)
        {
            foreach (var carrier in carriers)
            {
                InfoMessage("Tower {0}, Carrier Level: '{1}, Omitted: '{2}', ", tower, carrier.Level, carrier.OmitLevel);
                int division = 1;
                foreach (var reserve in carrier.TrayReserves)
                {
                    InfoMessage("Division: '{0}', Reserve Type: '{1}', Component: '{2}', Omitted: '{3}'",
                        division++,
                        reserve.ReserveType,
                        reserve.Component != null ? reserve.Component.FullPath : "none",
                        reserve.Component != null ? reserve.Component.Omit.ToString() : "none");
                }
            }    
        }

        private void AdjustSplitterPosition()
        {
            splitterLogMessageWindow.SplitPosition = ClientRectangle.Height - (panelButtons.Location.Y + panelButtons.Size.Height + 9);    
        }
        #endregion
    }
}
