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
            if(APIManager.KioskSettings.ContainsKey("visitor_badge_options"))
            {
                int option = int.Parse(APIManager.KioskSettings["visitor_badge_options"].ToString());

                btnYesPrint.Visibility      = (option == 1 || option == 3 ? Visibility.Visible : Visibility.Collapsed);
                btnNoBadgeNeeded.Visibility = (option == 2 || option == 3 ? Visibility.Visible : Visibility.Collapsed);

                if (option == 2)
                {
                    btnNoBadgeNeeded.Background = btnYesPrint.Background;
                    btnNoBadgeNeeded.Content = "Submit";
                    btnNoBadgeNeeded.Foreground = Brushes.White;
                    PrintBadgeText.Text = "";
                    PageHeading.Height = 80;
                }
            }                        

            Visitor.VisitDateTime = DateTime.Now;

            txtSchoolName.Text  = APIManager.KioskSettings["school"].ToString();
            txtVisitorName.Text = (Visitor.FirstName + " " + Visitor.LastName);
            txtVisitorType.Text = Visitor.CheckinOption;
            txtDestination.Text = Visitor.Destination;

            txtDate.Text = "Check-in: " + Visitor.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Visitor.VisitDateTime.ToString("hh:mm:ss tt");

            vbVisitorName.Stretch = (txtVisitorName.Text.Length > 20 ? Stretch.Fill : Stretch.None);

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

            await Task.Delay(100);

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
            btnNoBadgeNeeded.Content = "Please wait...";
            btn_GoBack.IsEnabled = false;
            btnYesPrint.IsEnabled = false;
            btnNoBadgeNeeded.IsEnabled = false;
            btnNoMakeChanges.IsEnabled = false;

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
