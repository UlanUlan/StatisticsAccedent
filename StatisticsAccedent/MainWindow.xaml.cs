using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace StatisticsAccedent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CenterFrame.Source = new Uri("Pages/Page_Panel.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            CenterFrame.Source = new Uri("Pages/Page_Stat.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
