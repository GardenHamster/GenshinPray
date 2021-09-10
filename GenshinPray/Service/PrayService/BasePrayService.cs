using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Models.VO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GenshinPray.Service.PrayService
{
    public abstract class BasePrayService
    {
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
            List<YSPrayRecord> sortList = new List<YSPrayRecord>();
            sortList.AddRange(YSPrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.五星).ToList());
            sortList.AddRange(YSPrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.四星).ToList());
            sortList.AddRange(YSPrayRecords.Where(m => m.GoodsItem.RareType == YSRareType.三星).ToList());
            return sortList.ToArray();
        }


        /// <summary>
        /// 获取90发内,玩家获得5星角色的累计祈愿次数,0代表还未获得S
        /// </summary>
        /// <param name="YSPrayRecords"></param>
        /// <param name="floor90Surplus"></param>
        /// <returns></returns>
        public int GetStar5Cost(YSPrayRecord[] YSPrayRecords, int floor90Surplus)
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
            return 90 - floor90Surplus + star5Index + 1;
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
            prayResult.ApiCallSurplus = authorizePO.DailyCall - prayTimesToday > 0 ? authorizePO.DailyCall - prayTimesToday : 0;
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
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="prayRecords"></param>
        /// <returns></returns>
        public List<GoodsVO> ChangeToGoodsVO(YSPrayRecord[] prayRecords)
        {
            return prayRecords.Select(m => new GoodsVO()
            {
                GoodsName = m.GoodsItem.GoodsName,
                GoodsType = Enum.GetName(typeof(YSGoodsType), m.GoodsItem.GoodsType),
                GoodsSubType = Enum.GetName(typeof(YSGoodsSubType), m.GoodsItem.GoodsSubType),
                RareType = Enum.GetName(typeof(YSRareType), m.GoodsItem.RareType),
            }).ToList();
        }

        /// <summary>
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="goodsItems"></param>
        /// <returns></returns>
        public List<GoodsVO> ChangeToGoodsVO(List<YSGoodsItem> goodsItems)
        {
            return goodsItems.Select(m => new GoodsVO()
            {
                GoodsName = m.GoodsName,
                GoodsType = Enum.GetName(typeof(YSGoodsType), m.GoodsType),
                GoodsSubType = Enum.GetName(typeof(YSGoodsSubType), m.GoodsSubType),
                RareType = Enum.GetName(typeof(YSRareType), m.RareType),
            }).ToList();
        }

    }
}
