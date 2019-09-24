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
    /// Interaction logic for Destination.xaml
    /// </summary>
    public partial class Destination : Window
    {
        public CheckinReasons checkinReason;


        public Destination()
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
            if (txt_Destination.Text != String.Empty)
            {
                Visitor.Destination = txt_Destination.Text;
                this.Close();     
                
                if(checkinReason != null)
                {
                    checkinReason.BadgePreview();
                }
            }
            else
            {
                MessageBox.Show("Destination is required for the selected option");
                ResetData();
            }
        }


        private void ResetData()
        {
            Visitor.Destination = "";
        }


        private void txt_Destination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_Click(sender, e);
            }
        }
    }
}
