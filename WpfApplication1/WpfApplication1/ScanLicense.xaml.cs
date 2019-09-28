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
    /// Interaction logic for Scan_License.xaml
    /// </summary>
    public partial class ScanLicense : Page
    {
        public System.Windows.Forms.Timer tmrDelay;


        public ScanLicense()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty && txt_DateOfBirth.Text != String.Empty)
            {
                Visitor.FirstName   = txt_FirstName.Text;
                Visitor.LastName    = txt_LastName.Text;
                Visitor.DateOfBirth = txt_DateOfBirth.Text;

                APIManager.VerifyVisitorData();

                this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));
            }
        }


        private void btnOfficeUseOnlyClick(object sender, RoutedEventArgs e)
        {
            ModalWindow mw = new ModalWindow
            {
                scanLicense = this
            };
            //mw.txt_password.Text = "";
            mw.ShowDialog();
        }


        private void txtBarcodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.End)
            {
                Visitor.BarcodeData = txtBarcodeData.Text;
                SetData();

                txtBarcodeData.Focus();
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tmrDelay = new System.Windows.Forms.Timer();
            tmrDelay.Interval = 1000;
            tmrDelay.Enabled = false;
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
                    Visitor.BarcodeData = strCurrentString;
                    SetData();

                    //Do something with the barcode entered
                    //txtBarcodeData.Text = "";
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
            APIManager.GetVisitorData();

            if(Visitor.FirstName != string.Empty)
            {
                txt_FirstName.Text = Visitor.FirstName;
            }

            if (Visitor.LastName != string.Empty)
            {
                txt_LastName.Text = Visitor.LastName;
            }

            if (Visitor.DateOfBirth != string.Empty)
            {
                txt_DateOfBirth.Text = Visitor.DateOfBirth;
            }
            
            Visitor.IsOfficeUseOnly = false;
            btnConfirm.IsEnabled = true;
        }

    }
}
