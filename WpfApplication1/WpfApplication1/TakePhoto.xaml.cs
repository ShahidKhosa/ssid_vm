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
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using Emgu.CV.WPF;
using log4net;
using System.Windows.Threading;
using System.IO;
using System.Drawing;


namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for TakePhoto.xaml
    /// </summary>
    public partial class TakePhoto : Page
    {
        private VideoCapture _capture = null;
        Image<Bgr, Byte> currentFrame = null;
        private CascadeClassifier _haarCascade;
        DispatcherTimer _timer;
        

        public TakePhoto()
        {
            InitializeComponent();
            LoadCamera();
            Helper.log.Info("Camera Loaded");
        }

        private void page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //LoadCamera();
        }


        private void btnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            SavePhoto();
        }


        private void SavePhoto()
        {
            btnTakePhoto.IsEnabled = false;
            btnTakePhoto.Content = "Processing...";

            Helper.SaveImageCapture(currentFrame);
            Helper.log.Info("Save Visitor Image");

            if (!Visitor.IsVerified)
            {
                bool result = APIManager.VerifyVisitorData();

                if (result)
                {
                    if (Visitor.PassURL == String.Empty || Visitor.BarcodeData.Length > 40)
                    {
                        this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));
                    }
                    else
                    {
                        if (Visitor.IsVisitor == 0)
                        {
                            this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
                        }
                        else
                        {
                            //its a parent signin to checkout student.
                            this.NavigationService.Navigate(new Uri("ParentSignout.xaml", UriKind.Relative));
                        }
                    }
                }
                else
                {
                    this.NavigationService.Navigate(new Uri("ScanFailure.xaml", UriKind.Relative));
                }
            }
            else
            {
                if (Visitor.IsVisitor == 0)
                {
                    this.NavigationService.Navigate(new Uri("CheckinReasons.xaml", UriKind.Relative));
                }
                else
                {
                    //its a parent signin to checkout student.
                    this.NavigationService.Navigate(new Uri("ParentSignout.xaml", UriKind.Relative));
                }
            }

        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {            
            //this.NavigationService.Navigate(new Uri("DigitalPass.xaml", UriKind.Relative));
            this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
        }


        private void LoadCamera()
        {
            _capture = new VideoCapture(0); 
            
            if(_capture.Height < 1 && _capture.Width < 1)
            {
                _capture = new VideoCapture(1);
            }

            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "haarcascade_frontalface_alt_tree.xml");

            _haarCascade = new CascadeClassifier(filePath);

            _timer = new DispatcherTimer();
            _timer.Tick += (_, __) =>
            {
                var cf = _capture.QueryFrame();                

                if (cf != null)
                {
                    currentFrame = cf.ToImage<Bgr, Byte>();                    
                    Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
                    
                    //var detectedFaces = _haarCascade.DetectMultiScale(grayFrame, 1.1, 10, System.Drawing.Size.Empty);                    
                    //var sdetectedFaces = _haarCascade.DetectMultiScale(grayFrame, 1.5, 0, new System.Drawing.Size(277, 352), new System.Drawing.Size(277, 352));                    
                    //foreach (var face in detectedFaces)
                    //{
                    //    currentFrame.Draw(face, new Bgr(System.Drawing.Color.Red), 1);
                    //}

                    currentFrame.Draw(new System.Drawing.Rectangle(174, 59, 277, 352), new Bgr(0, double.MaxValue, 0), 1);

                    imgCapture.Source = BitmapSourceConvert.ToBitmapSource(currentFrame);
                }
            };

            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timer.Start();

        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_capture != null)
                _capture.Dispose();//worked but crashed due to memory issue
        }
    }
}
