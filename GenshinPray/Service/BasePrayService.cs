using GenshinPray.Models;
using GenshinPray.Models.VO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public abstract class BasePrayService
    {

        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSGoodsItem> AllList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(0.6m,YSGoodsType.其他,YSRareType.其他,"5星物品"),
            new YSGoodsItem(5.1m,YSGoodsType.其他,YSRareType.其他,"4星物品"),
            new YSGoodsItem(94.3m,YSGoodsType.其他,YSRareType.其他,"3星物品")
        };

        /// <summary>
        /// 小保底物品概率
        /// </summary>
        protected readonly List<YSGoodsItem> Floor90List = new List<YSGoodsItem>()
        {
            new YSGoodsItem(100,YSGoodsType.其他,YSRareType.其他,"5星物品"),
        };

        /// <summary>
        /// 十连保底物品概率
        /// </summary>
        protected readonly List<YSGoodsItem> Floor10List = new List<YSGoodsItem>()
        {
            new YSGoodsItem(0.6m,YSGoodsType.其他,YSRareType.其他,"5星物品"),
            new YSGoodsItem(99.4m,YSGoodsType.其他,YSRareType.其他,"4星物品")
        };

        /// <summary>
        /// 根据名称随机实际补给项目
        /// </summary>
        /// <param name="prayRecord"></param>
        /// <param name="floor180Surplus"></param>
        /// <param name="floor20Surplus"></param>
        /// <returns></returns>
        protected abstract YSPrayRecord getPrayRecord(YSPrayRecord prayRecord, int floor180Surplus, int floor20Surplus);

        /// <summary>
        /// 判断一个项目是否up项目
        /// </summary>
        /// <param name="goodsItem"></param>
        /// <returns></returns>
        protected abstract bool isUpItem(YSGoodsItem goodsItem);

        /// <summary>
        /// 从物品列表中随机出一个物品
        /// </summary>
        /// <param name="goodsItemList"></param>
        /// <returns></returns>
        protected YSPrayRecord getRandomGoodsInList(List<YSGoodsItem> goodsItemList)
        {
            int randomRegion = 0;
            List<YSGoodsRegion> regionList = getGoodsRegionList(goodsItemList);
            YSGoodsRegion goodsRegion = getRandomInRegion(regionList, ref randomRegion);
            YSGoodsItem goodsItem = goodsRegion.GoodsItem;
            return new YSPrayRecord(goodsItem, randomRegion);
        }

        /// <summary>
        /// 将概率转化为一个数字区间
        /// </summary>
        /// <param name="goodsItemList"></param>
        /// <returns></returns>
        private List<YSGoodsRegion> getGoodsRegionList(List<YSGoodsItem> goodsItemList)
        {
            int sumRegion = 0;//总区间
            List<YSGoodsRegion> goodsRegionList = new List<YSGoodsRegion>();//区间列表,抽卡时随机获取该区间
            foreach (YSGoodsItem goodsItem in goodsItemList)
            {
                int startRegion = sumRegion;//开始区间
                sumRegion = sumRegion + Convert.ToInt32(goodsItem.Probability * 1000);
                goodsRegionList.Add(new YSGoodsRegion(goodsItem, startRegion, sumRegion));
            }
            return goodsRegionList;
        }

        /// <summary>
        /// 从区间列表中随机出一个区间
        /// </summary>
        /// <param name="regionList"></param>
        /// <param name="randomRegion"></param>
        /// <returns></returns>
        private YSGoodsRegion getRandomInRegion(List<YSGoodsRegion> regionList, ref int randomRegion)
        {
            randomRegion = RandomHelper.getRandomBetween(0, regionList.Last().EndRegion);
            if (randomRegion == regionList.Last().EndRegion) return regionList.Last();
            foreach (YSGoodsRegion goodsRegion in regionList)
            {
                if (randomRegion >= goodsRegion.StartRegion && randomRegion < goodsRegion.EndRegion) return goodsRegion;
            }
            return null;
        }

        /// <summary>
        /// 模拟抽卡,获取祈愿记录
        /// </summary>
        /// <param name="prayCount">抽卡次数</param>
        /// <param name="floor180Surplus">距离180大保底剩余多少抽</param>
        /// <param name="floor90Surplus">距离90小保底剩余多少抽</param>
        /// <param name="floor20Surplus">距离4星大保底剩余多少抽</param>
        /// <param name="floor10Surplus">距离4星小保底剩余多少抽</param>
        /// <returns></returns>
        public YSPrayRecord[] getPrayRecord(int prayCount, ref int floor180Surplus, ref int floor90Surplus, ref int floor20Surplus, ref int floor10Surplus)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                floor180Surplus--;
                floor90Surplus--;
                floor20Surplus--;
                floor10Surplus--;

                if (floor10Surplus > 0 && floor90Surplus > 0)//无保底情况
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(AllList), floor180Surplus, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus >= 10)//十连小保底,4星up概率为50%
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor10List), floor180Surplus, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus < 10)//十连大保底,必出4星up物品
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor10List), floor180Surplus, floor20Surplus);
                }
                if (floor90Surplus == 0 && floor180Surplus >= 90)//90小保底,5星up概率为50%
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor90List), floor180Surplus, floor20Surplus);
                }
                if (floor90Surplus == 0 && floor180Surplus < 90)//90大保底,必出5星up物品
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor90List), floor180Surplus, floor20Surplus);
                }

                bool isupItem = isUpItem(records[i].GoodsItem);//判断是否为本期up的物品

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isupItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 10;//十连大保底重置为10
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isupItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isupItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor90Surplus = 90;//九十小保底重置
                    floor180Surplus = 90;//九十大保底重置为90
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isupItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor90Surplus = 90;//九十小保底重置
                    floor180Surplus = 180;//九十大保底重置
                }
            }
            return records;
        }

        /// <summary>
        /// 显示顺序排序
        /// </summary>
        /// <param name="YSPrayRecords"></param>
        /// <returns></returns>
        public YSPrayRecord[] sortGoods(YSPrayRecord[] YSPrayRecords)
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
        public int getStar5Cost(YSPrayRecord[] YSPrayRecords, int floor90Surplus)
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
        /// <param name="prayRecords"></param>
        /// <param name="paryFileInfo"></param>
        /// <param name="Star5Cost"></param>
        /// <returns></returns>
        public PrayResult createPrayResult(YSPrayRecord[] prayRecords, FileInfo paryFileInfo, int Star5Cost)
        {
            PrayResult prayResult = new PrayResult();
            prayResult.PrayCount = prayRecords.Count();
            prayResult.Star5Cost = Star5Cost;
            prayResult.ImgUrl = "";
            prayResult.Goods = changeToGoodsVO(prayRecords);
            prayResult.Star5Goods = changeToGoodsVO(prayRecords.Where(m => m.GoodsItem.RareType == YSRareType.五星).ToArray());
            prayResult.Star5Goods = changeToGoodsVO(prayRecords.Where(m => m.GoodsItem.RareType == YSRareType.四星).ToArray());
            return prayResult;
        }

        /// <summary>
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="prayRecords"></param>
        /// <returns></returns>
        public List<GoodsVO> changeToGoodsVO(YSPrayRecord[] prayRecords)
        {
            return prayRecords.Select(m => new GoodsVO() { GoodsName = m.GoodsItem.GoodsName, RandomRegion = m.RandomRegion }).ToList();
        }



    }
}
