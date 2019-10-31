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

namespace SchoolSafeID
{
    class Helper
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string DefaultImage = GetPath() + "\\placeholder.jpg";


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

                CropImage(Visitor.FullImagePath, 175, 60, 275, 350);
            }
            catch( Exception ex)
            {
                Console.Write(ex.Message);
            }                        
        }


        public static void CropImage(string filePath, int x = 175, int y = 60, int width = 275, int height = 350)
        {
            Image source = Image.FromFile(filePath);            
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
    }
}
