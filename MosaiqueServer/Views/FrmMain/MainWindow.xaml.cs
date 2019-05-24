using MosaicServeur.Main;
using MosaicServeur.Views.FrmMain;
using Serveur.Controllers;
using Serveur.Models;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MosaicServeur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrmListenerController _frmListenerController;
        private ClientsListView _clientsListView;
        private Builder _builder;
        private Settings _settings;
        public static MainWindow instance;
        BrushConverter bc = new BrushConverter();

        public MainWindow()
        {
            instance = this;
            InitializeComponent();            
            GridSettings.Children.Clear();
            GridSettings.Children.Add(_clientsListView = new ClientsListView());
            ItemHome.IsSelected = true;
            Settings.updateMainUI += UIupdater;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            // Listener 
            _frmListenerController = new FrmListenerController();
            ListenerState.startListen = false;
            if (ListenerState.autoListen == true)
            {
                ListenerState.startListen = true;
                _frmListenerController.listen(ListenerState.listenPort, ListenerState.IPv6Support);
                //pipeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.LightbulbOn;
            }
            //  UserControls 
            _settings = new Settings(_frmListenerController);
            _builder = new Builder();

            if(ListenerState.ShowTermsAndConditions)
                ShowTermsAndConditions();
        }
        
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            ItemHome.Background     = (Brush)bc.ConvertFrom("#FF222222");
            ItemSettings.Background = (Brush)bc.ConvertFrom("#FF222222");
            ItemBuilder.Background  = (Brush)bc.ConvertFrom("#FF222222");
            ItemAbout.Background    = (Brush)bc.ConvertFrom("#FF222222");

            switch (index)
            {
                case 0:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(_clientsListView);                    
                    ItemHome.Background = (Brush)bc.ConvertFrom("#4A9EF5");
                    break;
                case 1:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(_settings);
                    ItemSettings.Background = (Brush)bc.ConvertFrom("#4A9EF5");
                    break;
                case 2:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(_builder);
                    ItemBuilder.Background = (Brush)bc.ConvertFrom("#4A9EF5");
                    break;
                case 3:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(new About());
                    ItemAbout.Background = (Brush)bc.ConvertFrom("#4A9EF5");
                    break;
                default:
                    break;
            }
        }

        private void click_ShowAbout(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ItemAbout.Background = (Brush)bc.ConvertFrom("#4A9EF5");
        }

        public void setListeningStatus(string status)
        {           
            statusBarListening.Dispatcher.BeginInvoke(new Action(delegate
            {
                lblListening.Text = status;
            }));
        }

        public void setWindowTitle(int connectedClients)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                this.Title = string.Format("Mosaique - [ Online Clients : {0} ]", connectedClients.ToString());
            }));
        }

        public void UIupdater(bool onOff)
        {
            Dispatcher.Invoke(new Action<bool>(Updater),onOff);
        }
        public void Updater(bool onOff)
        {
            //if (onOff == true)
            //{
            //    pipeIcon.Dispatcher.BeginInvoke(new Action(delegate
            //    {
            //        pipeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.LightbulbOn;
            //    }));
            //}
            //else
            //{
            //    pipeIcon.Dispatcher.BeginInvoke(new Action(delegate
            //    {
            //        pipeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Lightbulb;
            //    }));
            //}
        }

        private void ShowTermsAndConditions()
        {
            TermsAndConditions frm = new TermsAndConditions();
            frm.ShowDialog();            
            Thread.Sleep(300);
        }
    }
}
