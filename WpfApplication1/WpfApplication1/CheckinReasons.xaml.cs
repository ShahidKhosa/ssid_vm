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
    /// Interaction logic for CheckinReasons.xaml
    /// </summary>
    public partial class CheckinReasons : Page
    {
        public CheckinReasons()
        {
            InitializeComponent();
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Visitor.CheckinOption = "";
            this.NavigationService.Navigate(new Uri("TakePhoto.xaml", UriKind.Relative));
        }


        private void btnOptionClick(object sender, RoutedEventArgs e)
        {
            string btnVal = ((FrameworkElement)sender).Tag.ToString();

            Visitor.CheckinOption = btnVal;

            int[] destinationArray = { 1, 2, 3, 4 };

            int value = Int32.Parse(btnVal);
            var index = Array.FindIndex(destinationArray, x => x == value);
            
            if (index > -1)
            {
                //show the destination window.

            }

            this.NavigationService.Navigate(new Uri("BadgePreview.xaml", UriKind.Relative));
        }
    }
}
