using PDFtoPrinter;
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
            this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
        }

        private void page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            btnNoBadgeNeeded.Visibility = (APIManager.values["visitor_no_badge"].ToString().ToLower().Equals("on") ? Visibility.Visible : Visibility.Collapsed);            

            Visitor.VisitDateTime = DateTime.Now;

            txtSchoolName.Text  = APIManager.values["school"].ToString();
            txtVisitorName.Text = (Visitor.FirstName + " " + Visitor.LastName);
            txtVisitorType.Text = Visitor.CheckinOption;
            txtDestination.Text = "New Destination";

            txtDate.Text = Visitor.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Visitor.VisitDateTime.ToString("hh:mm:ss tt");


            string path = Helper.GetPath("\\" + APIManager.values["job_id"]);
            string ImagePath = String.Format("{0}\\{1}", path, "visitor_croped.jpg");
            
            imgVisitorImage.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            //Visitor.VisitDateTime.ToString("MM/dd/yyyy hh:mm:ss tt");            
        }

        private void btnYesPrint_Click(object sender, RoutedEventArgs e)
        {
            //Print Badge and complete visitor sign-in process and move to the next screen.
            //var filePath = @"D:\TestPrint\visitor-card-97328.pdf";
            //var printWrapper = new PDFtoPrintWrapper();
            //printWrapper.Print(filePath, Properties.Settings.Default.printer_name);

            this.NavigationService.Navigate(new Uri("PrintCompleted.xaml", UriKind.Relative));
        }

        private void btnNoMakeChanges_Click(object sender, RoutedEventArgs e)
        {
            //Make changes and go back to checkin option page.
            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }

        private void btnNoBadgeNeeded_Click(object sender, RoutedEventArgs e)
        {
            Visitor.ResetData();

            //Just Complete visitor sign-in process and go back to the home page.
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }
    }
}
