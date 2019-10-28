using System;
using System.Windows;
using System.Windows.Controls;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for StudentSigninReason.xaml
    /// </summary>
    public partial class StudentSigninReason : Page
    {
        public StudentSigninReason()
        {
            InitializeComponent();
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Student.CheckinOption = "";
            Student.CheckinOptionNumber = "";

            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("StudentSignin.xaml", UriKind.Relative));
            }            
        }


        private void btnOptionClick(object sender, RoutedEventArgs e)
        {
            Student.CheckinOptionNumber = ((FrameworkElement)sender).Tag.ToString();

            Student.CheckinOption = APIManager.KioskSettings["lateness_reason" + Student.CheckinOptionNumber].ToString();

            this.NavigationService.Navigate(new Uri("StudentBadgePreview.xaml", UriKind.Relative));
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            setOption("lateness_reason1", btnOption1);
            setOption("lateness_reason2", btnOption2);
            setOption("lateness_reason3", btnOption3);
            setOption("lateness_reason4", btnOption4);
            setOption("lateness_reason5", btnOption5);
            setOption("lateness_reason6", btnOption6);
            setOption("lateness_reason7", btnOption7);
            setOption("lateness_reason8", btnOption8);
            setOption("lateness_reason9", btnOption9);
        }


        private void setOption(string lateness_reason, Button button)
        {
            if (APIManager.KioskSettings[lateness_reason] != null && APIManager.KioskSettings[lateness_reason].ToString() != String.Empty)
            {
                button.Content = APIManager.KioskSettings[lateness_reason].ToString();
                button.Visibility = Visibility.Visible;
            }
            else
            {
                button.Content = "";
                button.Visibility = Visibility.Hidden;
            }
        }

    }
}
