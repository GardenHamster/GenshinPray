using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Type;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    public abstract class BasePrayController : ControllerBase
    {

        protected readonly List<YSSupplyItem> RoleStar5List = new List<YSSupplyItem>()
        {
            new YSSupplyItem(1,YSGoodsType.雷,YSRareType.五星,"刻晴"),
            new YSSupplyItem(1,YSGoodsType.水,YSRareType.五星,"莫娜"),
            new YSSupplyItem(1,YSGoodsType.冰,YSRareType.五星,"七七"),
            new YSSupplyItem(1,YSGoodsType.火,YSRareType.五星,"迪卢克"),
            new YSSupplyItem(1,YSGoodsType.风,YSRareType.五星,"琴")
        };

        protected readonly List<YSSupplyItem> RoleStar4List = new List<YSSupplyItem>()
        {
            new YSSupplyItem(1,YSGoodsType.风,YSRareType.四星,"砂糖"),
            new YSSupplyItem(1,YSGoodsType.雷,YSRareType.四星,"菲谢尔"),
            new YSSupplyItem(1,YSGoodsType.水,YSRareType.四星,"芭芭拉"),
            new YSSupplyItem(1,YSGoodsType.火,YSRareType.四星,"烟绯"),
            new YSSupplyItem(1,YSGoodsType.冰,YSRareType.四星,"罗莎莉亚"),
            new YSSupplyItem(1,YSGoodsType.火,YSRareType.四星,"辛焱"),
            new YSSupplyItem(1,YSGoodsType.冰,YSRareType.四星,"迪奥娜"),
            new YSSupplyItem(1,YSGoodsType.冰,YSRareType.四星,"重云"),
            new YSSupplyItem(1,YSGoodsType.岩,YSRareType.四星,"诺艾尔"),
            new YSSupplyItem(1,YSGoodsType.火,YSRareType.四星,"班尼特"),
            new YSSupplyItem(1,YSGoodsType.岩,YSRareType.四星,"凝光"),
            new YSSupplyItem(1,YSGoodsType.水,YSRareType.四星,"行秋"),
            new YSSupplyItem(1,YSGoodsType.雷,YSRareType.四星,"北斗"),
            new YSSupplyItem(1,YSGoodsType.火,YSRareType.四星,"香菱"),
            new YSSupplyItem(1,YSGoodsType.雷,YSRareType.四星,"雷泽")
        };

        protected readonly List<YSSupplyItem> ArmStar4List = new List<YSSupplyItem>()
        {
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.四星,"弓藏"),
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.四星,"祭礼弓"),
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.四星,"绝弦"),
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.四星,"西风猎弓"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.四星,"昭心"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.四星,"祭礼残章"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.四星,"流浪乐章"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.四星,"西风秘典"),
            new YSSupplyItem(1,YSGoodsType.长柄武器,YSRareType.四星,"西风长枪"),
            new YSSupplyItem(1,YSGoodsType.长柄武器,YSRareType.四星,"匣里灭辰"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.四星,"雨裁"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.四星,"祭礼大剑"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.四星,"钟剑"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.四星,"西风大剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.四星,"匣里龙吟"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.四星,"祭礼剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.四星,"笛剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.四星,"西风剑")
        };

        protected readonly List<YSSupplyItem> ArmStar3List = new List<YSSupplyItem>()
        {
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.三星,"弹弓"),
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.三星,"神射手之誓"),
            new YSSupplyItem(1,YSGoodsType.弓,YSRareType.三星,"鸦羽弓"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.三星,"翡玉法球"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.三星,"讨龙英杰谭"),
            new YSSupplyItem(1,YSGoodsType.法器,YSRareType.三星,"魔导绪论"),
            new YSSupplyItem(1,YSGoodsType.长柄武器,YSRareType.三星,"黑缨枪"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.三星,"以理服人"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.三星,"沐浴龙血的剑"),
            new YSSupplyItem(1,YSGoodsType.双手剑,YSRareType.三星,"铁影阔剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.三星,"飞天御剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.三星,"黎明神剑"),
            new YSSupplyItem(1,YSGoodsType.单手剑,YSRareType.三星,"冷刃")
        };

        /// <summary>
        /// 将概率转化为一个数字区间
        /// </summary>
        /// <param name="supplyItemList"></param>
        /// <returns></returns>
        protected virtual List<YSSupplyRegion> GetSupplyRegionList(List<YSSupplyItem> supplyItemList)
        {
            int sumRegion = 0;//总区间
            List<YSSupplyRegion> supplyRegionList = new List<YSSupplyRegion>();//区间列表,抽卡时随机获取该区间
            foreach (YSSupplyItem supplyItem in supplyItemList)
            {
                int startRegion = sumRegion;//开始区间
                sumRegion = sumRegion + Convert.ToInt32(supplyItem.Probability * 1000);
                supplyRegionList.Add(new YSSupplyRegion(supplyItem, startRegion, sumRegion));
            }
            return supplyRegionList;
        }

        /// <summary>
        /// 从物品列表中随机出一个物品
        /// </summary>
        /// <param name="supplyItemList"></param>
        /// <returns></returns>
        protected virtual YSSupplyRecord getRandomSupplyInList(List<YSSupplyItem> supplyItemList)
        {
            List<YSSupplyRegion> regionList = GetSupplyRegionList(supplyItemList);
            YSSupplyRegion supplyRegion = getRandomInRegion(regionList);
            YSSupplyItem supplyItem = supplyRegion.SupplyItem;
            return new YSSupplyRecord(supplyItem);
        }

        /// <summary>
        /// 从区间列表中随机出一个区间
        /// </summary>
        /// <param name="regionList"></param>
        /// <returns></returns>
        protected virtual YSSupplyRegion getRandomInRegion(List<YSSupplyRegion> regionList)
        {
            int randomRegion = RandomHelper.getRandomBetween(0, regionList.Last().EndRegion);
            if (randomRegion == regionList.Last().EndRegion) return regionList.Last();
            foreach (YSSupplyRegion supplyRegion in regionList)
            {
                if (randomRegion >= supplyRegion.StartRegion && randomRegion < supplyRegion.EndRegion) return supplyRegion;
            }
            return null;
        }

        /// <summary>
        /// 根据祈愿记录生成结果图
        /// </summary>
        /// <param name="YSSupplyRecords"></param>
        /// <returns></returns>
        public virtual FileInfo drawTenPrayImg(YSSupplyRecord[] YSSupplyRecords)
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
                for (int i = YSSupplyRecords.Length - 1; i >= 0; i--)
                {
                    YSSupplyRecord supplyRecord = YSSupplyRecords[i];
                    YSSupplyItem supplyItem = supplyRecord.SupplyItem;
                    drawFrame(bgGraphics, supplyItem, indexX, indexY);//画框
                    drawEquip(bgGraphics, supplyItem, indexX, indexY);//画装备或角色
                    drawIcon(bgGraphics, supplyItem, indexX, indexY);//画装备图标
                    drawStar(bgGraphics, supplyItem, indexX, indexY);//画星星
                    drawLight(bgGraphics, supplyItem, indexX, indexY);//画光框
                    drawProspect(bgGraphics, supplyItem, indexX, indexY);//画框前景色(带星)
                    drawCloseIcon(bgGraphics);//画关闭图标
                    indexX -= 148;
                }
                drawBubbles(bgGraphics);//画泡泡
                drawWaterMark(bgGraphics);//画水印
                bgGraphics.Dispose();//释放资源
                return ImageHelper.saveImageToJpg(bitmap, FilePath.getSupplyImgSavePath());
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
        /// <param name="YSSupplyRecord"></param>
        /// <returns></returns>
        public virtual FileInfo drawOnePrayImg(YSSupplyRecord YSSupplyRecord)
        {
            Bitmap bitmap = null;
            Graphics bgGraphics = null;
            try
            {
                string backImgUrl = FilePath.getYSPrayBGPath();
                bitmap = new Bitmap(backImgUrl);
                bgGraphics = Graphics.FromImage(bitmap);
                YSSupplyItem supplyItem = YSSupplyRecord.SupplyItem;
                if (supplyItem.GoodsType.isRole())
                {
                    drawRole(bgGraphics, supplyItem);//画角色大图
                    drawRoleIcon(bgGraphics, supplyItem);//画角色元素图标
                    drawRoleName(bgGraphics, supplyItem);//画角色名
                    drawRoleStar(bgGraphics, supplyItem);//画星星
                }
                if (supplyItem.GoodsType.isArm())
                {
                    drawEquipBg(bgGraphics, supplyItem);//画装备背景
                    drawEquip(bgGraphics, supplyItem);//画装备大图
                    drawEquipIcon(bgGraphics, supplyItem);//画装备图标
                    drawEquipName(bgGraphics, supplyItem);//画装备名
                    drawEquipStar(bgGraphics, supplyItem);//画星星
                    drawToken(bgGraphics, supplyItem);//画代币
                }
                drawBubbles(bgGraphics);//画泡泡
                drawWaterMark(bgGraphics);//画水印
                bgGraphics.Dispose();//释放资源
                return ImageHelper.saveImageToJpg(bitmap, FilePath.getSupplyImgSavePath());
            }
            finally
            {
                if (bgGraphics != null) bgGraphics.Dispose();
                if (bitmap != null) bitmap.Dispose();
            }
        }

        /*-------------------------------------------------------单抽---------------------------------------------------------------------*/
        protected void drawRole(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgRole = new Bitmap(FilePath.getYSBigRoleImgPath(supplyItem));
            imgRole = new Bitmap(imgRole, 2688, 1344);
            bgGraphics.DrawImage(imgRole, -339, -133, new Rectangle(0, 0, imgRole.Width, imgRole.Height), GraphicsUnit.Pixel);
            imgRole.Dispose();
        }

        protected void drawRoleIcon(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgIcon = new Bitmap(FilePath.getYSBigElementIconPath(supplyItem));
            bgGraphics.DrawImage(imgIcon, 73, 547, imgIcon.Width, imgIcon.Height);
            imgIcon.Dispose();
        }

        protected void drawRoleName(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Font nameFont = new Font("微软雅黑", 48, FontStyle.Bold);
            GraphicsPath path = new GraphicsPath();
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(194, 576, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72; ;
            path.AddString(supplyItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
            path.Dispose();
            nameFont.Dispose();
        }

        protected void drawRoleStar(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            int starCount = 0;
            int starWidth = 34;
            int starHeight = 34;
            int indexX = 200;
            int indexY = 663;
            if (supplyItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (supplyItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (supplyItem.RareType == YSRareType.三星)
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

        protected void drawEquipBg(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgBg = new Bitmap(FilePath.getYSEquipBgPath(supplyItem));
            bgGraphics.DrawImage(imgBg, 449, -17, 1114, 1114);
            imgBg.Dispose();
        }

        protected void drawEquip(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(supplyItem));
            bgGraphics.DrawImage(imgEquip, 699, -75, 614, 1230);
            imgEquip.Dispose();
        }

        protected void drawEquipIcon(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgIcon = new Bitmap(FilePath.getYSBlackEquipIconPath(supplyItem));
            bgGraphics.DrawImage(imgIcon, 47, 512, imgIcon.Width, imgIcon.Height);
            imgIcon.Dispose();
        }

        protected void drawEquipName(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Font nameFont = new Font("微软雅黑", 48, FontStyle.Bold);
            GraphicsPath path = new GraphicsPath();
            StringFormat format = StringFormat.GenericTypographic;
            RectangleF rect = new RectangleF(190, 556, 600, 200);
            float size = bgGraphics.DpiY * nameFont.SizeInPoints / 72; ;
            path.AddString(supplyItem.GoodsName, nameFont.FontFamily, (int)nameFont.Style, size, rect, format);
            bgGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bgGraphics.DrawPath(new Pen(Color.Black, 2), path);
            bgGraphics.FillPath(Brushes.White, path);
            path.Dispose();
            nameFont.Dispose();
        }

        protected void drawEquipStar(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            int starCount = 0;
            int starWidth = 34;
            int starHeight = 34;
            int indexX = 190;
            int indexY = 643;
            if (supplyItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (supplyItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (supplyItem.RareType == YSRareType.三星)
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

        protected void drawToken(Graphics bgGraphics, YSSupplyItem supplyItem)
        {
            Image imgToken = new Bitmap(FilePath.getYSTokenPath(supplyItem));
            bgGraphics.DrawImage(imgToken, 1394, 485, imgToken.Width, imgToken.Height);
            imgToken.Dispose();
        }

        protected void drawBubbles(Graphics bgGraphics)
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

        protected void drawFrame(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            Image imgFrame = new Bitmap(FilePath.getYSFrameImgPath());
            bgGraphics.DrawImage(imgFrame, indexX, indexY, imgFrame.Width, imgFrame.Height);
            imgFrame.Dispose();
        }

        protected void drawProspect(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            Image imgProspect = new Bitmap(FilePath.getYSProspectImgPath());
            bgGraphics.DrawImage(imgProspect, indexX + 9, indexY + 7, imgProspect.Width, imgProspect.Height);
            imgProspect.Dispose();
        }

        protected void drawEquip(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            if (supplyItem.GoodsType.isRole())
            {
                Image imgRole = new Bitmap(FilePath.getYSSmallRoleImgPath(supplyItem));
                imgRole = new Bitmap(imgRole, imgRole.Width, imgRole.Height);
                bgGraphics.DrawImage(imgRole, indexX + 4, indexY + 2, new Rectangle(0, 0, imgRole.Width - 5, imgRole.Height), GraphicsUnit.Pixel);
                //Image imgRole = new Bitmap(FilePath.getYSRoleImgPath(supplyItem));
                //bgGraphics.DrawImage(imgRole, indexX+4, indexY, imgRole.Width, imgRole.Height);
            }
            else
            {
                Image imgEquip = new Bitmap(new Bitmap(FilePath.getYSEquipImgPath(supplyItem)), 305, 610);
                bgGraphics.DrawImage(imgEquip, indexX + 5, indexY, new Rectangle(80, 0, 140, 550), GraphicsUnit.Pixel);
                //Image imgEquip = new Bitmap(FilePath.getYSEquipImgPath(supplyItem));
                //bgGraphics.DrawImage(imgEquip, indexX - 80, indexY, 305, 610);
            }
        }

        protected void drawIcon(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            if (supplyItem.GoodsType.isRole())
            {
                Image imgIcon = new Bitmap(FilePath.getYSSmallElementIconPath(supplyItem));
                bgGraphics.DrawImage(imgIcon, indexX + 40, indexY + 440, 72, 72);
                imgIcon.Dispose();
            }
            else
            {
                Image imgIcon = new Bitmap(FilePath.getYSWhiteEquipIconPath(supplyItem));
                bgGraphics.DrawImage(imgIcon, indexX + 30, indexY + 420, 100, 100);
                imgIcon.Dispose();
            }
        }

        protected void drawLight(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            int shiftXIndex = 0;
            int shiftYIndex = -5;
            if (supplyItem.RareType == YSRareType.五星) shiftXIndex = -63;
            if (supplyItem.RareType == YSRareType.四星) shiftXIndex = -61;
            if (supplyItem.RareType == YSRareType.三星) shiftXIndex = 2;
            Image imgLight = new Bitmap(FilePath.getYSLightPath(supplyItem));
            bgGraphics.DrawImage(imgLight, indexX + shiftXIndex, shiftYIndex, imgLight.Width, imgLight.Height);
            imgLight.Dispose();
        }

        protected void drawStar(Graphics bgGraphics, YSSupplyItem supplyItem, int indexX, int indexY)
        {
            int starCount = 0;
            int starWidth = 21;
            int starHeight = 21;
            if (supplyItem.RareType == YSRareType.五星)
            {
                starCount = 5;
            }
            else if (supplyItem.RareType == YSRareType.四星)
            {
                starCount = 4;
            }
            else if (supplyItem.RareType == YSRareType.三星)
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

        protected void drawCloseIcon(Graphics bgGraphics)
        {
            Image imgClose = new Bitmap(FilePath.getYSCloseIconPath());
            bgGraphics.DrawImage(imgClose, 1920 - 105, 20, imgClose.Width, imgClose.Height);
            imgClose.Dispose();
        }

        protected void drawWaterMark(Graphics bgGraphics)
        {
            Font watermarkFont = new Font("微软雅黑", 15, FontStyle.Regular);
            SolidBrush brushWatermark = new SolidBrush(Color.FromArgb(150, 178, 193));
            bgGraphics.DrawString("本图片由theresa3rd-bot模拟生成", watermarkFont, brushWatermark, 1580, 1030);
            brushWatermark.Dispose();
            watermarkFont.Dispose();
        }

        /// <summary>
        /// 显示顺序排序
        /// </summary>
        /// <param name="YSSupplyRecords"></param>
        /// <returns></returns>
        protected YSSupplyRecord[] sortSupply(YSSupplyRecord[] YSSupplyRecords)
        {
            List<YSSupplyRecord> sortList = new List<YSSupplyRecord>();
            sortList.AddRange(YSSupplyRecords.Where(m => m.SupplyItem.RareType == YSRareType.五星).ToList());
            sortList.AddRange(YSSupplyRecords.Where(m => m.SupplyItem.RareType == YSRareType.四星).ToList());
            sortList.AddRange(YSSupplyRecords.Where(m => m.SupplyItem.RareType == YSRareType.三星).ToList());
            return sortList.ToArray();
        }

    }
}
