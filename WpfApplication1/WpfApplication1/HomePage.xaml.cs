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
using FluentScheduler;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static HomePage ThisThis;


        public HomePage()
        {            
            AppSettings.GetAppSettings();

            InitializeComponent();
            InitPage();

            ThisThis = this;
        }


        public void InitPage()
        {
            try
            {
                if (APIManager.KioskSettings == null)
                {
                    APIManager.GetKioskSettings();
                }

                txtWelcomeText.Text = APIManager.KioskSettings["welcome_text"].ToString().Replace("<br/>", "\r\n").Replace("<br>", "\r\n");

                imgSchoolLogo.Source = new BitmapImage(new Uri(APIManager.LogoPath, UriKind.Absolute));

                if (APIManager.KioskSettings.ContainsKey("app_settings"))
                {
                    if (APIManager.KioskSettings["app_settings"].ToString().ToLower().Equals("on"))
                    {
                        //btnExit.Visibility = Visibility.Collapsed;                        
                        btnSettings.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //btnExit.Visibility = Visibility.Visible;
                        btnSettings.Visibility = Visibility.Collapsed;                        
                    }
                }
                else
                {
                    //btnExit.Visibility = Visibility.Collapsed;
                    btnSettings.Visibility = Visibility.Collapsed;
                }

                if (APIManager.KioskSettings.ContainsKey("turn_student_kiosk"))
                {
                    if (APIManager.KioskSettings["turn_student_kiosk"].ToString().ToLower().Equals("on"))
                    {
                        btnStudentSignin.Visibility = Visibility.Visible;
                        btnStudentSignout.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnStudentSignin.Visibility = Visibility.Collapsed;
                        btnStudentSignout.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    btnStudentSignin.Visibility = Visibility.Visible;
                    btnStudentSignout.Visibility = Visibility.Visible;
                }

                Helper.CleanExtraResources();
            }
            catch(Exception ex)
            {
                Helper.log.Error("Home Page Error", ex);
            }            
        }


        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            Visitor.ResetData();
            Student.ResetData();

            //UpdateManager.InstallUpdateSyncWithInfo();

            //Helper.ShowAppDownloadMessage();
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
            Helper.log.Info(System.Drawing.Printing.PrinterSettings.InstalledPrinters);

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
            if (APIManager.KioskSettings.ContainsKey("student_no_required") && APIManager.KioskSettings["student_no_required"].ToString().ToLower() == "no")
            {                
                this.NavigationService.Navigate(new Uri("SearchStudent.xaml", UriKind.Relative));
            }
            else if (APIManager.KioskSettings.ContainsKey("tardy_pass_option") && APIManager.KioskSettings["tardy_pass_option"].Equals("student_sign_in"))
            {             
                this.NavigationService.Navigate(new Uri("StudentSignin.xaml", UriKind.Relative));
            }
            else
            {
                this.NavigationService.Navigate(new Uri("SigninOptions.xaml", UriKind.Relative));
            }            
        }


        private void btnStudentSignout_Click(object sender, RoutedEventArgs e)
        {
            Visitor.IsVisitor = 1;// 1 mean a parent is checking out a student 
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));            
        }


        public void ReturnToHome()
        {
            Visitor.IsVisitor = 0;// Zero mean a visitor record.            
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // Shutdown the application.
            Application.Current.Shutdown();
        }
    }
}
