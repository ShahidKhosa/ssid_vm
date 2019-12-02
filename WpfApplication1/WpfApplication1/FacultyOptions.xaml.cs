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
    /// Interaction logic for FacultyOptions.xaml
    /// </summary>
    public partial class FacultyOptions : Page
    {
        public FacultyOptions()
        {
            InitializeComponent();
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("FacultySignin.xaml", UriKind.Relative));
        }

        private void btnOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("FacultySignout.xaml", UriKind.Relative));
        }
    }
}
