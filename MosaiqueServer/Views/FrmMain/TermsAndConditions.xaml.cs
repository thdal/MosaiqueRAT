using Serveur.Models;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace MosaicServeur.Views.FrmMain
{
    /// <summary>
    /// Logique d'interaction pour About.xaml
    /// </summary>
    public partial class TermsAndConditions : Window
    {
        private static bool _agree { get; set; }

        public TermsAndConditions()
        {
            InitializeComponent();
            _agree = false;
            lblTermsOfUse.Text = (MosaiqueServeur.Properties.Resources.TermsofUse).ToUpper();
        }

        private void FormLoad(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(Wait20Sec) { IsBackground = true };
            t.Start();
        }

        private void btnAgree_Click(object sender, RoutedEventArgs e)
        {
            var ShowTandC = !(chkShowTandC.IsChecked.Value);
            ListenerState.ShowTermsAndConditions = ShowTandC;
            _agree = true;
            this.Close();
        }

        private void btnDecline_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Wait20Sec()
        {
            for (int i = 19; i >= 0; i--)
            {
                Thread.Sleep(1000);
                try
                {
                    this.Dispatcher.Invoke((MethodInvoker)delegate { btnAgree.Content = "Agree (" + i + ")"; });
                }
                catch
                {
                }
            }

            this.Dispatcher.Invoke((MethodInvoker)delegate
            {
                btnAgree.Content = "Agree";
                btnAgree.IsEnabled = true;
            });
        }

        private void FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_agree)
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
