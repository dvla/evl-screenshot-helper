using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVL.Web.Test.Shared
{
    static class Utilities
    {
        public static ImageFormat DetermineImageFormat(string fileName)
        {
            string fileExtension;
            ImageFormat returnValue = null;

            fileExtension = System.IO.Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fileExtension)) { throw new ArgumentException("Filename did not include an extension"); }


            switch (fileExtension.ToLower())
            {
                case ".png":
                    returnValue = ImageFormat.Png;
                    break;
                case ".jpg":
                    returnValue = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    returnValue = ImageFormat.Jpeg;
                    break;
                case ".tiff":
                    returnValue = ImageFormat.Tiff;
                    break;
                case ".bmp":
                    returnValue = ImageFormat.Bmp;
                    break;
                case ".gif":
                    returnValue = ImageFormat.Gif;
                    break;
                case ".wmf":
                    returnValue = ImageFormat.Wmf;
                    break;
                default:
                    throw new ArgumentException(string.Format("'{0}' is not a supported image format", fileExtension));
            }

            return returnValue;
        }
    }
}
