using GenshinPray.Dao;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GenshinPray.Service.PrayService
{
    public abstract class BasePrayService : BaseService
    {
        protected MemberDao memberDao;
        protected GoodsDao goodsDao;

        public BasePrayService()
        {
            this.memberDao = new MemberDao();
            this.goodsDao = new GoodsDao();
        }

        /// <summary>
        /// 从物品列表中随机出一个物品
        /// </summary>
        /// <param name="probabilityList"></param>
        /// <returns></returns>
        protected YSProbability GetRandomInList(List<YSProbability> probabilityList)
        {
            List<YSRegion<YSProbability>> regionList = GetRegionList(probabilityList);
            YSRegion<YSProbability> region = GetRandomInRegion(regionList);
            return region.Item;
        }

        /// <summary>
        /// 将概率转化为一个数字区间
        /// </summary>
        /// <param name="probabilityList"></param>
        /// <returns></returns>
        private List<YSRegion<YSProbability>> GetRegionList(List<YSProbability> probabilityList)
        {
            int sumRegion = 0;//总区间
            List<YSRegion<YSProbability>> regionList = new List<YSRegion<YSProbability>>();//区间列表,抽卡时随机获取该区间
            foreach (var item in probabilityList)
            {
                int startRegion = sumRegion;//开始区间
                sumRegion = startRegion + Convert.ToInt32(item.Probability * 10000);//结束区间
                regionList.Add(new YSRegion<YSProbability>(item, startRegion, sumRegion));
            }
            return regionList;
        }

        /// <summary>
        /// 从区间列表中随机出一个区间
        /// </summary>
        /// <param name="regionList"></param>
        /// <returns></returns>
        private YSRegion<YSProbability> GetRandomInRegion(List<YSRegion<YSProbability>> regionList)
        {
            int randomRegion = RandomHelper.getRandomBetween(0, regionList.Last().EndRegion);
            foreach (var item in regionList)
            {
                if (randomRegion >= item.StartRegion && randomRegion < item.EndRegion) return item;
            }
            return regionList.Last();
        }

        /// <summary>
        /// 从物品列表中随机出一个物品
        /// </summary>
        /// <param name="goodsItemList"></param>
        /// <returns></returns>
        protected YSPrayRecord GetRandomInList(List<YSGoodsItem> goodsItemList)
        {
            List<YSRegion<YSGoodsItem>> regionList = GetRegionList(goodsItemList);
            YSRegion<YSGoodsItem> region = GetRandomInRegion(regionList);
            return new YSPrayRecord(region.Item);
        }

        /// <summary>
        /// 将概率转化为一个数字区间
        /// </summary>
        /// <param name="goodsItemList"></param>
        /// <returns></returns>
        private List<YSRegion<YSGoodsItem>> GetRegionList(List<YSGoodsItem> goodsItemList)
        {
            int sumRegion = 0;//总区间
            List<YSRegion<YSGoodsItem>> regionList = new List<YSRegion<YSGoodsItem>>();//区间列表,抽卡时随机获取该区间
            foreach (var item in goodsItemList)
            {
                int startRegion = sumRegion;//开始区间
                sumRegion = startRegion + Convert.ToInt32(item.Probability * 10000);//结束区间
                regionList.Add(new YSRegion<YSGoodsItem>(item, startRegion, sumRegion));
            }
            return regionList;
        }

        /// <summary>
        /// 从区间列表中随机出一个区间
        /// </summary>
        /// <param name="regionList"></param>
        /// <returns></returns>
        private YSRegion<YSGoodsItem> GetRandomInRegion(List<YSRegion<YSGoodsItem>> regionList)
        {
            int randomRegion = RandomHelper.getRandomBetween(0, regionList.Last().EndRegion);
            foreach (var item in regionList)
            {
                if (randomRegion >= item.StartRegion && randomRegion < item.EndRegion) return item;
            }
            return regionList.Last();
        }

        /// <summary>
        /// 判断一个项目是否up项目
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="goodsItem"></param>
        /// <returns></returns>
        protected bool IsUpItem(YSUpItem ySUpItem, YSGoodsItem goodsItem)
        {
            if (ySUpItem.Star5UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) return true;
            if (ySUpItem.Star4UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) return true;
            return false;
        }

        /// <summary>
        /// 显示顺序排序
        /// </summary>
        /// <param name="YSPrayRecords"></param>
        /// <returns></returns>
        public YSPrayRecord[] SortGoods(YSPrayRecord[] YSPrayRecords)
        {
            //先按物品种类排序（0->2），相同种类的物品之间按稀有度倒序排序（5->1）,最后New排在前面
            return YSPrayRecords.OrderBy(c => c.GoodsItem.GoodsType).ThenByDescending(c => c.GoodsItem.RareType).ThenBy(c => c.OwnCountBefore).ToArray();
        }

        /// <summary>
        /// 获取一次五星保底内,成员获得5星角色的累计祈愿次数,0代表还未获得S
        /// </summary>
        /// <param name="YSPrayRecords">祈愿结果</param>
        /// <param name="floorSurplus">剩余N次保底</param>
        /// <param name="maxSurplus">抽出5星最多需要N抽</param>
        /// <returns></returns>
        public int GetStar5Cost(YSPrayRecord[] YSPrayRecords, int floorSurplus, int maxSurplus)
        {
            int star5Index = -1;
            for (int i = 0; i < YSPrayRecords.Length; i++)
            {
                YSGoodsItem YSGoodsItem = YSPrayRecords[i].GoodsItem;
                if (YSGoodsItem.RareType != YSRareType.五星) continue;
                star5Index = i;
                break;
            }
            if (star5Index == -1) return 0;
            return maxSurplus - floorSurplus + star5Index + 1;
        }

        /// <summary>
        /// 创建结果集
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="ySPrayResult"></param>
        /// <param name="authorizePO"></param>
        /// <param name="prayTimesToday"></param>
        /// <param name="toBase64"></param>
        /// <returns></returns>
        public ApiPrayResult CreatePrayResult(YSUpItem ySUpItem, YSPrayResult ySPrayResult, AuthorizePO authorizePO, int prayTimesToday, bool toBase64)
        {
            ApiPrayResult prayResult = new ApiPrayResult();
            prayResult.Star5Cost = ySPrayResult.Star5Cost;
            prayResult.PrayCount = ySPrayResult.PrayRecords.Count();

            prayResult.Role180Surplus = ySPrayResult.MemberInfo.Role180Surplus;
            prayResult.Role90Surplus = ySPrayResult.MemberInfo.Role90Surplus;
            prayResult.Arm80Surplus = ySPrayResult.MemberInfo.Arm80Surplus;
            prayResult.ArmAssignValue = ySPrayResult.MemberInfo.ArmAssignValue;
            prayResult.Perm90Surplus = ySPrayResult.MemberInfo.Perm90Surplus;
            prayResult.Surplus10 = ySPrayResult.Surplus10;

            prayResult.ApiDailyCallSurplus = authorizePO.DailyCall - prayTimesToday > 0 ? authorizePO.DailyCall - prayTimesToday : 0;
            prayResult.ImgBase64 = toBase64 ? ImageHelper.ToBase64(new Bitmap(ySPrayResult.ParyFileInfo.FullName)) : null;
            prayResult.ImgPath = $"{ySPrayResult.ParyFileInfo.Directory.Name}/{ySPrayResult.ParyFileInfo.Name}";
            prayResult.ImgSize = ySPrayResult.ParyFileInfo.Length;
            prayResult.Star5Goods = ChangeToGoodsVO(ySPrayResult.PrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.五星).ToArray());
            prayResult.Star4Goods = ChangeToGoodsVO(ySPrayResult.PrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.四星).ToArray());
            prayResult.Star3Goods = ChangeToGoodsVO(ySPrayResult.PrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.三星).ToArray());
            prayResult.Star5Up = ChangeToGoodsVO(ySUpItem.Star5UpList);
            prayResult.Star4Up = ChangeToGoodsVO(ySUpItem.Star4UpList);
            return prayResult;
        }

        /// <summary>
        /// 绘制祈愿结果图片,返回FileInfo对象
        /// </summary>
        /// <param name="authorize"></param>
        /// <param name="sortPrayRecords"></param>
        /// <param name="memberInfo"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        protected FileInfo DrawPrayImg(AuthorizePO authorize, YSPrayRecord[] sortPrayRecords, MemberPO memberInfo, int imgWidth)
        {
            if (sortPrayRecords.Count() == 1) return DrawHelper.drawOnePrayImg(authorize, sortPrayRecords.First(), memberInfo, imgWidth);
            return DrawHelper.drawTenPrayImg(authorize, sortPrayRecords, memberInfo, imgWidth);
        }

        protected bool CheckIsNew(List<MemberGoodsDTO> memberGoods, YSPrayRecord[] records, YSPrayRecord checkRecord)
        {
            bool isOwnedBefore = memberGoods.Where(m => m.GoodsName == checkRecord.GoodsItem.GoodsName).Any();
            bool isOwnedInRecord = records.Where(m => m != null && m != checkRecord && m.GoodsItem.GoodsName == checkRecord.GoodsItem.GoodsName).Any();
            return isOwnedBefore == false && isOwnedInRecord == false;
        }

        protected int GetOwnCountBefore(List<MemberGoodsDTO> memberGoods, YSPrayRecord[] records, YSPrayRecord checkRecord)
        {
            MemberGoodsDTO ownGood = memberGoods.Where(m => m.GoodsName == checkRecord.GoodsItem.GoodsName).FirstOrDefault();
            int ownBefore = ownGood == null ? 0 : ownGood.Count;
            int ownInRecord = records.Where(m => m != null && m != checkRecord && m.GoodsItem.GoodsName == checkRecord.GoodsItem.GoodsName).Count();
            return ownBefore + ownInRecord;
        }

    }
}
