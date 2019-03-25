using Serveur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MosaicServeur.Main
{
    /// <summary>
    /// Logique d'interaction pour ClientsListView.xaml
    /// </summary>
    public partial class ClientsListView : UserControl
    {
            
        public ClientsListView()
        {
            InitializeComponent();
            //var MyItem = new MyItem { Ip= "salut", Name="çava", AccType = "bien", Country="oupas?",  Os="vraiment", Status="bien"};
            lvClients.Items.Add(new MyItem { Ip = "salut", Name = "çava", AccType = "bien", Country = "oupas?", Os = "vraiment", Status = "bien" });
        }

        private void testFrm(object sender, RoutedEventArgs e)
        {
            FrmRemoteDesktop frmRd = new FrmRemoteDesktop();
            frmRd.ShowDialog();
        }
    }

    public class MyItem
    {
        public string Ip { get; set; }

        public string Name { get; set; }

        public string AccType { get; set; }

        public string Country { get; set; }

        public string Os { get; set; }

        public string Status { get; set; }

    }
}
