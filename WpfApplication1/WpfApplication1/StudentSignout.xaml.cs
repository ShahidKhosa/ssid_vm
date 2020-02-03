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
using CoreScanner;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Forms;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for StudentSignin.xaml
    /// </summary>
    public partial class StudentSignout : Page
    {
        public static ModalWindow mw;
        CCoreScannerClass m_pCoreScanner;
        _ICoreScannerEvents_BarcodeEventEventHandler BarcodeEventHandler;        


        public StudentSignout()
        {
            InitializeComponent();
            Helper.UpdateLogoVisibility(footerBar);
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty)
            {
                var takePhoto = new TakePhoto
                {
                    IsStudentCheckout = true
                };

                this.NavigationService.Navigate(takePhoto);

                //this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative), );                
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            ResetData();

            try
            {
                m_pCoreScanner = new CoreScanner.CCoreScannerClass();
                ViewBarcode viewBarcode = new ViewBarcode(m_pCoreScanner);
                BarcodeEventHandler = new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
                m_pCoreScanner.BarcodeEvent += BarcodeEventHandler;
            }
            catch (Exception ex)
            {

            }
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
                string tmpScanData = scanData;

                Student.BarcodeData = ViewBarcode.ShowBarcodeLabel(tmpScanData);

                if (APIManager.GetStudentData())
                {
                    SetData();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Barcode Event issue " + e.Message);
            }
        }


        public void SetData()
        {            
            if (Student.FirstName != string.Empty)
            {                
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_FirstName.Text = Student.FirstName;

                }), DispatcherPriority.Background);
            }

            if (Student.LastName != string.Empty)
            {                
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_LastName.Text = Student.LastName;

                }), DispatcherPriority.Background);
            }

            if (Student.Grade != string.Empty)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_Grade.Text = Student.Grade;

                }), DispatcherPriority.Background);                
            }


            if (Student.FirstName != string.Empty && Student.LastName != string.Empty)
            {                                
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnConfirm.IsEnabled = true;                    

                }), DispatcherPriority.Background);
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
        }


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {            
            Student.BarcodeData = "900000039";

            if (APIManager.GetStudentData())
            {
                SetData();
            }
        }


        public void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
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
