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
            Visitor.VisitDateTime = DateTime.Now;

            txtSchoolName.Text  = "New School Name";
            txtVisitorName.Text = (Visitor.FirstName + " " + Visitor.LastName);
            txtVisitorType.Text = "Visitor Type " + Visitor.CheckinOption;
            txtDestination.Text = "New Destination";

            txtDate.Text = Visitor.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Visitor.VisitDateTime.ToString("hh:mm:ss tt");

            string filePath = @"C:\SSID\" + Visitor.Image;
            imgVisitorImage.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));

            //Visitor.VisitDateTime.ToString("MM/dd/yyyy hh:mm:ss tt");            
        }
    }
}
