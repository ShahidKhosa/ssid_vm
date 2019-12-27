using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using Newtonsoft.Json;
using Emgu.CV;
using Emgu.CV.Structure;
using log4net;
using System.Threading.Tasks;
using System.Windows;
using System.Deployment;
using System.Deployment.Application;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolSafeID
{
    class Helper
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string DefaultImage = GetPath() + "\\placeholder.jpg";


        public static Visibility LogoVisibility
        {
            get
            {
                string visibility = "visible";

                if (APIManager.KioskSettings.ContainsKey("ssid_logo_settings"))
                {
                    visibility = APIManager.KioskSettings["ssid_logo_settings"].ToString();
                }

                return (visibility.ToLower().Equals("visible") ? Visibility.Visible : Visibility.Hidden);
            }
        }


        public static string GetPath(string innerPath = "")
        {
            string path = Directory.GetCurrentDirectory() + "\\SSID" + innerPath;

            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            return path;
        }


        public static void SaveImageCapture(Image<Bgr, Byte> currentFrame)
        {            
            try
            {
                Visitor.FullImagePath = Guid.NewGuid() + ".jpg";
                currentFrame.Save(Visitor.FullImagePath);


                //601 x 480
                //Width="415" Height="355"
                //currentFrame.Draw(new System.Drawing.Rectangle(69, 0, 279, 354), new Bgr(System.Drawing.Color.Red), 1);
                //currentFrame.Draw(new System.Drawing.Rectangle(162, 62, 279, 354), new Bgr(System.Drawing.Color.Red), 1);

                CropImage(Visitor.FullImagePath, 71, 2, 275, 350);
            }
            catch( Exception ex)
            {
                Console.Write(ex.Message);
            }                        
        }


        public static void CropImage(string filePath, int x = 175, int y = 60, int width = 275, int height = 350)
        {
            System.Drawing.Image source = System.Drawing.Image.FromFile(filePath);            
            Rectangle crop = new Rectangle(x, y, width, height);

            using (var bmp = new Bitmap(crop.Width, crop.Height))
            { 
                var gr = Graphics.FromImage(bmp);            
                gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);

                Visitor.CroppedImagePath = Guid.NewGuid() + ".jpg";

                bmp.Save(Visitor.CroppedImagePath);

                Visitor.VisitorHasNewImage = true;
            }            
        }

        public async static void ShowAppDownloadMessage()
        {
            await Task.Delay(1500);

            string message = "A new version of this app is available. Click OK to download now?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                // OK code here                  
            }
            else
            {
                // Cancel code here                  
            }
        }


        public static void CleanExtraResources()
        {
            try
            {
                string dirPath = GetPath() + "\\" + APIManager.KioskSettings["job_id"] + "\\";

                string[] filePaths = Directory.GetFiles(dirPath, "*.jpg");
                foreach (string filePath in filePaths)
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        //Helper.log.Error("Delete Image Error " + ex.Message, ex);
                    }
                }

                string[] files = Directory.GetFiles(GetPath(), "*.pdf");
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        //Helper.log.Error("Delete PDF File Error " + ex.Message, ex);
                    }
                }
            }
            catch(Exception ex)
            {
                Helper.log.Error("Clean Extra resouces Error " + ex.Message, ex);
            }
        }


        public static void CheckForUpdate()
        {
            ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
            //
            if (info.UpdateAvailable)
            {
                updateCheck.Update();
                MessageBox.Show("The application has been upgraded, and will now restart.");                
                //System.Windows.Forms.Application.Restart();
            }
        }


        public static void UpdateLogoVisibility(StackPanel footerBar)
        {
            if (APIManager.KioskSettings.ContainsKey("ssid_logo_settings"))
            {
                if (APIManager.KioskSettings["ssid_logo_settings"].ToString().ToLower().Equals("hidden"))
                {
                    footerBar.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                }
                else
                {
                    AddBackgroundImage(footerBar);
                }
            }
            else
            {
                AddBackgroundImage(footerBar);
            }
        }


        private static void AddBackgroundImage(StackPanel footerBar)
        {
            ImageBrush brush = new ImageBrush
            {
                Opacity = 1,
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Right,
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/poweredby_schoolsafe.png"))
            };

            footerBar.Background = brush;
        }
    }
}
