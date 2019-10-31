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
            Destinations destination = (Destinations)cmbDestination.SelectedItem;

            if (destination != null)
            {
                Visitor.Destination = (destination.ID == 10 ? txt_Destination.Text : destination.Destination);
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Destinations> destinations = new List<Destinations>();

            for (int i = 1; i < 10; i++)
            {
                string key = "destination" + i;

                if (APIManager.KioskSettings.ContainsKey(key) && APIManager.KioskSettings[key].ToString() != string.Empty)
                {
                    destinations.Add(new Destinations() { ID = i, Destination = APIManager.KioskSettings[key].ToString() });
                }
            }

            destinations.Add(new Destinations() { ID = 10, Destination = "Other" });

            cmbDestination.ItemsSource = destinations;
            cmbDestination.SelectedValuePath = "ID";
            cmbDestination.DisplayMemberPath = "Destination";
        }


        private void cmbDestination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Destinations destination = (Destinations)cmbDestination.SelectedItem;

            if (destination != null && destination.ID == 10)
            {
                txt_Destination.Text = "";
                txt_Destination.Visibility = Visibility.Visible;
            }
            else
            {
                txt_Destination.Text = "";
                txt_Destination.Visibility = Visibility.Collapsed;
            }
        }
    }


    public class Destinations
    {
        public int ID { get; set; }
        public string Destination { get; set; }
    }
}
