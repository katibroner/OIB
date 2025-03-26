#region using

using System;
using System.ComponentModel;
using MaterialManagerEventsTutorial.ViewModels;
using System.Windows;

#endregion using

namespace MaterialManagerEventsTutorial.Views
{
    /// <summary>
    /// Interaction logic for MainPanelView.xaml
    /// </summary>
    public partial class MainPanelView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanelView"/> class.
        /// </summary>
        public MainPanelView()
        {
            InitializeComponent();
            DataContext = new MainPanelViewModel();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 30, 0);
            dispatcherTimer.Start();
        }

        private void MainPanelView_OnClosing(object sender, CancelEventArgs e)
        {
            EventsController.Unsubscribe();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(EventsController.IsServiceEndpointExists)
                EventsController.RenewOrCreateSubscription();
        }
    }
}
