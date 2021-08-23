using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class ImageHelper
    {

        /// <summary>
        /// 将Image保存为Jpg
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileInfo saveImageToJpg(Image bmp, string savePath, string fileName = "")
        {
            ImageCodecInfo imageCodecInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            if (string.IsNullOrEmpty(fileName)) fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string fullSavePath = savePath + fileName + ".jpg";
            bmp.Save(fullSavePath, imageCodecInfo, myEncoderParameters);
            return new FileInfo(fullSavePath);
        }

        /// <summary>
        /// 将Image保存为Png
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileInfo saveImageToPng(Image bmp, string savePath, string fileName = "")
        {
            ImageCodecInfo imageCodecInfo = GetEncoderInfo("image/png");
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            if (string.IsNullOrEmpty(fileName)) fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string fullSavePath = savePath + fileName + ".png";
            bmp.Save(fullSavePath, imageCodecInfo, myEncoderParameters);
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



    }
}
