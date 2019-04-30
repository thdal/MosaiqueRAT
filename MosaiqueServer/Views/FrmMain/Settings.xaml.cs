using Serveur.Controllers;
using Serveur.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MosaicServeur
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        private int PORT = 0;
        private FrmListenerController _frmListenerController;
        public delegate void UpdateMainUI(bool onOff);        // UPDATE MAIN UI
        public static event UpdateMainUI updateMainUI;                   // UPDATE MAIN UI

        public Settings(FrmListenerController frmListenerController)
        {
            _frmListenerController = frmListenerController;
            InitializeComponent();
            PORT = ListenerState.listenPort;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            txtPort.Text = ListenerState.listenPort.ToString();

            if (ListenerState.startListen == true)
            {
                btnListen.Content = "Stop listening";
            }

            chkStartupConnections.IsChecked = ListenerState.autoListen;
            chkPopupNotification.IsChecked = ListenerState.showPopup;
            chkIPv6.IsChecked = ListenerState.IPv6Support;
        }

        private void btnListening(object sender, RoutedEventArgs e)
        {
            if (btnListen.Content.ToString() == "Start listening")
            {
                ListenerState.listenPort = Convert.ToInt32(txtPort.Text);
                ListenerState.startListen = true;
                _frmListenerController.listen(int.Parse(txtPort.Text), chkIPv6.IsChecked.Value);
                btnListen.Content = "Stop listening";
                updateMainUI(true);
            }
            else
            {
                ListenerState.startListen = false;
                _frmListenerController.stopListening();
                btnListen.Content = "Start listening";
                updateMainUI(false);
            }
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            ListenerState.autoListen = chkStartupConnections.IsChecked.Value;
            ListenerState.showPopup = chkPopupNotification.IsChecked.Value;
            ListenerState.IPv6Support = chkIPv6.IsChecked.Value;
            ListenerState.listenPort = int.Parse(txtPort.Text);
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            chkStartupConnections.IsChecked = ListenerState.autoListen;
            chkPopupNotification.IsChecked = ListenerState.showPopup;
            chkIPv6.IsChecked = ListenerState.IPv6Support;
            txtPort.Text = ListenerState.listenPort.ToString();
        }

        //  NumPort EVENT
        public int numPort
        {
            get { return PORT; }
            set
            {
                if (PORT >= 0)
                    PORT = value;

                txtPort.Text = PORT.ToString();
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(txtPort.Text)) < 65535)
                numPort++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(txtPort.Text)) > 0)
                numPort--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPort == null)
            {
                return;
            }
           
            if (!int.TryParse(txtPort.Text, out PORT))
            {
                txtPort.Text = PORT.ToString();
            }

            if ((int.Parse(txtPort.Text) > 65535))
            {
                txtPort.Text = 0.ToString();
            }

            if (!(int.Parse(txtPort.Text) > 0))
            {
                txtPort.Text = 0.ToString();
            }
        }
    }
}
