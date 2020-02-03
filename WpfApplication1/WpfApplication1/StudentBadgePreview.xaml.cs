using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            Helper.UpdateLogoVisibility(footerBar);
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("StudentSigninReason.xaml", UriKind.Relative));
            }            
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
            btnNoBadgeNeeded.Content = "Please wait...";

            btn_GoBack.IsEnabled = false;
            btnPrintStudentPass.IsEnabled = false;
            btnNoBadgeNeeded.IsEnabled = false;
            btnPrintTemporaryBadge.IsEnabled = false;

            APIManager.SendStudentData(0, "");

            //Just Complete Student sign-in process and go back to the home page.
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private async void PrintBadge(Button button, String BadgeType)
        {
            button.Content = "Please wait...";

            btn_GoBack.IsEnabled = false;
            btnPrintStudentPass.IsEnabled = false;
            btnNoBadgeNeeded.IsEnabled = false;
            btnPrintTemporaryBadge.IsEnabled = false;

            await Task.Delay(100);

            //Print Badge and complete Student sign-in process and move to the next screen.
            APIManager.SendStudentData(1, BadgeType);

            await Task.Delay(5000);
            this.NavigationService.Navigate(new Uri("PrintCompleted.xaml", UriKind.Relative));
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            if (APIManager.KioskSettings.ContainsKey("student_badge_options"))
            {
                int option = int.Parse(APIManager.KioskSettings["student_badge_options"].ToString());

                btnPrintTemporaryBadge.Visibility = btnPrintStudentPass.Visibility = (option == 1 || option == 3 ? Visibility.Visible : Visibility.Collapsed);                               

                btnNoBadgeNeeded.Visibility = (option == 2 || option == 3 ? Visibility.Visible : Visibility.Collapsed);

                if (option == 2)
                {                    
                    btnNoBadgeNeeded.Background = btnPrintStudentPass.Background;
                    btnNoBadgeNeeded.Content    = "Submit";
                    btnNoBadgeNeeded.Foreground = Brushes.White;
                    PrintBadgeText.Text = "";
                    PageHeading.Height = 80;
                }
            }            

            Student.VisitDateTime = DateTime.Now;

            txtSchoolName.Text = APIManager.KioskSettings["school"].ToString();
            txtStudentName.Text = (Student.FirstName + " " + Student.LastName);
            txtSigninReason.Text = Student.CheckinOption;
            txtGrade.Text = Student.Grade;

            txtDate.Text = "Check-in: " + Student.VisitDateTime.ToString("MM/dd/yyyy");
            txtTime.Text = Student.VisitDateTime.ToString("hh:mm:ss tt");
            vbStudentName.Stretch = (txtStudentName.Text.Length > 20 ? Stretch.Fill : Stretch.None);

            try
            {
                await Task.Delay(100);

                imgStudentImage.Source = new BitmapImage(new Uri(Student.ImagePath, UriKind.Absolute));
            }
            catch (System.NotSupportedException ex)
            {                
                imgStudentImage.Source = new BitmapImage(new Uri(Helper.DefaultImage, UriKind.Relative));
            }
            catch(Exception ex)
            {
                imgStudentImage.Source = new BitmapImage(new Uri(Helper.DefaultImage, UriKind.Relative));
            }
                        
            //Student.VisitDateTime.ToString("MM/dd/yyyy hh:mm:ss tt");  
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            imgStudentImage.Source = null;
        }
    }
}
