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
    /// Interaction logic for VisitorSignout.xaml
    /// </summary>
    public partial class VisitorSignout : Page
    {        
        public static ModalWindow mw;
        CCoreScannerClass m_pCoreScanner;
        _ICoreScannerEvents_BarcodeEventEventHandler BarcodeEventHandler;


        public VisitorSignout()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty && Visitor.ID > 0)
            {                
                APIManager.Signout();

                this.NavigationService.Navigate(new Uri("SignoutCompleted.xaml", UriKind.Relative));
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
                string tmpScanData  = scanData;
                Visitor.BarcodeData = ViewBarcode.ShowBarcodeLabel(tmpScanData);                

                if (APIManager.GetVisitorData("manage_signout_preview"))
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

            if (Visitor.FirstName != string.Empty && Visitor.LastName != string.Empty)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnConfirm.IsEnabled = true;
                    formWrapper.Visibility = Visibility.Visible;

                }), DispatcherPriority.Background);
            }            
        }


        public void ResetData()
        {            
            Visitor.ResetData();         

            txt_FirstName.Text = "";
            txt_LastName.Text = "";                        
            formWrapper.Visibility = Visibility.Collapsed;

            txt_FirstName.IsEnabled = false;
            txt_LastName.IsEnabled = false;            
            btnConfirm.IsEnabled = false;
        }



        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {            
            Visitor.BarcodeData = "97370-v";

            if (APIManager.GetVisitorData("manage_signout_preview"))
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
