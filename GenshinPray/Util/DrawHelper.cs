using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class DrawHelper
    {
        /// <summary>
        /// 根据祈愿记录生成结果图
        /// </summary>
        /// <param name="YSPrayRecords"></param>
        /// <returns></returns>
        public static FileInfo drawTenPrayImg(YSPrayRecord[] YSPrayRecords)
        {
            Bitmap bitmap = null;
            Graphics bgGraphics = null;
            try
            {
                int startIndexX = 230 + 151 * 9;
                int startIndexY = 228;
                int indexX = startIndexX;
                int indexY = startIndexY;
                bitmap = new Bitmap(FilePath.getYSPrayBGPath());
                bgGraphics = Graphics.FromImage(bitmap);
                for (int i = YSPrayRecords.Length - 1; i >= 0; i--)
                {
                    YSPrayRecord goodsRecord = YSPrayRecords[i];
                    YSGoodsItem goodsItem = goodsRecord.GoodsItem;
                    drawFrame(bgGraphics, goodsItem, indexX, indexY);//画框
                    drawEquip(bgGraphics, goodsItem, indexX, indexY);//画装备或角色
                    drawIcon(bgGraphics, goodsItem, indexX, indexY);//画装备图标
                    drawStar(bgGraphics, goodsItem, indexX, indexY);//画星星
                    drawLight(bgGraphics, goodsItem, indexX, indexY);//画光框
                    drawProspect(bgGraphics, goodsItem, indexX, indexY);//画框前景色(带星)
                    drawCloseIcon(bgGraphics);//画关闭图标
                    indexX -= 148;
                }
                drawBubbles(bgGraphics);//画泡泡
                drawWaterMark(bgGraphics);//画水印
                bgGraphics.Dispose();//释放资源
                return ImageHelper.saveImageToJpg(bitmap, FilePath.getPrayImgSavePath());
            }
            finally
            {
                if (bgGraphics != null) bgGraphics.Dispose();
                if (bitmap != null) bitmap.Dispose();
            }
        }

        /// <summary>
        /// 根据祈愿记录生成结果图
        /// </summary>
        /// <param name="YSPrayRecord"></param>
        /// <returns></returns>
        public static FileInfo drawOnePrayImg(YSPrayRecord YSPrayRecord)
        {
            Bitmap bitmap = null;
            Graphics bgGraphics = null;
            try
            {
                string backImgUrl = FilePath.getYSPrayBGPath();
                bitmap = new Bitmap(backImgUrl);
                bgGraphics = Graphics.FromImage(bitmap);
                YSGoodsItem goodsItem = YSPrayRecord.GoodsItem;
                if (goodsItem.GoodsType.isRole())
                {
                    drawRole(bgGraphics, goodsItem);//画角色大图
                    drawRoleIcon(bgGraphics, goodsItem);//画角色元素图标
                    drawRoleName(bgGraphics, goodsItem);//画角色名
                    drawRoleStar(bgGraphics, goodsItem);//画星星
                }
                if (goodsItem.GoodsType.isArm())
                {
                    drawEquipBg(bgGraphics, goodsItem);//画装备背景
                    drawEquip(bgGraphics, goodsItem);//画装备大图
                    drawEquipIcon(bgGraphics, goodsItem);//画装备图标
                    drawEquipName(bgGraphics, goodsItem);//画装备名
                    drawEquipStar(bgGraphics, goodsItem);//画星星
                    drawToken(bgGraphics, goodsItem);//画代币
                }
                drawBubbles(bgGraphics);//画泡泡
                drawWaterMark(bgGraphics);//画水印
                bgGraphics.Dispose();//释放资源
                return ImageHelper.saveImageToJpg(bitmap, FilePath.getPrayImgSavePath());
            }
            finally
            {
                if (bgGraphics != null) bgGraphics.Dispose();
                if (bitmap != null) bitmap.Dispose();
            }
        }

        /*-------------------------------------------------------单抽---------------------------------------------------------------------*/
        private static void drawRole(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgRole = new Bitmap(FilePath.getYSBigRoleImgPath(goodsItem));
            imgRole = new Bitmap(imgRole, 2688, 1344);
            bgGraphics.DrawImage(imgRole, -339, -133, new Rectangle(0, 0, imgRole.Width, imgRole.Height), GraphicsUnit.Pixel);
            imgRole.Dispose();
        }

        private static void drawRoleIcon(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgIcon = new Bitmap(FilePath.getYSBigElementIconPath(goodsItem));
            bgGraphics.DrawImage(imgIcon, 73, 547, imgIcon.Width, imgIcon.Height);
            imgIcon.Dispose();
        }

        private static void drawRoleName(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Font nameFont = new Font("微软雅黑", 48, FontStyle.Bold);
            GraphicsPath path = new GraphicsPath();
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(194, 576, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72; ;
            path.AddString(goodsItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
            path.Dispose();
            nameFont.Dispose();
        }

        private static void drawRoleStar(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            int starCount = 0;
            int starWidth = 34;
            int starHeight = 34;
            int indexX = 200;
            int indexY = 663;
            if (goodsItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (goodsItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (goodsItem.RareType == YSRareType.三星)
            {
                starCount = 3;
            }
            Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX, indexY, starWidth, starHeight);
                indexX += starWidth + 5;
            }
            imgStar.Dispose();
        }

        private static void drawEquipBg(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgBg = new Bitmap(FilePath.getYSEquipBgPath(goodsItem));
            bgGraphics.DrawImage(imgBg, 449, -17, 1114, 1114);
            imgBg.Dispose();
        }

        private static void drawEquip(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(goodsItem));
            bgGraphics.DrawImage(imgEquip, 699, -75, 614, 1230);
            imgEquip.Dispose();
        }

        private static void drawEquipIcon(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgIcon = new Bitmap(FilePath.getYSBlackEquipIconPath(goodsItem));
            bgGraphics.DrawImage(imgIcon, 47, 512, imgIcon.Width, imgIcon.Height);
            imgIcon.Dispose();
        }

        private static void drawEquipName(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Font nameFont = new Font("微软雅黑", 48, FontStyle.Bold);
            GraphicsPath path = new GraphicsPath();
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(190, 556, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72; ;
            path.AddString(goodsItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
            path.Dispose();
            nameFont.Dispose();
        }

        private static void drawEquipStar(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            int starCount = 0;
            int starWidth = 34;
            int starHeight = 34;
            int indexX = 190;
            int indexY = 643;
            if (goodsItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (goodsItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (goodsItem.RareType == YSRareType.三星)
            {
                starCount = 3;
            }
            Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX, indexY, starWidth, starHeight);
                indexX += starWidth + 5;
            }
            imgStar.Dispose();
        }

        private static void drawToken(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            Image imgToken = new Bitmap(FilePath.getYSTokenPath(goodsItem));
            bgGraphics.DrawImage(imgToken, 1394, 485, imgToken.Width, imgToken.Height);
            imgToken.Dispose();
        }

        private static void drawBubbles(Graphics bgGraphics)
        {
            List<Image> bigImageList = new List<Image>();
            List<string> bigPathList = FilePath.getBubblesBigPathList();
            foreach (var item in bigPathList) bigImageList.Add(new Bitmap(item));
            for (int i = 0; i < 10; i++)
            {
                int randomWidth = RandomHelper.getRandomBetween(50, 200);
                int randomXIndex = RandomHelper.getRandomBetween(20, 1900);
                int randomyIndex = RandomHelper.getRandomBetween(20, 1060);
                Image randomImage = bigImageList[RandomHelper.getRandomBetween(0, bigImageList.Count - 1)];
                bgGraphics.DrawImage(randomImage, randomXIndex, randomyIndex, randomWidth, randomWidth);
            }

            List<Image> smallImageList = new List<Image>();
            List<string> smallPathList = FilePath.getBubblesSmallPathList();
            foreach (var item in smallPathList) smallImageList.Add(new Bitmap(item));
            for (int i = 0; i < 100; i++)
            {
                int randomWidth = RandomHelper.getRandomBetween(5, 15);
                int randomXIndex = RandomHelper.getRandomBetween(20, 1900);
                int randomyIndex = RandomHelper.getRandomBetween(20, 1060);
                Image randomImage = smallImageList[RandomHelper.getRandomBetween(0, smallImageList.Count - 1)];
                bgGraphics.DrawImage(randomImage, randomXIndex, randomyIndex, randomWidth, randomWidth);
            }

            foreach (var item in bigImageList) item.Dispose();
            foreach (var item in smallImageList) item.Dispose();
        }

        /*-------------------------------------------------------十连---------------------------------------------------------------------*/

        private static void drawFrame(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            Image imgFrame = new Bitmap(FilePath.getYSFrameImgPath());
            bgGraphics.DrawImage(imgFrame, indexX, indexY, imgFrame.Width, imgFrame.Height);
            imgFrame.Dispose();
        }

        private static void drawProspect(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            Image imgProspect = new Bitmap(FilePath.getYSProspectImgPath());
            bgGraphics.DrawImage(imgProspect, indexX + 9, indexY + 7, imgProspect.Width, imgProspect.Height);
            imgProspect.Dispose();
        }

        private static void drawEquip(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            if (goodsItem.GoodsType.isRole())
            {
                Image imgRole = new Bitmap(FilePath.getYSSmallRoleImgPath(goodsItem));
                imgRole = new Bitmap(imgRole, imgRole.Width, imgRole.Height);
                bgGraphics.DrawImage(imgRole, indexX + 4, indexY + 2, new Rectangle(0, 0, imgRole.Width - 5, imgRole.Height), GraphicsUnit.Pixel);
                //Image imgRole = new Bitmap(FilePath.getYSRoleImgPath(goodsItem));
                //bgGraphics.DrawImage(imgRole, indexX+4, indexY, imgRole.Width, imgRole.Height);
            }
            else
            {
                Image imgEquip = new Bitmap(new Bitmap(FilePath.getYSEquipImgPath(goodsItem)), 305, 610);
                bgGraphics.DrawImage(imgEquip, indexX + 5, indexY, new Rectangle(80, 0, 140, 550), GraphicsUnit.Pixel);
                //Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(goodsItem));
                //bgGraphics.DrawImage(imgEquip, indexX - 80, indexY, 305, 610);
            }
        }

        private static void drawIcon(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            if (goodsItem.GoodsType.isRole())
            {
                Image imgIcon = new Bitmap(FilePath.getYSSmallElementIconPath(goodsItem));
                bgGraphics.DrawImage(imgIcon, indexX + 40, indexY + 440, 72, 72);
                imgIcon.Dispose();
            }
            else
            {
                Image imgIcon = new Bitmap(FilePath.getYSWhiteEquipIconPath(goodsItem));
                bgGraphics.DrawImage(imgIcon, indexX + 30, indexY + 420, 100, 100);
                imgIcon.Dispose();
            }
        }

        private static void drawLight(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            int shiftXIndex = 0;
            int shiftYIndex = -5;
            if (goodsItem.RareType == YSRareType.五星) shiftXIndex = -63;
            if (goodsItem.RareType == YSRareType.四星) shiftXIndex = -61;
            if (goodsItem.RareType == YSRareType.三星) shiftXIndex = 2;
            Image imgLight = new Bitmap(FilePath.getYSLightPath(goodsItem));
            bgGraphics.DrawImage(imgLight, indexX + shiftXIndex, shiftYIndex, imgLight.Width, imgLight.Height);
            imgLight.Dispose();
        }

        private static void drawStar(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            int starCount = 0;
            int starWidth = 21;
            int starHeight = 21;
            if (goodsItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (goodsItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (goodsItem.RareType == YSRareType.三星)
            {
                starCount = 3;
            }

            int indexXAdd = (150 - (starCount * starWidth)) / 2;
            Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX + indexXAdd, indexY + 530, starWidth, starHeight);
                indexXAdd += starWidth;
            }
            imgStar.Dispose();
        }

        private static void drawCloseIcon(Graphics bgGraphics)
        {
            Image imgClose = new Bitmap(FilePath.getYSCloseIconPath());
            bgGraphics.DrawImage(imgClose, 1920 - 105, 20, imgClose.Width, imgClose.Height);
            imgClose.Dispose();
        }

        private static void drawWaterMark(Graphics bgGraphics)
        {
            Font watermarkFont = new Font("微软雅黑", 15, FontStyle.Regular);
            SolidBrush brushWatermark = new SolidBrush(Color.FromArgb(150, 178, 193));
            bgGraphics.DrawString("本图片由theresa3rd-bot模拟生成", watermarkFont, brushWatermark, 1580, 1030);
            brushWatermark.Dispose();
            watermarkFont.Dispose();
        }





    }
}
