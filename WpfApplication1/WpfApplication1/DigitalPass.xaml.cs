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

            if (Visitor.IsVisitor == 0)
            {
                this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
            }
            else
            {
                //its a parent signin to checkout student.
                this.NavigationService.Navigate(new Uri("ParentSignout.xaml", UriKind.Relative));
            }            
        }


        private void btnCreateDigtalPass_Click(object sender, RoutedEventArgs e)
        {
            Visitor.DigitalPass = true;

            if ((!txt_Phone.Text.Equals("(___) ___-____") && txt_Phone.Text != String.Empty) || (txt_Email.Text != String.Empty && ValidatorExtensions.IsValidEmailAddress(txt_Email.Text)))
            {
                // we must need to validate the email address before moving forward.
                Visitor.EmailAddress = txt_Email.Text;
                Visitor.PhoneNumber = txt_Phone.Text;

                if (Visitor.IsVisitor == 0)
                {
                    this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
                }
                else
                {
                    //its a parent signin to checkout student.
                    this.NavigationService.Navigate(new Uri("ParentSignout.xaml", UriKind.Relative));
                }                
            }
        }


        private void ResetData()
        {
            Visitor.DigitalPass  = false;
            Visitor.EmailAddress = "";
            Visitor.PhoneNumber  = "";

            txt_Email.Text = "";
            txt_Phone.Text = "";
        }
        

        private void txt_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool result = ValidatorExtensions.IsValidEmailAddress(txt_Email.Text);

            txt_Email.BorderBrush = (result == true ? Brushes.Green : Brushes.Red);            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (APIManager.KioskSettings.ContainsKey("send_digital_pass"))
            {
                string digitalPassType = APIManager.KioskSettings["send_digital_pass"].ToString().ToLower();

                if (digitalPassType.Equals("phone"))
                {
                    txt_Phone.Visibility = Visibility.Visible;
                    txt_Email.Visibility = Visibility.Collapsed;
                }
                else if(digitalPassType.Equals("email"))
                {
                    txt_Email.Visibility = Visibility.Visible;
                    txt_Phone.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txt_Email.Visibility = Visibility.Visible;
                    txt_Phone.Visibility = Visibility.Visible;
                }

                if(txt_Phone.Visibility == Visibility.Visible)
                {
                    txt_Phone.Select(1, 0);
                    txt_Phone.Focus();
                }
            }
            else
            {
                txt_Email.Visibility = Visibility.Visible;
                txt_Phone.Visibility = Visibility.Visible;
            }
        }
    }
}
