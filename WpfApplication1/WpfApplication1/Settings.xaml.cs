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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            school_url.Text = Properties.Settings.Default.school_url;            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.school_url  = school_url.Text.ToString();
            Properties.Settings.Default.printer_name = printersList.SelectedValue.ToString();

            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
