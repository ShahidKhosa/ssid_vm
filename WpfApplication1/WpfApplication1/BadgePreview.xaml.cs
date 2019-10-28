using PDFtoPrinter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for BadgePreview.xaml
    /// </summary>
    public partial class BadgePreview : Page
    {
        public BadgePreview()
        {
            InitializeComponent();
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
            }            
        }


        private void page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            btnNoBadgeNeeded.Visibility = (APIManager.KioskSettings["visitor_no_badge"].ToString().ToLower().Equals("on") ? Visibility.Visible : Visibility.Collapsed);            

            Visitor.VisitDateTime = DateTime.Now;

            txtSchoolName.Text  = APIManager.KioskSettings["school"].ToString();
            txtVisitorName.Text = (Visitor.FirstName + " " + Visitor.LastName);
            txtVisitorType.Text = Visitor.CheckinOption;
            txtDestination.Text = Visitor.Destination;

            txtDate.Text = "Check-in: " + Visitor.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Visitor.VisitDateTime.ToString("hh:mm:ss tt");
                        
            //imgVisitorImage.Source = new BitmapImage(new Uri(Visitor.CroppedImagePath, UriKind.Absolute));
            //Visitor.VisitDateTime.ToString("MM/dd/yyyy hh:mm:ss tt");            

            try
            {
                imgVisitorImage.Source = new BitmapImage(new Uri(Visitor.CroppedImagePath, UriKind.Absolute));
            }
            catch (System.NotSupportedException ex)
            {
                imgVisitorImage.Source = new BitmapImage(new Uri(Helper.DefaultImage, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                imgVisitorImage.Source = new BitmapImage(new Uri(Helper.DefaultImage, UriKind.Absolute));
            }
        }


        private async void btnYesPrint_Click(object sender, RoutedEventArgs e)
        {
            btnYesPrint.Content = "Please wait...";

            btn_GoBack.IsEnabled = false;
            btnYesPrint.IsEnabled = false;
            btnNoBadgeNeeded.IsEnabled = false;
            btnNoMakeChanges.IsEnabled = false;

            //Print Badge and complete visitor sign-in process and move to the next screen.
            APIManager.SendVisitorData(1);

            await Task.Delay(5000);

            this.NavigationService.Navigate(new Uri("PrintCompleted.xaml", UriKind.Relative));
        }


        private void btnNoMakeChanges_Click(object sender, RoutedEventArgs e)
        {
            //Make changes and go back to checkin option page.
            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }


        private async void btnNoBadgeNeeded_Click(object sender, RoutedEventArgs e)
        {
            APIManager.SendVisitorData(0);
            
            await Task.Delay(1000);
            //Just Complete visitor sign-in process and go back to the home page.
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            imgVisitorImage.Source = null;
        }
    }
}
