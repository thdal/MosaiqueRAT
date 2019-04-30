using System.Windows.Controls;

namespace MosaicServeur.Views.FrmMain
{
    /// <summary>
    /// Logique d'interaction pour About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            lblAbout.Text = MosaiqueServeur.Properties.Resources.License;
        }
    }
}
