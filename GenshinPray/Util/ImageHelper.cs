using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class ImageHelper
    {

        /// <summary>
        /// 将Image保存为Jpg
        /// </summary>
        /// <param name="image"></param>
        /// <param name="savePath"></param>
        /// <param name="imgWidth"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileInfo saveImageToJpg(Image image, string savePath, int imgWidth = 0, string fileName = "")
        {
            ImageCodecInfo imageCodecInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            if (string.IsNullOrEmpty(fileName)) fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string fullSavePath = Path.Combine(savePath, fileName + ".jpg");
            if (imgWidth == 0)
            {
                image.Save(fullSavePath, imageCodecInfo, myEncoderParameters);
            }
            else
            {
                int imgHeight = (int)(Convert.ToDecimal(image.Width) / imgWidth * image.Height);
                using Image imgResize = new Bitmap(image, imgWidth, imgHeight);
                imgResize.Save(fullSavePath, imageCodecInfo, myEncoderParameters);
            }
            return new FileInfo(fullSavePath);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().Where(o => o.MimeType == mimeType).FirstOrDefault();
        }

        /// <summary>
        /// 保持纵横比压缩图片
        /// </summary>
        /// <param name="originFile"></param>
        /// <param name="savePath"></param>
        /// <param name="size"></param>
        /// <param name="maxCompressTimes"></param>
        /// <returns></returns>
        public static FileInfo compressImage(FileInfo originFile, string savePath, double size, int maxCompressTimes)
        {
            FileInfo compressFileInfo = compressImage(originFile, savePath, size);
            maxCompressTimes--;
            while (compressFileInfo.Length > size && maxCompressTimes > 0)
            {
                compressFileInfo = compressImage(compressFileInfo, savePath, size);
                maxCompressTimes--;
            }
            return compressFileInfo;
        }

        /// <summary>
        /// 保持纵横比压缩图片
        /// </summary>
        /// <param name="originFile"></param>
        /// <param name="savePath"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static FileInfo compressImage(FileInfo originFile, string savePath, double size)
        {
            double originSize = originFile.Length;
            if (originSize < size) return originFile;
            Bitmap originBitmap = new Bitmap(originFile.FullName);
            int originWidth = originBitmap.Size.Width;
            int originHeight = originBitmap.Size.Height;
            double multiple = Math.Floor(size / originSize * 100 - 1) / 100;
            if (multiple < 0) multiple = 0.01;
            double sqrt = Math.Sqrt(multiple);
            int changeWidth = (int)Math.Ceiling(originWidth * sqrt * 0.95);
            int changeHeight = (int)Math.Ceiling(originHeight * sqrt * 0.95);
            Bitmap bitmap = new Bitmap(changeWidth, changeHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(originBitmap, 0, 0, changeWidth, changeHeight);
            graphics.Dispose();
            return saveImageToJpg(bitmap, savePath);
        }

        /// <summary>
        /// 图片转base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ToBase64(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }



    }
}
