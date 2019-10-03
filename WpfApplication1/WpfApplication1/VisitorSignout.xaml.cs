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
            if (txt_FirstName.Text != String.Empty && txt_LastName.Text != String.Empty)
            {
                Visitor.FirstName = txt_FirstName.Text;
                Visitor.LastName = txt_LastName.Text;

                bool result = APIManager.VerifyVisitorData();

                if (result)
                {
                    this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Error! please try again.");
                }
            }
        }




        private void txtBarcodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.End)
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
            APIManager.GetVisitorData("manage_signout_preview");

            if (Visitor.FirstName != string.Empty)
            {
                txt_FirstName.Text = Visitor.FirstName;
            }

            if (Visitor.LastName != string.Empty)
            {
                txt_LastName.Text = Visitor.LastName;
            }
            
            btnConfirm.IsEnabled = true;
        }

    }
}
