using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DotNETStudy.File.Base64
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        static string ImgToBase64String(string imageFilename)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(imageFilename))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);

                        byte[] bytes = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(bytes, 0, (int)ms.Length);

                        return Convert.ToBase64String(bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when execute [ImgToBase64String] method. exception message: {ex.Message}.");
                return null;
            }
        }

        static Bitmap Base64StringToImage(string base64String, string filename, string filePath)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        bmp.Save($"{filePath}/{filename}", ImageFormat.Jpeg);

                        return bmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when execute [Base64StringToImage] method. exception message: {ex.Message}.");
                return null;
            }
        }
    }
}
