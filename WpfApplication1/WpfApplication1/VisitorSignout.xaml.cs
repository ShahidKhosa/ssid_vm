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
    /// Interaction logic for VisitorSignout.xaml
    /// </summary>
    public partial class VisitorSignout : Page
    {
        public System.Windows.Forms.Timer tmrDelay;


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
            tmrDelay = new System.Windows.Forms.Timer();
            tmrDelay.Interval = 1000;
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
            APIManager.GetVisitorData("manage_signout_preview");

            if (Visitor.FirstName != string.Empty)
            {
                txt_FirstName.Text = Visitor.FirstName;
            }

            if (Visitor.LastName != string.Empty)
            {
                txt_LastName.Text = Visitor.LastName;
            }

            if (Visitor.FirstName != string.Empty && Visitor.LastName != string.Empty)
            {
                btnConfirm.IsEnabled = true;
            }            
        }


        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {            
            Visitor.BarcodeData = "97370-v";
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
