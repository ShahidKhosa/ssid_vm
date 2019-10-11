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
    /// Interaction logic for SigninOptions.xaml
    /// </summary>
    public partial class SigninOptions : Page
    {
        public SigninOptions()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentSignin.xaml", UriKind.Relative));
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SearchStudent.xaml", UriKind.Relative));
        }
    }
}
