using System;
using System.Collections.Generic;
using System.Globalization;
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
using TouchKeyboardSample.Extensions;
using TouchKeyboardSample.Providers;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for Scan_License.xaml
    /// </summary>
    public partial class ScanLicense : Page
    {        
        public System.Windows.Forms.Timer tmrDelay;
        private readonly ITouchKeyboardProvider _touchKeyboardProvider = new TouchKeyboardProvider();
        public static ModalWindow mw;


        public ScanLicense()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private bool ValidateDOB()
        {
            if(txt_DateOfBirth.Text != String.Empty && !txt_DateOfBirth.Text.Contains("_"))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(txt_DateOfBirth.Text.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    if (dt.Year > (DateTime.Now.Year - 80) && dt.Year < (DateTime.Now.Year - 15))
                    {
                        return true;
                    }
                    else
                    {
                        Helper.log.Info("Out of range visitor dob: " + txt_DateOfBirth.Text);
                        MessageBox.Show("Please enter valid date of birth (mm/dd/yyyy)");
                    }
                }
                catch(Exception ex)
                {
                    Helper.log.Error("Invalid visitor date of birth: " + ex.Message, ex);
                    MessageBox.Show("Please enter valid date of birth (mm/dd/yyyy)");
                }
            }                       

            return false;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty && ValidateDOB())
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
                    int result = APIManager.VerifyVisitorData();

                    if(result == 1)
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
                    else if(result == 0)
                    {
                        this.NavigationService.Navigate(new Uri("ScanFailure.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Error, Please try again.");
                        btn_Home_Click(sender, e);
                    }
                }                                
            }
        }


        private void btnOfficeUseOnlyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mw != null)
                {
                    mw.Close();
                }

                mw = new ModalWindow
                {
                    scanLicense = this
                };
                
                mw.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResetData();        
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

                SetBarcodeFocus();

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

                if (Visitor.FirstName != string.Empty && Visitor.LastName != string.Empty)
                {
                    Visitor.IsOfficeUseOnly = false;
                    btnConfirm.IsEnabled = true;

                    formWrapper.Visibility = Visibility.Visible;
                }                    
            }
        }


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Executing the Open command");
            Visitor.BarcodeData = "@ANSI 636058050002DL00410217ZO02580064DLDAQY081724446DCSEADSDDENDACSTACYDDFNDADLYNNDDGNDCADDCBNONEDCDNONEDBC2DAU504DAYGRNDAG7016 STONYCREEK DRIVEDAIOKLAHOMACITYDAJOKDAK731320000DCFNONEDCGUSADAW118DBA07312019DBB12091979DBD08262015ZOZOANZOBNZOCRENEWALZODZOE5579ZOF55ZOG33.50ZOHZOINZOJN";
            SetData();

            SetBarcodeFocus();
            txtBarcodeData.Text = "";            
        }


        public void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void txt_DateOfBirth_GotFocus(object sender, RoutedEventArgs e)
        {   
            if(txt_DateOfBirth.Text == string.Empty)
            {
                txt_DateOfBirth.Mask = "00/00/0000";                
            }

            txt_DateOfBirth.Select(0, 0);
        }


        public void ResetData()
        {
            int isVisitor = Visitor.IsVisitor;
            Visitor.ResetData();
            Visitor.IsVisitor = isVisitor;

            txt_FirstName.Text = "";
            txt_LastName.Text = "";
            txt_DateOfBirth.Text = "";

            Visitor.IsOfficeUseOnly = false;
            Visitor.OfficeUseOnlyPassword = "";
            formWrapper.Visibility = Visibility.Collapsed;

            txt_FirstName.IsEnabled = false;
            txt_LastName.IsEnabled = false;
            txt_DateOfBirth.IsEnabled = false;
            btnConfirm.IsEnabled = false;            

            tmrDelay = new System.Windows.Forms.Timer();
            tmrDelay.Interval = 1200;
            tmrDelay.Enabled = false;
            SetBarcodeFocus();
        }


        private void txt_DateOfBirth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_DateOfBirth.Text == string.Empty || txt_DateOfBirth.Text.Equals("__/__/____"))
            {
                txt_DateOfBirth.Mask = "";                
            }
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mw != null)
                {
                    mw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void SetBarcodeFocus()
        {
            if(!Visitor.IsOfficeUseOnly)
            {
                txtBarcodeData.Focus();
            }            
        }

    }
}
