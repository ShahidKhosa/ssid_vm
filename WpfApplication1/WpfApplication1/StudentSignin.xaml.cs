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
    /// Interaction logic for StudentSignin.xaml
    /// </summary>
    public partial class StudentSignin : Page
    {
        public System.Windows.Forms.Timer tmrDelay;


        public StudentSignin()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            if(tmrDelay != null)
            {
                tmrDelay.Stop();
            }

            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
            }            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty)
            {
                if (tmrDelay != null)
                {
                    tmrDelay.Stop();
                }

                this.NavigationService.Navigate(new Uri("StudentSigninReason.xaml", UriKind.Relative));
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResetData();

            tmrDelay = new System.Windows.Forms.Timer
            {
                Interval = 1000,
                Enabled = false
            };

            txtBarcodeData.Focus();
        }


        private void txtBarcodeData_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtBarcodeData.Text.Trim().Length == 1)
                {
                    tmrDelay.Enabled = true;
                    tmrDelay.Start();
                    tmrDelay.Tick += new EventHandler(tmrDelay_Tick);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void tmrDelay_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrDelay.Stop();
                string strCurrentString = txtBarcodeData.Text.Trim().ToString();
                if (strCurrentString != "")
                {
                    Student.BarcodeData = strCurrentString;
                    SetData();

                    //Do something with the barcode entered
                    txtBarcodeData.Text = "";
                }
                txtBarcodeData.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void SetData()
        {
            APIManager.GetStudentData();

            if (Student.FirstName != string.Empty)
            {
                txt_FirstName.Text = Student.FirstName;
            }

            if (Student.LastName != string.Empty)
            {
                txt_LastName.Text = Student.LastName;
            }

            if (Student.Grade != string.Empty)
            {
                txt_Grade.Text = Student.Grade;
            }


            if (Student.FirstName != string.Empty && Student.LastName != string.Empty)
            {
                txtBarcodeData.Focus();
                btnConfirm.IsEnabled = true;
            }
        }


        private void ResetData()
        {
            Student.ResetData();

            txt_FirstName.Text = "";
            txt_LastName.Text = "";
            txt_Grade.Text = "";
            txt_FirstName.IsEnabled = false;
            txt_LastName.IsEnabled = false;
            txt_Grade.IsEnabled = false;
            btnConfirm.IsEnabled = false;
            txtBarcodeData.Focus();
        }


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {            
            Student.BarcodeData = "900000039";
            SetData();

            txtBarcodeData.Focus();
            txtBarcodeData.Text = "";
        }


        public void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
