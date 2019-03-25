using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ee.iLawyer.Domain
{
    public static class ImageHelper
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static ImageSource ToImageSource(this Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new System.ComponentModel.Win32Exception();
            }
            return wpfBitmap;
        }

        public static ImageSource ToImageSourceFromFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }
            try
            {
                var bitmap = (Bitmap)Bitmap.FromFile(filename);
                var imagesource = ToImageSource(bitmap);
                bitmap.Dispose();
                return imagesource;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static ImageSource ToImageSourceFromBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return null;
            }
            try
            {

                byte[] bytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(bytes))
                {
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                    var imagesource = ToImageSource(bmp);

                    return imagesource;
                }
                //TODO:



            }
            catch (Exception)
            {

                return null;
            }

        }


        public static string ToBase64String(this Image img)
        {
            return Convert.ToBase64String(ToBytes(img));
        }


        public static byte[] ToBytes(this Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var buffer = ms.ToArray();
                return buffer;
            }
        }
    }
}
