using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.BaseClasses;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Notifications.ClientUpdateService;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Notifications.SubscriptionService;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Types;
using BoardSide = Asm.As.Oib.SiplacePro.Proxy.Business.Objects.BoardSide;
using BoardSideEnum = Asm.As.Oib.SiplacePro.Proxy.Business.Types.BoardSide;

namespace Oib.Tutorial.PrinterProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<PropertyId> _subscribedUpdates = new List<PropertyId>();
        private readonly List<ServerObjectBase> _createdObjects = new List<ServerObjectBase>();
        private Board _currentBoard;
        private PrinterBoard _currentPrinterBoard;
        private Recipe _currentRecipe;
        private Line _currentLine;
        private string _folderName = "PrinterProgrammingDemo";

        #region Properties

        private ServerObjectBase _selectedObject;
        public ServerObjectBase SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                OnSelectedObjectChanged();
            }
        }

        private bool _isProxyInitialized;
        public bool IsProxyInitialized
        {
            get { return _isProxyInitialized; }
            set
            {
                _isProxyInitialized = value;
            }
        }

        private List<Board> _boards;
        public List<Board> Boards
        {
            get { return _boards; }
            set
            {
                _boards = value;
                LbBoards.ItemsSource = _boards;
            }
        }
        private List<PrinterBoard> _printerBoards;
        public List<PrinterBoard> PrinterBoards
        {
            get { return _printerBoards; }
            set
            {
                _printerBoards = value;
                LbPrinterBoards.ItemsSource = _printerBoards;
            }
        }
        #endregion

        
        public MainWindow()
        {
            InitializeComponent();

            PrintLine("Welcome to the Printer Programming Tutorial Application.", false);
            PrintLine($"Click \"{BtnInitialize.Content}\" to start.", false);
        }


        #region EventHandlers
        private void OnProxyInitialized()
        {
            PrintLine("Initialized");
            IsProxyInitialized = true;
            EnableButtons();
            LoadBoards();
            LoadPrinterBoards();
            PrintLine("Select a board to proceed...");
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_createdObjects.Count > 0)
            {
                MessageBoxResult result =
                    MessageBox.Show(
                        $"Would you like to delete created objects?",
                        "Delete created objects", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _currentLine?.PrintersInLine.Clear();
                    foreach (var serverType in Session.CurrentSession.OrderedDeleteTypes)
                    {
                        foreach (var createdObject in _createdObjects.Where(o => o.ObjectServerType == serverType))
                        {
                            if (createdObject is Stencil stencil)
                                stencil.ImageDefinition.StencilImage = null;
                            Session.CurrentSession.DeleteObject(createdObject.Identity);
                        }
                    }
                    foreach (var folder in _createdObjects.Where(o => o.ObjectServerType == ObjectServerType.Folder))
                    {
                        var identities = Session.CurrentSession.GetIdentities($@"{folder.FullTypePath}\*");
                        if (identities != null && identities.Count == 0)
                            Session.CurrentSession.DeleteObject(folder.Identity);
                    }
                }
            }

            if (IsProxyInitialized)
            {
                Session.CurrentSession.SubscriptionService.SubscribedPropertyUpdate -= SubscriptionService_SubscribedPropertyUpdate;
                UnsubscribePropertyUpdates();
                Session.CurrentSession.ClientUpdateService.RemoteObjectCreated -= ClientUpdateService_RemoteObjectCreated;
                Session.CurrentSession.ClientUpdateService.Stop();
                SessionManager.ReleaseAllSessions();
                IsProxyInitialized = false;
            }

            base.OnClosing(e);
        }

        private void OnSelectedObjectChanged()
        {
            BtnCreatePrinterBoard.IsEnabled = IsProxyInitialized && SelectedObject is Board;
        }

        private void BtnInitialize_OnClick(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
        private void BtnCreatePrinterBoard_OnClick(object sender, RoutedEventArgs e)
        {
            _currentPrinterBoard = null;
            _currentBoard = SelectedObject as Board;
            DoTransaction(() =>
            {
                _currentPrinterBoard = CreatePrinterBoard(_currentBoard);
            });
            BtnCreateTopSide.IsEnabled = _currentPrinterBoard != null;

            if (_currentPrinterBoard == null) return;
            LoadPrinterBoards();
            LbPrinterBoards.SelectedIndex = PrinterBoards.IndexOf(_currentPrinterBoard);
        }

        private void BtnCreateTopSide_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPrinterBoard == null) return;

            DoTransaction(() =>
            {
                #region DOC_INIT_PRINTER_BOARD_SIDE

                _currentPrinterBoard.TopSide.PrinterProgram.ProductName = _currentBoard.Name + ".Top";
                CopyFiducials(_currentBoard.TopSide, _currentPrinterBoard.TopSide);

                #endregion
            });
            BtnCreateTopSideProgram.IsEnabled = true;
        }

        private void BtnCreateTopSideProgram_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPrinterBoard == null) return;
            PrinterBoardSide printerBoardSide = _currentPrinterBoard.TopSide;
            DoTransaction(() =>
            {
                CreatePrinterProgram(printerBoardSide);
            });
            BtnCreateRecipe.IsEnabled = true;
        }

        private void BtnCreateRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPrinterBoard == null) return;
            PrinterBoardSide printerBoardSide = _currentPrinterBoard.TopSide;
            DoTransaction(() =>
            {
                CreateRecipe(_currentBoard, printerBoardSide);
            });
        }

        private void TbOutputClear_OnClick(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }
        private void LbBoardsReload_OnClick(object sender, RoutedEventArgs e)
        {
            LoadBoards();
        }
        private void LbBoards_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LbBoards.SelectedIndex;
            if (index < 0 || index >= Boards.Count) return;
            var board = Boards[index];
            PrintLine($"Selected Board: {board}, WxLxH: {board.Width}x{board.Length}x{board.Height}");
            SelectedObject = board;
        }
        private void LbPrinterBoardsReload_OnClick(object sender, RoutedEventArgs e)
        {
            LoadPrinterBoards();
        }
        private void LbPrinterBoards_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LbPrinterBoards.SelectedIndex;
            if (index < 0 || index >= PrinterBoards.Count) return;
            var printerBoard = PrinterBoards[index];
            PrintLine($"Selected PrinterBoard: {printerBoard}, WxLxH: {printerBoard.Width}x{printerBoard.Length}x{printerBoard.Height}");
            SelectedObject = printerBoard;
        }
        #endregion


        private void Initialize()
        {
            PrintLine("Initializing...");
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            BtnInitialize.IsEnabled = false;
            await Task.Run(() =>
            {
                #region DOC_INITIALIZE_PROXY

                SessionManager.OpenTimeout = new TimeSpan(0, 1, 0);
                SessionManager.CurrentCallbackEndpointBase = "net.tcp://localhost:5555/Asm.As.Oib.spi.testprinterprogramming";
                SessionManager.TcpPortSharingEnabled = false;
                SessionManager.TcpPortBandWidth = 100;

                //Login
                if (Session.CurrentSession.SecurityEnabled)
                {
                    Dispatcher.Invoke(() =>
                    {
                        PrintLine("Security is enabled, logging in...");
                        if (!Session.CurrentSession.SecurityManager.LogonUser("service", "", false))
                        {
                            MessageBox.Show("Logon failed.");
                            Environment.Exit(0);
                        }
                    });
                }

                //Subscribe to changes
                SubscribeToPropertyUpdate(PropertyId.BoardFamily_PrinterBoardRef);
                SubscribeToPropertyUpdate(PropertyId.PrinterBoardSide_StencilImageRef);
                SubscribeToPropertyUpdate(PropertyId.Stencil_FrameTypeRef);
                SubscribeToPropertyUpdate(PropertyId.PrinterProgramData_ProductName);
                SubscribeToPropertyUpdate(PropertyId.PrinterProgramData_ToolingRef);
                SubscribeToPropertyUpdate(PropertyId.PrinterProgramData_DepositionRef);
                SubscribeToPropertyUpdate(PropertyId.PrinterProgramData_SeparationProcessRef);
                Session.CurrentSession.SubscriptionService.SubscribedPropertyUpdate += SubscriptionService_SubscribedPropertyUpdate;

                //Subscribe to general changes
                Session.CurrentSession.ClientUpdateService.Start();
                Session.CurrentSession.ClientUpdateService.RemoteObjectCreated += ClientUpdateService_RemoteObjectCreated;

                #endregion

            });
            OnProxyInitialized();
        }
        
        private void SubscribeToPropertyUpdate(PropertyId propertyId)
        {
            Session.CurrentSession.SubscriptionService.SubscribeToPropertyUpdate(propertyId);
            _subscribedUpdates.Add(propertyId);
        }
        private void UnsubscribePropertyUpdates()
        {
            foreach (PropertyId propertyId in _subscribedUpdates)
            {
                Session.CurrentSession.SubscriptionService.UnsubscribeToPropertyUpdate(propertyId);
            }
            _subscribedUpdates.Clear();
        }

        private void EnableButtons()
        {
            BtnInitialize.IsEnabled = !IsProxyInitialized;
            BtnCreatePrinterBoard.IsEnabled = IsProxyInitialized && SelectedObject is Board;
        }
        

        #region SPI Wrappers
        private T LoadOrCreateObject<T>(string name) where T : ServerObjectBase
        {
            T obj = LoadObject<T>(name) ?? CreateObject<T>(name);
            return obj;
        }
        private T LoadObject<T>(string name) where T : NamedObjectBase
        {
            string typeName = typeof(T).Name;
            try
            {
                PrintLine($"Loading {typeName}:{name}...");
                ObjectServerType objectType = (ObjectServerType)Enum.Parse(typeof(ObjectServerType), typeName);
                var identity = Session.CurrentSession.GetIdentity(name, objectType);
                if(identity == null)
                    return LoadObject<T>(_folderName, name);
                T obj = (T)Session.CurrentSession.GetObject(identity);
                return obj;
            }
            catch (Exception ex)
            {
                PrintLine(ex.ToString());
            }

            return null;
        }

        #region DOC_LOAD_OBJECT
        private T LoadObject<T>(string folder, string name) where T : NamedObjectBase
        {
            string typeName = typeof(T).Name;
            try
            {
                PrintLine($"Loading {typeName}:{name}...");
                ObjectServerType objectType = (ObjectServerType)Enum.Parse(typeof(ObjectServerType), typeName);
                var identity = Session.CurrentSession.GetIdentity($@"{folder}\{name}", objectType);
                if (identity != null)
                {
                    T obj = (T)Session.CurrentSession.GetObject(identity);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                PrintLine(ex.ToString());
            }
            return null;
        }

        #endregion
        private List<T> LoadObjects<T>() where T : NamedObjectBase
        {
            var objects = new List<T>();
            string typeName = typeof(T).Name;
            try
            {
                PrintLine($"Loading {typeName}s...");
                var identities = Session.CurrentSession.GetIdentities($"{typeName}:*");
                if (identities == null || identities.Count == 0)
                {
                    PrintLine("\tno Identities received.", false);
                    return objects;
                }
                foreach (var ident in identities)
                {
                    PrintLine("\t" + ident.FullPath, false);
                    T obj = (T)Session.CurrentSession.GetObject(ident);
                    objects.Add(obj);
                }
            }
            catch (Exception ex)
            {
                PrintLine(ex.ToString());
            }

            return objects;
        }
        private List<ServerObjectBase> LoadObjects(string typeName)
        {
            var objects = new List<ServerObjectBase>();
            try
            {
                PrintLine($"Loading {typeName}s...");
                var identities = Session.CurrentSession.GetIdentities($"{typeName}:*");
                foreach (var ident in identities)
                {
                    var obj = (ServerObjectBase)Session.CurrentSession.GetObject(ident);
                    objects.Add(obj);
                }
            }
            catch (Exception ex)
            {
                PrintLine(ex.ToString());
            }

            return objects;
        }
        private T CreateObject<T>(string name) where T : ServerObjectBase
        {
            string typeName = typeof(T).Name;
            var obj = (T)CreateObject(typeName, _folderName, name);
            return obj;
        }

        #region DOC_CREATE_OBJECT
        private T CreateObject<T>(string folder, string name, string userText = "") where T : ServerObjectBase
        {
            string typeName = typeof(T).Name;
            var obj = (T)CreateObject(typeName, folder, name, userText);
            return obj;
        }
        private ServerObjectBase CreateObject(string typeName, string name)
        {
            ObjectServerType objectType = (ObjectServerType)Enum.Parse(typeof(ObjectServerType), typeName);
            var obj = (ServerObjectBase)Session.CurrentSession.CreateObject(objectType, name);
            PrintLine($"{typeName} created: {obj.FullPath}");
            
            _createdObjects.Add(obj);
            return obj;
        }
        private ServerObjectBase CreateObject(string typeName, string folderName, string name, string userText = "")
        {
            Folder folder = GetFolder(typeName, folderName);
            var obj = (ServerObjectBase)Session.CurrentSession.CreateObject(folder, name, userText);
            PrintLine($"{typeName} created: {obj.FullPath}");

            _createdObjects.Add(obj);
            return obj;
        }
        private Folder GetFolder(string typeName, string folderName)
        {
            Folder folder = Session.CurrentSession.GetFolder($@"{typeName}:\{folderName}");
            if (folder == null)
            {
                folder = Session.CurrentSession.CreateFolder($@"{typeName}:\{folderName}");
                _createdObjects.Add(folder);
            }
            return folder;
        }
        private Folder GetFolder<T>(string folderName)
        {
            string typeName = typeof(T).Name;
            return GetFolder(typeName, folderName);
        }

        #endregion
        private bool DeleteObject(string pattern)
        {
            var identities = Session.CurrentSession.GetIdentities(pattern);
            if (identities == null || identities.Count == 0)
            {
                PrintLine("\tno Identities received.", false);
                Session.CurrentSession.Commit();
                return false;
            }
            if (identities.Count > 1)
            {
                MessageBoxResult result =
                    MessageBox.Show($"{identities.Count} identities received. Do you like to delete all objects?",
                        "Delete objects", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var ident in identities)
                    {
                        Session.CurrentSession.DeleteObject(ident);
                        PrintLine($"object deleted: {ident}");
                    }
                }
            }
            else
            {
                var ident = identities.First();
                MessageBoxResult result =
                    MessageBox.Show($"Do you like to delete the object \"{ident}\"?",
                        "Delete object", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Session.CurrentSession.DeleteObject(ident);
                    PrintLine($"object deleted: {ident}");
                }
            }
            return true;
        }
        
        private bool DoTransaction(Action action)
        {
            Session.CurrentSession.StartTransaction();
            try
            {
                action.Invoke();

                Session.CurrentSession.Commit();
                return true;
            }
            catch (Exception ex)
            {
                PrintLine(ex.ToString());
                Session.CurrentSession.Rollback();
            }

            return false;
        }

        #endregion


        private void LoadBoards()
        {
            Boards = LoadObjects<Board>();
            LbBoards.Items.Refresh();
        }
        private void LoadPrinterBoards()
        {
            PrinterBoards = LoadObjects<PrinterBoard>();
            LbPrinterBoards.Items.Refresh();
        }

        #region DOC_CREATE_PRINTER_BOARD
        
        private PrinterBoard CreatePrinterBoard(Board board)
        {
            if (board == null) return null;
            string name = board.Name;
            var printerBoard = LoadOrCreateObject<PrinterBoard>(name);
            printerBoard.Height = board.Height;
            printerBoard.Width = board.Width;
            printerBoard.Length = board.Length;
            var boardFamily = LoadOrCreateObject<BoardFamily>(name);
            boardFamily.PrinterBoard = printerBoard;
            if(!boardFamily.ContainsBoard(board.Identity))
                boardFamily.AddBoard(board.Identity);
            return printerBoard;
        }

        #endregion

        #region DOC_SETUP_PRINTER_PROGRAM

        private void CreatePrinterProgram(PrinterBoardSide printerBoardSide)
        {
            AssignStencil(printerBoardSide, @"System\265");
            AssignTooling(printerBoardSide);
            AssignDeposition(printerBoardSide);
            AssignSqueegees(printerBoardSide);
            AssignSeparation(printerBoardSide);
            AssignCleaning(printerBoardSide);
        }

        #endregion

        #region DOC_COPY_FIDUCIALS
        private void CopyFiducials(BoardSide boardSide, PrinterBoardSide printerBoardSide)
        {
            foreach (var fiducial in boardSide.FiducialList.FiducialPlacements.Values)
            {
                if (printerBoardSide.OptionalFiducials.Any(f => f.RefDesignator == fiducial.RefDesignator))
                    continue;

                var printerFiducial = new PrinterFiducialPlacement(Session.CurrentSession);
                printerFiducial.DefinedOnBoard = true;
                printerFiducial.RefDesignator = fiducial.RefDesignator;
                printerFiducial.OffsetX = fiducial.OffsetX;
                printerFiducial.OffsetY = fiducial.OffsetY;
                printerFiducial.BoardFiducialInfo.Angle = fiducial.Angle;
                printerFiducial.PrintOnly = true;

                printerBoardSide.OptionalFiducials.Add(printerFiducial);
            }
        }
        #endregion

        #region DOC_SETUP_STENCIL
        private void AssignStencil(PrinterBoardSide printerBoardSide, string frameType)
        {
            var stencil = LoadOrCreateObject<Stencil>(printerBoardSide.PrinterProgramProductName);
            stencil.FrameType = LoadObject<PrinterStencilFrameType>(frameType);
            var stencilImage = LoadOrCreateObject<StencilImage>(printerBoardSide.PrinterProgramProductName);
            stencil.ImageDefinition.StencilImage = stencilImage;
            printerBoardSide.StencilImage = stencilImage;
        }
        #endregion

        #region DOC_SETUP_PRINTER_TOOLING
        private void AssignTooling(PrinterBoardSide printerBoardSide, string toolingName = @"System\Default Printer Tooling")
        {
            var printerTooling = LoadOrCreateObject<PrinterTooling>(toolingName);
            printerBoardSide.PrinterProgram.Tooling = printerTooling;
        }
        #endregion

        #region DOC_ASSIGN_DEPOSITION_AND_SQUEEGEES
        private void AssignDeposition(PrinterBoardSide printerBoardSide)
        {
            var board = (PrinterBoard)printerBoardSide.ServerParentNamed;
            var depositions = LoadObjects<PrinterDeposition>();
            foreach (var deposition in depositions)
            {
                foreach (var squeegee in deposition.SqueegeeSetup.SqueegeeProcesses.Keys)
                {
                    if (squeegee.Length >= board.Length)
                    {
                        printerBoardSide.PrinterProgram.Deposition = deposition;
                        return;
                    }
                }
            }
        }
        private void AssignSqueegees(PrinterBoardSide printerBoardSide)
        {
            var board = (PrinterBoard)printerBoardSide.ServerParentNamed;
            var squeegees = printerBoardSide.PrinterProgram.Deposition.SqueegeeSetup.SqueegeeProcesses.Keys;
            foreach (var squeegee in squeegees)
            {
                if (squeegee.Length >= board.Length)
                {
                    printerBoardSide.PrinterProgram.Squeegees.Add(squeegee);
                    return;
                }
            }
        }

        #endregion

        #region DOC_ASSIGN_SEPARATION_PROCESS
        private void AssignSeparation(PrinterBoardSide printerBoardSide, string name = @"System\Default Separation")
        {
            var separation = LoadOrCreateObject<SeparationProcess>(name);
            printerBoardSide.PrinterProgram.SeparationProcess = separation;
        }
        #endregion

        #region DOC_ASSIGN_CLEANING_PROFILE
        private void AssignCleaning(PrinterBoardSide printerBoardSide, int cleanRate = 5, string name = @"System\Quick")
        {
            var cleaningProfile = LoadOrCreateObject<PrinterCleaningProfile>(name);
            if (printerBoardSide.PrinterProgram.Cleaning.CleaningModes.Count == 0)
                printerBoardSide.PrinterProgram.Cleaning.CleaningModes.Add(new CleaningMode(Session.CurrentSession));
            var cleaningMode = printerBoardSide.PrinterProgram.Cleaning.CleaningModes.First();
            cleaningMode.CleanMode = cleaningProfile;
            cleaningMode.CleanRate = cleanRate;
        }
        #endregion

        #region DOC_CREATE_RECIPE
        private void CreateRecipe(Board board, PrinterBoardSide printerBoardSide)
        {
            var line = GetLine();
            var recipe = LoadOrCreateObject<Recipe>(printerBoardSide.PrinterProgramProductName);
            var printerBoard = (PrinterBoard)printerBoardSide.Parent;
            recipe.PrinterBoardElement.PrinterBoard = printerBoard;
            recipe.PrinterBoardElement.BoardSide = Equals(printerBoard.TopSide, printerBoardSide) ? BoardSideEnum.Top : BoardSideEnum.Bottom;
            var boardElement = new BoardElement(board);
            boardElement.BoardSideProduced = recipe.PrinterBoardElement.BoardSide;
            recipe.BoardElements.Add(boardElement);
            recipe.Line = line;
            _currentRecipe = recipe;
        }
        private Line GetLine(string name = @"OIB-SC-Tutorials\X2")
        {
            var line = LoadObject<Line>(name) ?? CreateObject<Line>(name);
            PrintLine($"\tStations in line ({line.StationInLines.Count} stations):", false);
            foreach (var station in line.StationInLines)
            {
                PrintLine($"\t\t{station}", false);
            }
            PrintLine($"\tPrinters in line ({line.PrintersInLine.Count} printers):", false);
            foreach (var printer in line.PrintersInLine)
            {
                PrintLine($"\t\t{printer}", false);
            }

            if (line.PrintersInLine.Count == 0)
            {
                var printer = GetPrinter();
                var pil = new PrinterInLine(Session.CurrentSession);
                pil.Printer = printer;
                line.PrintersInLine.Add(pil);

                PrintLine($"\tAdded {printer} to {line}", false);
            }
            _currentLine = line;
            return line;
        }
        private Printer GetPrinter(string printerTypeName = "DEK TQ")
        {
            var folder = GetFolder<Printer>(_folderName);
            var printerName = $"{printerTypeName} Printer Demo";
            var printerType = LoadObject<PrinterType>(printerTypeName);
            var printer = LoadObject<Printer>(printerName) ?? (Printer)Session.CurrentSession.CreateObject(folder, printerName, printerType.ResourceID);
            printer.PrinterRecipeDownload = true;
            _createdObjects.Add(printer);
            return printer;
        }
        
        #endregion



        #region Events
        private void ClientUpdateService_RemoteObjectCreated(object sender, RemoteObjectCreated args)
        {
            PrintLine($"RemoteObjectCreated: {args.Identity.FullPath}");
        }

        private void SubscriptionService_SubscribedPropertyUpdate(object sender, PropertyUpdateArgs args)
        {
            PrintLine($"SubscribedPropertyUpdate: {args.Identity.FullPath}");
        }

        #endregion
        
        #region Output
        private void ClearOutput()
        {
            TbOutput.Text = string.Empty;
        }
        private void Print(string text, bool timestamp = true)
        {
            TbOutput.Text += timestamp ? $"{DateTime.Now:hh:mm:ss.fff}: {text}" : text;
            TbOutput.ScrollToEnd();
        }
        private void PrintLine()
        {
            Print(Environment.NewLine, false);
        }
        private void PrintLine(string text, bool timestamp = true)
        {
            Print(text + Environment.NewLine, timestamp);
        }

        #endregion

    }
}
