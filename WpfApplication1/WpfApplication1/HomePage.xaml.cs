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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            AppSettings.GetAppSettings();

            InitializeComponent();
            InitPage();
        }

        public void InitPage()
        {            
            if (APIManager.KioskSettings == null)
            {
                APIManager.GetKioskSettings();
            }

            txtWelcomeText.Text = APIManager.KioskSettings["welcome_text"].ToString().Replace("<br/>", "\r\n").Replace("<br>", "\r\n");

            imgSchoolLogo.Source = new BitmapImage(new Uri(APIManager.LogoPath, UriKind.Absolute));

            if (APIManager.KioskSettings.ContainsKey("app_settings"))
            {
                if (APIManager.KioskSettings["app_settings"].ToString().ToLower().Equals("off"))
                {
                    btnSettings.Visibility = Visibility.Hidden;
                }
            }
        }


        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            Visitor.ResetData();            
        }        


        private void btn_Signin_Click(object sender, RoutedEventArgs e)
        {
            Visitor.IsVisitor = 0;// Zero mean a visitor record.            
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }


        private void btn_Signout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("VisitorSignout.xaml", UriKind.Relative));
        }


        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.school_url.Text = AppSettings.JobNo;
            settings.homePage = this;

            loadPrinters(settings.printersList);            
            settings.ShowDialog();
        }


        public void loadPrinters(ComboBox printersList)
        {
            int printerIndex = 0;
            int i = 0;

            foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if(printerName.Equals(AppSettings.PrinterName))
                {
                    printerIndex = i;
                }

                printersList.Items.Add(printerName);

                i++;
            }

            printersList.SelectedIndex = printerIndex;

        }

        private void btnStudentSignin_Click(object sender, RoutedEventArgs e)
        {
            //if(APIManager.KioskSettings.ContainsKey("student_no_required") && APIManager.KioskSettings["student_no_required"].ToString().ToLower() == "no")
            //{
            //    //header('Location: ./visitor-management&a=search_student');
            //    this.NavigationService.Navigate(new Uri("VisitorSignout.xaml", UriKind.Relative));
            //}
            //else if (APIManager.KioskSettings.ContainsKey("tardy_pass_option") && APIManager.KioskSettings["tardy_pass_option"].ToString() == string.Empty)
            //{
            //    //header('Location: ./visitor-management&a=student_sign_in');
            //    this.NavigationService.Navigate(new Uri("VisitorSignout.xaml", UriKind.Relative));
            //}

            this.NavigationService.Navigate(new Uri("SigninOptions.xaml", UriKind.Relative));
        }

        private void btnStudentSignout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SearchStudent.xaml", UriKind.Relative));
        }
    }
}
