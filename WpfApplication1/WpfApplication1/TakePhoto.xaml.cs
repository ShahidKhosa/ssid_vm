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
        public bool IsStudentCheckout = false;
        

        public TakePhoto()
        {
            InitializeComponent();
            Helper.UpdateLogoVisibility(footerBar);
            IsStudentCheckout = false;

            Helper.log.Info(" Beore Camera Load.");

            LoadCamera();            
        }


        private void btnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            btnTakePhoto.IsEnabled = false;
            btnTakePhoto.Content = "Processing...";
            btnBack.IsEnabled = false;

            SavePhoto();

        }


        private async void SavePhoto()
        {
            await Task.Delay(100);

            Helper.SaveImageCapture(currentFrame);
            Helper.log.Info("Save Visitor Image");

            if(IsStudentCheckout)
            {
                APIManager.Signout();

                this.NavigationService.Navigate(new Uri("SignoutCompleted.xaml", UriKind.Relative));
            }

            else if (Visitor.IsVerified != 1)
            {
                int result = APIManager.VerifyVisitorData();

                if (result == 1)
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
                else if (result == 0)
                {
                    this.NavigationService.Navigate(new Uri("ScanFailure.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Error, Please try again.");
                    this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
                }
            }
            else if(Visitor.IsVerified == 1)
            {                
                if (Visitor.IsVisitor == 1 || Visitor.IsOfficeUseOnly)
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
        }


        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("ScanLicense.xaml", UriKind.Relative));
            }            
        }


        private void LoadCamera()
        {
            try
            {
                _capture = new VideoCapture(0);

                //if(_capture.Height < 1 && _capture.Width < 1)
                //{
                //    _capture = new VideoCapture(1);
                //}

                Helper.log.Info(" Camera Width: " + _capture.Width);
                Helper.log.Info(" Camera Height: " + _capture.Height);                

                //string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "haarcascade_frontalface_alt_tree.xml");
                //Helper.log.Info(" haarcascade_frontalface_alt_tree.xml file Path: " + filePath);
                _haarCascade = new CascadeClassifier("fonts\\haarcascade_frontalface_alt_tree.xml");

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


                        //601 x 480
                        //Width="415" Height="355"                    
                        currentFrame.Draw(new System.Drawing.Rectangle(181, 63, 279, 354), new Bgr(System.Drawing.Color.Red), 1);

                        imgCapture.ImageSource = BitmapSourceConvert.ToBitmapSource(currentFrame);
                    }
                };

                _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                _timer.Start();

            }
            catch(Exception ex)
            {
                Helper.log.Error("Camera Load Exception: " + ex.Message , ex);
            }

        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_capture != null)
                _capture.Dispose();//worked but crashed due to memory issue
        }
    }
}
