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
using CoreScanner;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Forms;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for Scan_License.xaml
    /// </summary>
    public partial class ScanLicense : Page
    {                
        public static ModalWindow mw;
        CCoreScannerClass m_pCoreScanner;

        _ICoreScannerEvents_BarcodeEventEventHandler BarcodeEventHandler;


        public ScanLicense()
        {
            InitializeComponent();
            Helper.UpdateLogoVisibility(footerBar);
        }

        /// <summary>
        /// BarcodeEvent received
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="scanData">Barcode string</param>
        public void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                string tmpScanData  = scanData;

                Visitor.BarcodeData = ViewBarcode.ShowBarcodeLabel(tmpScanData);

                if (APIManager.GetVisitorData())
                {
                    SetData();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Barcode Event issue " + e.Message);
            }
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
            }            
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
                        System.Windows.MessageBox.Show("Please enter valid date of birth (mm/dd/yyyy)");
                    }
                }
                catch(Exception ex)
                {
                    Helper.log.Error("Invalid visitor date of birth: " + ex.Message, ex);
                    System.Windows.MessageBox.Show("Please enter valid date of birth (mm/dd/yyyy)");
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
                        System.Windows.MessageBox.Show("Error, Please try again.");
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
                System.Windows.MessageBox.Show(ex.Message);
            }            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResetData();

            try
            {
                m_pCoreScanner = new CoreScanner.CCoreScannerClass(); 
                ViewBarcode viewBarcode = new ViewBarcode(m_pCoreScanner);
                //viewBarcode.ChangeScannerType();
                BarcodeEventHandler = new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
                m_pCoreScanner.BarcodeEvent += BarcodeEventHandler;                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }            
        }


        public void SetData()
        {            
            if (Visitor.BarcodeData != string.Empty)
            {                
                if (Visitor.FirstName != string.Empty)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txt_FirstName.Text = Visitor.FirstName;

                    }), DispatcherPriority.Background);                                        
                }

                if (Visitor.LastName != string.Empty)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txt_LastName.Text = Visitor.LastName;

                    }), DispatcherPriority.Background);                    
                }

                if (Visitor.DateOfBirth != string.Empty)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txt_DateOfBirth.Text = Visitor.DateOfBirth;

                    }), DispatcherPriority.Background);                    
                }

                if (Visitor.FirstName != string.Empty && Visitor.LastName != string.Empty)
                {
                    Visitor.IsOfficeUseOnly = false;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        btnConfirm.IsEnabled    = true;                        

                    }), DispatcherPriority.Background);                    
                }                    
            }
        }


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Executing the Open command");
            Visitor.BarcodeData = "@ANSI 636058050002DL00410217ZO02580064DLDAQY081724446DCSEADSDDENDACSTACYDDFNDADLYNNDDGNDCADDCBNONEDCDNONEDBC2DAU504DAYGRNDAG7016 STONYCREEK DRIVEDAIOKLAHOMACITYDAJOKDAK731320000DCFNONEDCGUSADAW118DBA07312019DBB12091979DBD08262015ZOZOANZOBNZOCRENEWALZODZOE5579ZOF55ZOG33.50ZOHZOINZOJN";
            SetData();                
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

            txt_FirstName.IsEnabled = false;
            txt_LastName.IsEnabled = false;
            txt_DateOfBirth.IsEnabled = false;
            btnConfirm.IsEnabled = false;            
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
                m_pCoreScanner.BarcodeEvent -= BarcodeEventHandler;

                if (mw != null)
                {
                    mw.Close();
                }                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

    }
}
