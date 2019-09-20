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
            ResetData();
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(txt_password.Password != String.Empty && txt_password.Password.Equals("1234"))
            {
                if(scanLicense != null)
                {
                    scanLicense.txt_FirstName.IsEnabled = true;
                    scanLicense.txt_LastName.IsEnabled = true;
                    scanLicense.txt_DateOfBirth.IsEnabled = true;
                    scanLicense.btnConfirm.IsEnabled = true;

                    Visitor.IsOfficeUseOnly = true;
                    Visitor.OfficeUseOnlyPassword = txt_password.Password;
                }
            }
            else
            {
                MessageBox.Show("Password not matched, please enter correct password to unlock fields");
                ResetData();
            }

            this.Close();
        }


        private void ResetData()
        {
            Visitor.IsOfficeUseOnly = false;
            Visitor.OfficeUseOnlyPassword = "";

            scanLicense.txt_FirstName.IsEnabled = false;
            scanLicense.txt_LastName.IsEnabled = false;
            scanLicense.txt_DateOfBirth.IsEnabled = false;
            scanLicense.btnConfirm.IsEnabled = false;
            scanLicense.txtBarcodeData.Focus();
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
