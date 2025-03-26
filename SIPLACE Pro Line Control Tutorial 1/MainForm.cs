#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;

#region DOC_NAMESPACES

using Asm.As.Oib.SiplacePro.Proxy.Types;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using Asm.As.Oib.SiplacePro.LineControl.Proxy.Business.Objects;
using Asm.As.Oib.SiplacePro.LineControl.Contracts;
using LcObjects = Asm.As.Oib.SiplacePro.LineControl.Contracts.Data;
using Asm.As.Oib.SiplacePro.LineControl.Contracts.Faults;

#endregion

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form
    {
        #region Fields

        private LineControl _LineControlProxy;

        #endregion

        #region Constructor

        public mainForm()
        {
            InitializeComponent();

            UpdateRecipeRelatedButtons();
        }

        #endregion

        #region Form Events

        protected override void OnClosed(EventArgs e)
        {
            ReleaseLineControlProxy();
            SessionManager.ReleaseAllSessions();

            base.OnClosed(e);
        }

        #endregion

        #region Stop Line

        private void ButtonStopLine_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                #region DOC_STOP_LINE;

                if (_LineControlProxy.StopLine(_TextBoxLineFullPath.Text))
                    _ToolStripStatusLabel.Text = string.Format("Line '{0}' stopped.", _TextBoxLineFullPath.Text);
                else
                    _ToolStripStatusLabel.Text = string.Format("Line '{0}' NOT stopped.", _TextBoxLineFullPath.Text);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Continue Line

        private void ButtonContinueLine_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                #region DOC_CONTINUE_LINE

                if (_LineControlProxy.ContinueLine(_TextBoxLineFullPath.Text))
                    _ToolStripStatusLabel.Text = string.Format("Line '{0}' continued.", _TextBoxLineFullPath.Text);
                else
                    _ToolStripStatusLabel.Text = string.Format("Line '{0}' NOT continued.", _TextBoxLineFullPath.Text);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Get Line Status

        private void ButtonGetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowLineStatus()
        {
            #region DOC_LINE_STATUS

            LcObjects.LineControlLineStatus lineStatus = _LineControlProxy.GetLineStatus(_TextBoxLineFullPath.Text);

            MsgOut("LINE Status");
            MsgOut("  Line:                          \t" + lineStatus.Line);
            MsgOut("  LineControlServer:       \t" + lineStatus.LineControlServerHostName);
            MsgOut("  ProductionSchedule:      \t" + lineStatus.ProductionSchedule);

            foreach (LcObjects.LineControlStationStatus stationStatus in lineStatus.LineControlStationStati)
            {
                MsgOut("-------------------------------------------------------------");
                MsgOut("STATION:                  \t" + stationStatus.Name);
                MsgOut("  HostName:            \t" + stationStatus.HostName);
                MsgOut("  HostIP:                    \t" + stationStatus.HostIP);
                MsgOut("  ConnectionState:     \t" + stationStatus.ConnectionState);
                MsgOut("  SetupName:           \t" + stationStatus.SetupName);
                MsgOut("  SoftwareVersion:     \t" + stationStatus.SoftwareVersion);
                MsgOut("-------------------------------------------------------------");
                MsgOut("RIGHT conveyor (1):");
                MsgOut("  BoardName:           \t" + stationStatus.RightConveyorStatus.BoardName);
                MsgOut("  BoardSide:              \t" + stationStatus.RightConveyorStatus.BoardSide);
                MsgOut("  ProcessState:        \t" + stationStatus.RightConveyorStatus.ProcessState);
                MsgOut("  RecipeName:          \t" + stationStatus.RightConveyorStatus.RecipeName);
                MsgOut("-------------------------------------------------------------");
                MsgOut("LEFT conveyor (2):");
                MsgOut("  BoardName:           \t" + stationStatus.LeftConveyorStatus.BoardName);
                MsgOut("  BoardSide:              \t" + stationStatus.LeftConveyorStatus.BoardSide);
                MsgOut("  ProcessState:        \t" + stationStatus.LeftConveyorStatus.ProcessState);
                MsgOut("  RecipeName:          \t" + stationStatus.LeftConveyorStatus.RecipeName);
            }

            #endregion
        }

        #endregion

        #region Create Production Schedule

        private void ButtonCreateProductionSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                #region DEC_CREATE_PS

                // Get the SIPLACE Pro session for the SPI adapter
                SessionManager.CurrentSessionEndpoint =
                    new EndpointAddress(new Uri(GetSiplaceProEndpointAddress()));
                SessionManager.CurrentCallbackEndpointBase =
                    string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //SessionManager.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                Session session = Session.CurrentSession;

                // Check the production schedule to exist in SIPLACE Pro
                Identity identity = session.IdentityList.GetIdentity(_TextBoxPSFullPath.Text,
                    ObjectServerType.ProductionSchedule);

                if (identity == null)
                {
                    // Get the line the production schedule will be for.
                    Line line = session.GetObject(_TextBoxLineFullPath.Text, ObjectServerType.Line) as Line;
                    if (line == null)
                        throw new ApplicationException(string.Format("Line '{0}' does not exist in SIPLACE Pro.",
                            _TextBoxLineFullPath.Text));

                    string productionScheduleName = Path.GetFileName(_TextBoxPSFullPath.Text);
                    string folderFullName = Path.GetDirectoryName(_TextBoxPSFullPath.Text);

                    session.StartTransaction();
                    try
                    {
                        // Get or create folder if it does not already exist.
                        Folder folder = session.CreateFolder("ProductionSchedule:" + folderFullName);

                        // Create production schedule
                        ProductionSchedule productionSchedule =
                            (ProductionSchedule)Session.CurrentSession.CreateObject(folder, productionScheduleName);

                        // Assign the line the production schedule is for.
                        productionSchedule.Line = line;

                        session.Commit();
                    }
                    catch
                    {
                        session.Rollback();
                        throw;
                    }

                }
                else
                {
                    ProductionSchedule productionSchedule = (ProductionSchedule)identity.Object;
                    Line line = productionSchedule.Line;
                    _TextBoxLineFullPath.Text = line.FullPath;
                }

                #endregion

                UpdateProductionScheduleGrid();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Add recipes of job to production schedule

        private void ButtonAddRecipesOfJob_Click(object sender, EventArgs e)
        {
            var dlg = new SelectJobForm(GetSiplaceProEndpointAddress());
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    #region DOC_FILL_PS

                    #region Use Client Authentication

                    // Usage of client authentication
                    if (_cbUseClientAuthentication.Checked)
                    {
                        // Add client certificate to authenticate to service

                        // **** IMPORTANT ****
                        //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                        //- uncomment to use client authentication, but consider these values overrides app.config settings
                        //Session.SetCertificateForClientAuthentication(
                        //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                    }

                    #endregion

                    // Get the SIPLACE Pro session
                    Session session = Session.CurrentSession;

                    // Get the production schedule
                    ProductionSchedule productionSchedule =
                        session.GetObject("ProductionSchedule:" + _TextBoxPSFullPath.Text) as ProductionSchedule;
                    if (productionSchedule == null)
                        throw new ApplicationException(string.Format(
                            "The production schedule '{0}' does not exist in SIPLACE Pro.",
                            _TextBoxPSFullPath.Text));

                    // Get the job
                    Job job = session.GetObject("Job:" + dlg.JobFullPath) as Job;
                    if (job == null)
                        throw new ApplicationException(string.Format(
                            "The job '{0}' does not exist in SIPLACE Pro.",
                            dlg.JobFullPath));

                    session.StartTransaction();
                    try
                    {
                        foreach (RecipeElement recipeElement in job.Recipes)
                        {
                            if (recipeElement.Recipe == null)
                                continue;

                            // Check recipe to have a setup
                            if (recipeElement.Recipe.Setup == null)
                            {
                                MsgOut(string.Format(
                                    "Skipping Recipe: Recipe '{0}' has no setup assigned. Possibly it is not optimized.",
                                    recipeElement.Recipe.FullPath));
                                continue;
                            }

                            // Check recipe to have a board
                            if (recipeElement.BoardElements.Count != 1)
                            {
                                MsgOut(string.Format(
                                    "Skipping Recipe: Recipe '{0}' needs to have exactly one board assigned.",
                                    recipeElement.Recipe.FullPath));
                                continue;
                            }

                            // Check recipe to be for the same line as the production schedule is:
                            if (recipeElement.Recipe.Setup.Line != productionSchedule.Line)
                            {
                                MsgOut(string.Format(
                                    "Skipping Recipe: Recipe '{0}' is for line '{1}' while the production schedule is for line '{2}'.",
                                    recipeElement.Recipe.FullPath, recipeElement.Recipe.Setup.FullPath, productionSchedule.Line.FullPath));
                                continue;
                            }

                            // Create a production schedule element and add the recipe to it.
                            ProductionScheduleElement pse = new ProductionScheduleElement(session);
                            pse.Recipe = recipeElement.Recipe;
                            pse.ScheduledLotSize = recipeElement.Recipe.LotSize;

                            // Add the production schedule element to the production schedule.
                            productionSchedule.ProductionScheduleElements.Add(pse);
                        }

                        session.Commit();
                    }
                    catch
                    {
                        session.Rollback();
                        throw;
                    }

                    #endregion

                    UpdateProductionScheduleGrid();
                }
                catch (FaultException<SiplaceProLineControlFault> ex)
                {
                    MsgOut("LINECONTROL ERROR: " + ex.Message);
                }
                catch (CommunicationException ex)
                {
                    MsgOut("COMMUNICATION ERROR: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MsgOut("ERROR: " + ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region Integrity Check

        private void ButtonIntegrityCheck_Click(object sender, EventArgs e)
        {
            if (_DataGridViewProductionSchedule.SelectedRows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                string lineFullPath = _TextBoxLineFullPath.Text;

                DataGridViewRow selectedRow = _DataGridViewProductionSchedule.SelectedRows[0];
                ProductionScheduleElement productionScheduleElement = (ProductionScheduleElement) selectedRow.Cells["PSE"].Value;

                string productionScheduleFullPath = ((ProductionSchedule) productionScheduleElement.ServerParent).FullPath;

                MsgOut(string.Format("Integrity Check for recipe '{0}'", productionScheduleElement.Recipe.FullPath));

                #region DOC_PERFORM_INTEGRITY_CHECK

                try
                {
                    InitializeLineControlPoxy();

                    // Perform the Integrity Check for the recipe included in the production schedule element
                    List<LcObjects.OptimizerMessage> messages = _LineControlProxy.CheckDownloadData(
                        lineFullPath, // Full path of the line              
                        productionScheduleFullPath, // Full path of the production schedule
                        productionScheduleElement.ID, // Id of the produciton schedule element
                        IntegrityCheckMode.AllowHeadstepRecalcuation, // Integrity Check options
                        true); // Allow repartititioning

                    // Output to messsages of SIPLACE Integrity Check
                    foreach (LcObjects.OptimizerMessage message in messages)
                    {
                        MsgOut(message.MessageType + " " + message.Message);
                    }
                }
                catch (FaultException<SiplaceProLineControlFault> ex)
                {
                    MsgOut("LINECONTROL ERROR: " + ex.Message);
                }
                catch (CommunicationException ex)
                {
                    MsgOut("COMMUNICATION ERROR: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MsgOut("ERROR: " + ex.Message);
                }

                #endregion

                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Download

        private void ButtonStartRecipe_Click(object sender, EventArgs e)
        {
            if (_DataGridViewProductionSchedule.SelectedRows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                DataGridViewRow selectedRow = _DataGridViewProductionSchedule.SelectedRows[0];
                ProductionScheduleElement productionScheduleElement = (ProductionScheduleElement) selectedRow.Cells["PSE"].Value;

                ProductionSchedule productionSchedule = (ProductionSchedule) productionScheduleElement.ServerParent;

                MsgOut(string.Format(
                    "Specifying recipe '{0}' to {1} sub-line.",
                    productionScheduleElement.Recipe.FullPath, (_rbSIPLACESubLine.Checked ? "SIPLACE Station" : "DEK Printer")));

                #region DOC_DOWNLOAD

                try
                {
                    InitializeLineControlPoxy();

                    var downloadRequest = new LcObjects.DownloadRequest
                    {
                        ProductionScheduleFullPath = productionSchedule.FullPath,
                        ProductionScheduleElementIndexRight =
                            productionSchedule.ProductionScheduleElements.IndexOf(productionScheduleElement),
                        SubLineSelection = _rbSIPLACESubLine.Checked ? 0:1,
                        Lanes = ConveyorLanes.Right,
                    };

                    LcObjects.DownloadResult result = _LineControlProxy.Download(downloadRequest);

                    // Handle the download result
                    if (!result.Succeeded)
                    {
                        MsgOut(string.Format("DOWNLOAD: The download has failed: '{0}'", result.ExceptionMessage));
                    }

                    ShowLineStatus();
                }
                catch (FaultException<SiplaceProLineControlFault> ex)
                {
                    MsgOut("LINECONTROL ERROR: " + ex.Message);
                }
                catch (CommunicationException ex)
                {
                    MsgOut("COMMUNICATION ERROR: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MsgOut("ERROR: " + ex.Message);
                }

                #endregion

                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Line Control Proxy Related

        #region DOC_CREATE_PROXY

        private void InitializeLineControlPoxy()
        {
            if (_LineControlProxy == null)
            {
                // Create a tcp binding and extend the timeouts
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                binding.CloseTimeout = TimeSpan.FromSeconds(10);
                binding.OpenTimeout = TimeSpan.FromSeconds(10);
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                binding.ReliableSession.Enabled = true;
                binding.PortSharingEnabled = true;
                binding.MaxBufferPoolSize = 524288;
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxDepth = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                // Create the endpoint
                EndpointAddress endpointAddress = new EndpointAddress(_TextBoxLineControlEndpointAddress.Text);

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //LineControl.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Create the proxy
                _LineControlProxy = new LineControl(endpointAddress, binding);
            }
        }

        #endregion

        private void ReleaseLineControlProxy()
        {
            try
            {
                if (_LineControlProxy != null)
                    _LineControlProxy.Dispose();
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: failed to dispose of line control proxy: " + ex);
            }
            finally
            {
                _LineControlProxy = null;
            }
        }

        #endregion

        #region Messages (message view)

        private void MsgClear()
        {
            _ListBox.Items.Clear();
            Update();
        }

        public delegate void MsgOutDelegate(string message);

        public void MsgOut(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MsgOutDelegate(MsgOut), new object[] {message});
            }
            else
            {
                _ListBox.Items.Add(message);

                // Scroll to the last entry
                _ListBox.SelectedIndex = _ListBox.Items.Count - 1;
            }
        }

        #endregion

        #region Misc

        private void UpdateProductionScheduleGrid()
        {
            // Remove old displayed data
            _DataGridViewProductionSchedule.DataSource = null;
            Update();

            #region Use Client Authentication

            // Usage of client authentication
            if (_cbUseClientAuthentication.Checked)
            {
                // Add client certificate to authenticate to service

                // **** IMPORTANT ****
                //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                //- uncomment to use client authentication, but consider these values overrides app.config settings
                //Session.SetCertificateForClientAuthentication(
                //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
            }

            #endregion

            // Load production schedule
            ProductionSchedule productionSchedule = Session.CurrentSession.GetObject(
                _TextBoxPSFullPath.Text,
                ObjectServerType.ProductionSchedule) as ProductionSchedule;

            if (productionSchedule != null) // If object exists:
            {
                // Init data table
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("BoardFullPath", typeof (string));
                dataTable.Columns.Add("RecipeFullPath", typeof (string));
                dataTable.Columns.Add("SetupFullPath", typeof (string));
                dataTable.Columns.Add("BoardSide", typeof (string));
                dataTable.Columns.Add("Quantity", typeof (int));
                dataTable.Columns.Add("PSE", typeof (ProductionScheduleElement));

                // Fill data table
                foreach (ProductionScheduleElement pse in productionSchedule.ProductionScheduleElements)
                {
                    DataRow row = dataTable.NewRow();
                    row["BoardFullPath"] = pse.Recipe.BoardElements[0].Board.FullPath;
                    row["RecipeFullPath"] = pse.Recipe.FullPath;
                    row["SetupFullPath"] =
                        pse.Recipe.Setup != null
                            ? pse.Recipe.Setup.FullTypePath
                            : "(No setup information available)";
                    row["BoardSide"] = pse.Recipe.BoardElements[0].BoardSideProduced.ToString();
                    row["Quantity"] = pse.ScheduledLotSize;
                    row["PSE"] = pse; // Add the PSE as invisible column
                    dataTable.Rows.Add(row);
                }

                // Assign data table to grid view
                _DataGridViewProductionSchedule.DataSource = dataTable;

                // Adjust grid view appearance
                var dataGridViewColumn = _DataGridViewProductionSchedule.Columns["PSE"];
                if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                _DataGridViewProductionSchedule.AutoResizeColumns();
            }

            UpdateRecipeRelatedButtons();
        }

        private void UpdateRecipeRelatedButtons()
        {
            if (_DataGridViewProductionSchedule.SelectedRows.Count == 0)
            {
                _ButtonIntegrityCheck.Enabled = false;
                _ButtonStartRecipe.Enabled = false;
            }
            else
            {
                _ButtonIntegrityCheck.Enabled = true;
                _ButtonStartRecipe.Enabled = true;
            }
        }

        private void DataGridViewProductionSchedule_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateRecipeRelatedButtons();
        }

        private ConveyorLanes GetConveyorLane()
        {
            if (textBoxConveyorLane.Text.ToLower().StartsWith("l"))
                return ConveyorLanes.Left;
            if (textBoxConveyorLane.Text.ToLower().StartsWith("r"))
                return ConveyorLanes.Right;
            if (textBoxConveyorLane.Text.ToLower().StartsWith("b"))
                return ConveyorLanes.Both;

            MsgOut("Invalid Lane, 'both' will be used");
            return ConveyorLanes.Both;
        }

        private string GetSiplaceProEndpointAddress()
        {
            var address = _TextBoxSiplaceProEndpointAddress.Text;
            if (string.IsNullOrEmpty((address)))
            {
                address = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro";
            }

            return address;
        }

        #endregion

        #region Block/Unblock Input Conveyor

        private void ButtonBlockInputConveyor_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                var stationPath = textBoxStationPath.Text.Trim();

                #region DOC_BlockStationInputConveyor

                _ToolStripStatusLabel.Text =
                    string.Format(
                        _LineControlProxy.BlockStationInputConveyor(stationPath, true, "OIB Line Control Tutorial")
                            ? "The station input conveyor of station '{0}' blocked."
                            : "The station input conveyor of station '{0}' NOT blocked.", stationPath);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonUnblockInputConveyor_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                var stationPath = textBoxStationPath.Text.Trim();

                #region DOC_UnBlockStationInputConveyor

                _ToolStripStatusLabel.Text =
                    string.Format(
                        _LineControlProxy.UnBlockStationInputConveyor(stationPath, "OIB Line Control Tutorial")
                            ? "The station input conveyor of station '{0}' unblocked."
                            : "The station input conveyor of station '{0}' NOT unblocked.", stationPath);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Block/Unblock Conveyor

        private void ButtonBlockConveyor_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                var stationPath = textBoxStationPath.Text.Trim();
                var conveyorLane = GetConveyorLane();

                #region DOC_BlockStationConveyor

                _ToolStripStatusLabel.Text =
                    string.Format(
                        _LineControlProxy.BlockStationConveyor(stationPath, conveyorLane, "OIB Line Control Tutorial")
                            ? "The '{0}' conveyor lane(s) of station '{1}' blocked."
                            : "The '{0}' conveyor lane(s) of station '{1}' NOT blocked.", conveyorLane, stationPath);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonUnblockConveyor_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                var stationPath = textBoxStationPath.Text.Trim();
                var conveyorLane = GetConveyorLane();

                #region DOC_UnblockStationConveyor

                _ToolStripStatusLabel.Text =
                    string.Format(
                        _LineControlProxy.UnblockStationConveyor(stationPath, conveyorLane, "OIB Line Control Tutorial")
                            ? "The '{0}' conveyor lane(s) of station '{1}' unblocked."
                            : "The '{0}' conveyor lane(s) of station '{1}' NOT unblocked.", conveyorLane, stationPath);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region External Stop

        private void buttonExternalStop_Click(object sender, EventArgs e)
        {
            var failed = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                MsgClear();

                InitializeLineControlPoxy();

                var stationPath = textBoxStationPath.Text.Trim();

                #region DOC_ExternalStopOperation

                _LineControlProxy.ExternalStopOperation(stationPath, "OIB Line Control Tutorial");
                _ToolStripStatusLabel.Text = string.Format("The station '{0}' was stopped with external operation.", stationPath);

                #endregion

                ShowLineStatus();
            }
            catch (FaultException<SiplaceProLineControlFault> ex)
            {
                failed = true;
                MsgOut("LINECONTROL ERROR: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                failed = true;
                MsgOut("COMMUNICATION ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                failed = true;
                MsgOut("ERROR: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            if (failed)
                _ToolStripStatusLabel.Text = @"The external stop operation failed.";
        }

        #endregion
    }
}