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

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }


        private void btn_Signin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("BadgePreview.xaml", UriKind.Relative));
            //this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
            //this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }

        private void btn_Signout_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}
