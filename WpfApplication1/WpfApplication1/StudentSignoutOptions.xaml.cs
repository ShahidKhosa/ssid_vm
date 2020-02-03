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
    public partial class StudentSignoutOptions : Page
    {
        public StudentSignoutOptions()
        {
            InitializeComponent();
            Helper.UpdateLogoVisibility(footerBar);
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }


        private void btnStudent_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentSignout.xaml", UriKind.Relative));
        }


        private void btnParent_Click(object sender, RoutedEventArgs e)
        {
            Visitor.IsVisitor = 1;// 1 mean a parent is checking out a student 
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));            
        }
    }
}
