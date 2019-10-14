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

                //New Visitor take photo and then verify the data
               if (Visitor.IsVisitor == 1 || Visitor.IsOfficeUseOnly || Visitor.VisitorLiveImage == string.Empty)
                {
                    this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
                }
                //Visitor already have the photo, now verify the data
                else
                {                    
                    bool result = APIManager.VerifyVisitorData();

                    if(result)
                    {
                        if(Visitor.PassURL == String.Empty || Visitor.BarcodeData.Length > 40)
                        {
                            this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));                            
                        }
                        else
                        {
                            if (Visitor.IsVisitor == 0)
                            {
                                this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
                            }
                            else
                            {
                                //its a parent signin to checkout student.
                                this.NavigationService.Navigate(new Uri("ParentSignout.xaml", UriKind.Relative));
                            }                            
                        }                        
                    }
                    else
                    {
                        this.NavigationService.Navigate(new Uri("ScanFailure.xaml", UriKind.Relative));
                    }
                }                                
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
                //@ANSI 636058050002DL00410217ZO02580064DLDAQY081724446DCSEADSDDENDACSTACYDDFNDADLYNNDDGNDCADDCBNONEDCDNONEDBC2DAU504DAYGRNDAG7016 STONYCREEK DRIVEDAIOKLAHOMACITYDAJOKDAK731320000DCFNONEDCGUSADAW118DBA07312019DBB12091979DBD08262015ZOZOANZOBNZOCRENEWALZODZOE5579ZOF55ZOG33.50ZOHZOINZOJN
                Visitor.BarcodeData = txtBarcodeData.Text;
                SetData();

                txtBarcodeData.Focus();
                txtBarcodeData.Text = "";
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tmrDelay = new System.Windows.Forms.Timer();
            tmrDelay.Interval = 1200;
            tmrDelay.Enabled = false;
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
                    Visitor.BarcodeData = strCurrentString;
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
            if(Visitor.BarcodeData != string.Empty)
            {
                APIManager.GetVisitorData();

                if (Visitor.FirstName != string.Empty)
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


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Executing the Open command");
            Visitor.BarcodeData = "@ANSI 636058050002DL00410217ZO02580064DLDAQY081724446DCSEADSDDENDACSTACYDDFNDADLYNNDDGNDCADDCBNONEDCDNONEDBC2DAU504DAYGRNDAG7016 STONYCREEK DRIVEDAIOKLAHOMACITYDAJOKDAK731320000DCFNONEDCGUSADAW118DBA07312019DBB12091979DBD08262015ZOZOANZOBNZOCRENEWALZODZOE5579ZOF55ZOG33.50ZOHZOINZOJN";
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
