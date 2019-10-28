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
using System.Windows.Shapes;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ScanLicense scanLicense;

        public ModalWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
            ResetData();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {            
            if (txt_password.Password != String.Empty && txt_password.Password.Equals(APIManager.KioskSettings["office_use_only"].ToString()))
            {
                if(scanLicense != null)
                {
                    scanLicense.txt_FirstName.Text   = "";
                    scanLicense.txt_LastName.Text    = "";
                    scanLicense.txt_DateOfBirth.Text = "";

                    scanLicense.txt_FirstName.IsEnabled = true;
                    scanLicense.txt_LastName.IsEnabled = true;
                    scanLicense.txt_DateOfBirth.IsEnabled = true;
                    scanLicense.btnConfirm.IsEnabled = true;

                    scanLicense.txt_FirstName.Focus();
                    Visitor.IsOfficeUseOnly = true;
                    Visitor.OfficeUseOnlyPassword = txt_password.Password;

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Password not matched, please enter correct password to unlock fields");
                ResetData();
            }            
        }


        private void ResetData()
        {
            if(scanLicense.txt_FirstName.Text != string.Empty)
            {
                scanLicense.btnConfirm.IsEnabled = true;
            }

            Visitor.IsOfficeUseOnly = false;
            Visitor.OfficeUseOnlyPassword = "";

            scanLicense.ResetData();
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_Click(sender, e);
            }
        }
    }
}
