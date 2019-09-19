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
    /// Interaction logic for Scan_License.xaml
    /// </summary>
    public partial class ScanLicense : Page
    {
        public ScanLicense()
        {
            InitializeComponent();
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Visitor.FirstName = txt_FirstName.Text;
            Visitor.LastName = txt_LastName.Text;
            Visitor.DateOfBirth = txt_DateOfBirth.Text;

            this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));
        }

        private void btnOfficeUseOnlyClick(object sender, RoutedEventArgs e)
        {
            ModalWindow mw = new ModalWindow
            {
                scanLicense = this
            };
            //mw.txt_password.Text = "";
            mw.ShowDialog();
        }

        private void txtBarcodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.End)
            {
                string barcode = txtBarcodeData.Text;

                txtBarcodeData.Text = "";
            }
        }
    }
}
