using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for StudentBadgePreview.xaml
    /// </summary>
    public partial class StudentBadgePreview : Page
    {
        public StudentBadgePreview()
        {
            InitializeComponent();
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentSigninReason.xaml", UriKind.Relative));
        }


        private void btnPrintStudentPass_Click(object sender, RoutedEventArgs e)
        {
            PrintBadge(btnPrintStudentPass, "STUDENT PASS");            
        }


        private void btnPrintTemporaryBadge_Click(object sender, RoutedEventArgs e)
        {
            PrintBadge(btnPrintTemporaryBadge, "TEMP BADGE");           
        }


        private void btnNoBadgeNeeded_Click(object sender, RoutedEventArgs e)
        {
            APIManager.SendStudentData(0, "");

            //Just Complete Student sign-in process and go back to the home page.
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void PrintBadge(Button button, String BadgeType)
        {
            button.Content = "Please wait...";

            btn_GoBack.IsEnabled = false;
            btnPrintStudentPass.IsEnabled = false;
            btnNoBadgeNeeded.IsEnabled = false;
            btnPrintTemporaryBadge.IsEnabled = false;

            //Print Badge and complete Student sign-in process and move to the next screen.
            APIManager.SendStudentData(1, BadgeType);

            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnNoBadgeNeeded.Visibility = (APIManager.KioskSettings["student_no_badge"].ToString().ToLower().Equals("on") ? Visibility.Visible : Visibility.Collapsed);

            Student.VisitDateTime = DateTime.Now;

            txtSchoolName.Text = APIManager.KioskSettings["school"].ToString();
            txtStudentName.Text = (Student.FirstName + " " + Student.LastName);
            txtSigninReason.Text = Student.CheckinOption;
            txtGrade.Text = Student.Grade;

            txtDate.Text = Student.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Student.VisitDateTime.ToString("hh:mm:ss tt");

            imgStudentImage.Source = new BitmapImage(new Uri(Student.CroppedImagePath, UriKind.Absolute));
            //Student.VisitDateTime.ToString("MM/dd/yyyy hh:mm:ss tt");  
        }
    }
}
