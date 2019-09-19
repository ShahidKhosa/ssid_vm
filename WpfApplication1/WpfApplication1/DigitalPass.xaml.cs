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
    /// Interaction logic for DigitalPass.xaml
    /// </summary>
    public partial class DigitalPass : Page
    {
        public DigitalPass()
        {
            InitializeComponent();
        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }

        private void btnNoThanks_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }

        private void btnCreateDigtalPass_Click(object sender, RoutedEventArgs e)
        {
            Visitor.DigitalPass = true;

            // we must need to validate the email address before moving forward.
            Visitor.EmailAddress = txt_Email.Text;
            Visitor.PhoneNumber  = txt_Phone.Text;

            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }

        private void ResetData()
        {
            Visitor.DigitalPass  = false;
            Visitor.EmailAddress = "";
            Visitor.PhoneNumber  = "";

            txt_Email.Text = "";
            txt_Phone.Text = "";
        }
    }
}
