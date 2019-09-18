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
    /// Interaction logic for TakePhoto.xaml
    /// </summary>
    public partial class TakePhoto : Page
    {
        WebCam webcam;

        public TakePhoto()
        {
            InitializeComponent();
        }

        private void page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgCapture);
            webcam.Start();
        }


        private void btnTakePhoto_Click(object sender, RoutedEventArgs e)
        {            
            Helper.SaveImageCapture((BitmapSource)imgCapture.Source);
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));
        }
    }
}
