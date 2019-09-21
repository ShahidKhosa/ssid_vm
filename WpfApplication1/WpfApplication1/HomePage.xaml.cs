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
            Visitor.ResetData();
        }


        private void btn_Signin_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("BadgePreview.xaml", UriKind.Relative));
            //this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }

        private void btn_Signout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.school_url.Text = Properties.Settings.Default.school_url;

            loadPrinters(settings.printersList);            
            settings.ShowDialog();
        }


        public void loadPrinters(ComboBox printersList)
        {
            int printerIndex = 0;
            int i = 0;

            foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if(printerName.Equals(Properties.Settings.Default.printer_name))
                {
                    printerIndex = i;
                }

                printersList.Items.Add(printerName);

                i++;
            }

            printersList.SelectedIndex = printerIndex;

        }
    }
}
