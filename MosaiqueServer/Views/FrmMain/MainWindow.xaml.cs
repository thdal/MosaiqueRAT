using MosaicServeur.Main;
using System.Windows;
using System.Windows.Controls;

namespace MosaicServeur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(new ClientsListView());
                    break;
                case 1:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(new Settings());
                    break;
                case 2:
                    GridSettings.Children.Clear();
                    GridSettings.Children.Add(new Builder());
                    break;
                default:
                    break;
            }
        }
    }
}
