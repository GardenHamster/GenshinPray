using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Models.PO;
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
        /// <param name="memberInfo"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        public static FileInfo drawTenPrayImg(YSPrayRecord[] YSPrayRecords, MemberPO memberInfo, int imgWidth)
        {
            int startIndexX = 230 + 151 * 9;
            int startIndexY = 228;
            int indexX = startIndexX;
            int indexY = startIndexY;
            using Bitmap bitmap = new Bitmap(FilePath.getYSPrayBGPath());
            using Graphics bgGraphics = Graphics.FromImage(bitmap);
            for (int i = YSPrayRecords.Length - 1; i >= 0; i--)
            {
                YSPrayRecord prayRecord = YSPrayRecords[i];
                YSGoodsItem goodsItem = prayRecord.GoodsItem;
                drawFrame(bgGraphics, goodsItem, indexX, indexY);//画框
                if (goodsItem.RareType == YSRareType.五星) drawShading(bgGraphics, goodsItem, indexX, indexY);//画底纹
                drawEquip(bgGraphics, goodsItem, indexX, indexY);//画装备或角色
                drawProspect(bgGraphics, goodsItem, indexX, indexY);//画框前景色(带星)
                drawIcon(bgGraphics, prayRecord, indexX, indexY);//画装备图标或转换素材
                drawStar(bgGraphics, prayRecord, indexX, indexY);//画星星
                drawLight(bgGraphics, goodsItem, indexX, indexY);//画光框
                drawNewIcon(bgGraphics, prayRecord, indexX, indexY);//画New图标
                drawCloseIcon(bgGraphics);//画关闭图标
                indexX -= 148;
            }
            drawBubbles(bgGraphics);//画泡泡
            drawWaterMark(bgGraphics);//画水印
            drawUID(bgGraphics, memberInfo.MemberCode);//画UID
            return ImageHelper.saveImageToJpg(bitmap, FilePath.getPrayImgSavePath(), imgWidth);
        }

        /// <summary>
        /// 根据祈愿记录生成结果图
        /// </summary>
        /// <param name="YSPrayRecord"></param>
        /// <param name="memberInfo"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        public static FileInfo drawOnePrayImg(YSPrayRecord YSPrayRecord, MemberPO memberInfo, int imgWidth)
        {
            string backImgUrl = FilePath.getYSPrayBGPath();
            using Bitmap bitmap = new Bitmap(backImgUrl);
            using Graphics bgGraphics = Graphics.FromImage(bitmap);
            YSGoodsItem goodsItem = YSPrayRecord.GoodsItem;
            if (goodsItem.GoodsType == YSGoodsType.角色)
            {
                drawRole(bgGraphics, goodsItem);//画角色大图
                drawRoleIcon(bgGraphics, goodsItem);//画角色元素图标
                drawRoleName(bgGraphics, goodsItem);//画角色名
                drawRoleStar(bgGraphics, goodsItem);//画星星
            }
            if (goodsItem.GoodsType == YSGoodsType.武器)
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
            drawUID(bgGraphics, memberInfo.MemberCode);//画UID
            return ImageHelper.saveImageToJpg(bitmap, FilePath.getPrayImgSavePath(), imgWidth);
        }

        /*-------------------------------------------------------单抽---------------------------------------------------------------------*/
        private static void drawRole(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgRole = new Bitmap(FilePath.getYSBigRoleImgPath(goodsItem));
            using Image imgResize = new Bitmap(imgRole, 2688, 1344);
            bgGraphics.DrawImage(imgResize, -339, -133, new Rectangle(0, 0, imgResize.Width, imgResize.Height), GraphicsUnit.Pixel);
        }

        private static void drawRoleIcon(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgIcon = new Bitmap(FilePath.getYSBigElementIconPath(goodsItem));
            bgGraphics.DrawImage(imgIcon, 73, 547, imgIcon.Width, imgIcon.Height);
        }

        private static void drawRoleName(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using GraphicsPath path = new GraphicsPath();
            Font nameFont = new Font("汉仪文黑-85W", 45, FontStyle.Bold);
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(194, 576, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72; ;
            path.AddString(goodsItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
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
            using Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX, indexY, starWidth, starHeight);
                indexX += starWidth + 5;
            }
        }

        private static void drawEquipBg(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgBg = new Bitmap(FilePath.getYSEquipBgPath(goodsItem));
            bgGraphics.DrawImage(imgBg, 449, -17, 1114, 1114);
        }

        private static void drawEquip(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(goodsItem));
            bgGraphics.DrawImage(imgEquip, 699, -75, 614, 1230);
        }

        private static void drawEquipIcon(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgIcon = new Bitmap(FilePath.getYSBlackEquipIconPath(goodsItem));
            bgGraphics.DrawImage(imgIcon, 47, 512, imgIcon.Width, imgIcon.Height);
        }

        private static void drawEquipName(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using GraphicsPath path = new GraphicsPath();
            Font nameFont = new Font("汉仪文黑-85W", 45, FontStyle.Bold);
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(190, 555, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72;
            path.AddString(goodsItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
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
            using Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX, indexY, starWidth, starHeight);
                indexX += starWidth + 5;
            }
        }

        private static void drawToken(Graphics bgGraphics, YSGoodsItem goodsItem)
        {
            using Image imgToken = new Bitmap(FilePath.getYSTokenPath(goodsItem));
            bgGraphics.DrawImage(imgToken, 1394, 485, imgToken.Width, imgToken.Height);
        }

        private static void drawBubbles(Graphics bgGraphics)
        {
            int randomBigCount = new Random().Next(5, 11);
            List<Image> bigImageList = new List<Image>();
            List<string> bigPathList = FilePath.getBubblesBigPathList();
            foreach (var item in bigPathList) bigImageList.Add(new Bitmap(item));
            for (int i = 0; i < randomBigCount; i++)
            {
                int randomWidth = RandomHelper.getRandomBetween(50, 200);
                int randomXIndex = RandomHelper.getRandomBetween(20, 1900);
                int randomyIndex = RandomHelper.getRandomBetween(20, 1060);
                Image randomImage = bigImageList[RandomHelper.getRandomBetween(0, bigImageList.Count - 1)];
                bgGraphics.DrawImage(randomImage, randomXIndex, randomyIndex, randomWidth, randomWidth);
            }

            int randomSmallCount = new Random().Next(50, 101);
            List<Image> smallImageList = new List<Image>();
            List<string> smallPathList = FilePath.getBubblesSmallPathList();
            foreach (var item in smallPathList) smallImageList.Add(new Bitmap(item));
            for (int i = 0; i < randomSmallCount; i++)
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
            using Image imgFrame = new Bitmap(FilePath.getYSFrameImgPath());
            bgGraphics.DrawImage(imgFrame, indexX, indexY, imgFrame.Width, imgFrame.Height);
        }

        private static void drawProspect(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            using Image imgProspect = new Bitmap(FilePath.getYSProspectImgPath());
            bgGraphics.DrawImage(imgProspect, indexX + 9, indexY + 10, imgProspect.Width, imgProspect.Height);
        }

        private static void drawEquip(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            if (goodsItem.GoodsType == YSGoodsType.角色)
            {
                using Image imgRole = new Bitmap(FilePath.getYSSmallRoleImgPath(goodsItem));
                using Image imgResize = new Bitmap(imgRole, imgRole.Width, imgRole.Height);
                bgGraphics.DrawImage(imgResize, indexX - 1, indexY + 5, new Rectangle(-3, 0, imgResize.Width, imgResize.Height), GraphicsUnit.Pixel);
            }
            if (goodsItem.GoodsType == YSGoodsType.武器)
            {
                using Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(goodsItem));
                using Image imgResize = new Bitmap(imgEquip, 305, 610);
                bgGraphics.DrawImage(imgResize, indexX + 5, indexY, new Rectangle(80, 0, 140, 550), GraphicsUnit.Pixel);
            }
        }

        private static void drawIcon(Graphics bgGraphics, YSPrayRecord prayRecord, int indexX, int indexY)
        {
            YSGoodsItem goodsItem = prayRecord.GoodsItem;
            if (goodsItem.GoodsType == YSGoodsType.武器)
            {
                using Image imgIcon = new Bitmap(FilePath.getYSWhiteEquipIconPath(goodsItem));
                bgGraphics.DrawImage(imgIcon, indexX + 30, indexY + 420, 100, 100);//画武器图标
            }
            if (goodsItem.GoodsType == YSGoodsType.角色 && prayRecord.OwnCountBefore == 0)
            {
                using Image imgIcon = new Bitmap(FilePath.getYSSmallElementIconPath(goodsItem));
                bgGraphics.DrawImage(imgIcon, indexX + 40, indexY + 440, 72, 72);//画元素图标
            }
            if (goodsItem.GoodsType == YSGoodsType.角色 && prayRecord.OwnCountBefore > 0 && prayRecord.OwnCountBefore < 6)
            {
                using Image imgStarLight = new Bitmap(FilePath.getYSStarLightIconPath(goodsItem.RareType == YSRareType.五星 ? 10 : 2));
                bgGraphics.DrawImage(imgStarLight, indexX + 25, indexY + 320, imgStarLight.Width, imgStarLight.Height);//画星辉
                using Image imgStarDust = new Bitmap(FilePath.getYSStarDustIconPath(goodsItem));
                bgGraphics.DrawImage(imgStarDust, indexX + 25, indexY + 440, imgStarDust.Width, imgStarDust.Height);//画星尘
            }
            if (goodsItem.GoodsType == YSGoodsType.角色 && prayRecord.OwnCountBefore >= 6)
            {
                using Image imgStarLight = new Bitmap(FilePath.getYSStarLightIconPath(goodsItem.RareType == YSRareType.五星 ? 25 : 5));
                bgGraphics.DrawImage(imgStarLight, indexX + 25, indexY + 440, imgStarLight.Width, imgStarLight.Height);//画星辉
            }
        }

        private static void drawLight(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            int shiftXIndex = 0;
            int shiftYIndex = -5;
            if (goodsItem.RareType == YSRareType.五星) shiftXIndex = -105;
            if (goodsItem.RareType == YSRareType.四星) shiftXIndex = -98;
            if (goodsItem.RareType == YSRareType.三星) shiftXIndex = 2;
            using Image imgLight = new Bitmap(FilePath.getYSLightPath(goodsItem));
            bgGraphics.DrawImage(imgLight, indexX + shiftXIndex, shiftYIndex, imgLight.Width, imgLight.Height);
        }

        private static void drawStar(Graphics bgGraphics, YSPrayRecord prayRecord, int indexX, int indexY)
        {
            int starCount = 0;
            int starWidth = 21;
            int starHeight = 21;

            if (prayRecord.IsNew == false) return;
            YSGoodsItem goodsItem = prayRecord.GoodsItem;

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
            using Image imgStar = new Bitmap(FilePath.getYSStarPath());
            for (int i = 0; i < starCount; i++)
            {
                bgGraphics.DrawImage(imgStar, indexX + indexXAdd, indexY + 530, starWidth, starHeight);
                indexXAdd += starWidth;
            }
        }

        private static void drawShading(Graphics bgGraphics, YSGoodsItem goodsItem, int indexX, int indexY)
        {
            using Image imgShading = new Bitmap(FilePath.getYSShadingPath());
            bgGraphics.DrawImage(imgShading, indexX, indexY + 45, imgShading.Width, imgShading.Height);
        }

        private static void drawCloseIcon(Graphics bgGraphics)
        {
            using Image imgClose = new Bitmap(FilePath.getYSCloseIconPath());
            bgGraphics.DrawImage(imgClose, 1920 - 105, 20, imgClose.Width, imgClose.Height);
        }

        private static void drawNewIcon(Graphics bgGraphics, YSPrayRecord prayRecord, int indexX, int indexY)
        {
            if (prayRecord.IsNew == false) return;
            using Image imgNew = new Bitmap(FilePath.getYSNewIconPath());
            bgGraphics.DrawImage(imgNew, indexX + 100, indexY + 20, (float)(imgNew.Width * 0.95), (float)(imgNew.Height * 0.95));
        }

        private static void drawWaterMark(Graphics bgGraphics)
        {
            using Font watermarkFont = new Font("汉仪文黑-85W", 10, FontStyle.Regular);
            using SolidBrush brushWatermark = new SolidBrush(Color.FromArgb(150, 178, 193));
            bgGraphics.DrawString("本图片由GardenHamster/GenshinPray模拟生成", watermarkFont, brushWatermark, 10, 1050);
        }

        private static void drawUID(Graphics bgGraphics, string uid)
        {
            using Font watermarkFont = new Font("汉仪文黑-85W", 15, FontStyle.Regular);
            using SolidBrush brushWatermark = new SolidBrush(Color.White);
            bgGraphics.DrawString($"UID：{uid}", watermarkFont, brushWatermark, 1650, 1040);
        }



    }
}
