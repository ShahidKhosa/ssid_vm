using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace SchoolSafeID
{
    class Helper
    {
        //Block Memory Leak
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);
        public static BitmapSource bs;
        public static IntPtr ip;
        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {

            ip = source.GetHbitmap();

            bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,

                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ip);

            return bs;

        }

        public static string SaveImageCapture(BitmapSource bitmap)
        {
            string fileName = "";

            try
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.QualityLevel = 100;                

                // Save Image
                string path = @"C:\SSID";

                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                fileName = "visitor_" + Guid.NewGuid().ToString() + ".jpg";
                string filePath = path + "\\" + fileName;

                FileStream fstream = new FileStream(filePath, FileMode.Create);
                encoder.Save(fstream);
                fstream.Close();

                CropImage(filePath, 75, 50, 275, 350);
            }
            catch( Exception ex)
            {
                Console.Write(ex.Message);
            }
            
            return fileName;
        }

        public static void CropImage(string filePath, int x = 70, int y = 0, int width = 275, int height = 350)
        {
            Image source = Image.FromFile(filePath);            
            Rectangle crop = new Rectangle(x, y, width, height);

            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            bmp.Save("abc.jpg");
        }
    }
}
