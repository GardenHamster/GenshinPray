using GenshinPray.Common;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service.PrayService
{
    public class RolePrayService : BasePrayService
    {

        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSProbability> AllList = new List<YSProbability>()
        {
            new YSProbability(0.6m, YSProbabilityType.五星物品),
            new YSProbability(5.1m, YSProbabilityType.四星物品),
            new YSProbability(94.3m,YSProbabilityType.三星物品)
        };

        /// <summary>
        /// 小保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor90List = new List<YSProbability>()
        {
            new YSProbability(100, YSProbabilityType.五星物品),
        };

        /// <summary>
        /// 十连保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor10List = new List<YSProbability>()
        {
            new YSProbability(0.6m, YSProbabilityType.五星物品),
            new YSProbability(99.4m,YSProbabilityType.四星物品)
        };


        /// <summary>
        /// 模拟抽卡,获取祈愿记录
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <param name="floor180Surplus">距离180大保底剩余多少抽</param>
        /// <param name="floor90Surplus">距离90小保底剩余多少抽</param>
        /// <param name="floor20Surplus">距离4星大保底剩余多少抽</param>
        /// <param name="floor10Surplus">距离4星小保底剩余多少抽</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(YSUpItem ySUpItem, int prayCount, ref int floor180Surplus, ref int floor90Surplus, ref int floor20Surplus, ref int floor10Surplus)
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
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem, floor180Surplus, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus >= 10)//十连小保底,4星up概率为50%
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, floor180Surplus, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus < 10)//十连大保底,必出4星up物品
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, floor180Surplus, floor20Surplus);
                }
                if (floor90Surplus == 0 && floor180Surplus >= 90)//90小保底,5星up概率为50%
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem, floor180Surplus, floor20Surplus);
                }
                if (floor90Surplus == 0 && floor180Surplus < 90)//90大保底,必出5星up物品
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem, floor180Surplus, floor20Surplus);
                }

                bool isUpItem = IsUpItem(ySUpItem, records[i].GoodsItem);//判断是否为本期up的物品

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 10;//十连大保底重置为10
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isUpItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor90Surplus = 90;//九十发小保底重置
                    floor180Surplus = 90;//九十发大保底重置为90
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isUpItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor90Surplus = 90;//九十发小保底重置
                    floor180Surplus = 180;//九十发大保底重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品)
            {
                bool isGetUp = floor180Surplus < 90 ? true : RandomHelper.getRandomBetween(1, 100) <= 50;
                return isGetUp ? GetRandomInList(ySUpItem.Star5UpList) : GetRandomInList(ySUpItem.Star5NonUpList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品)
            {
                bool isGetUp = floor20Surplus < 10 ? true : RandomHelper.getRandomBetween(1, 100) <= 50;
                return isGetUp ? GetRandomInList(ySUpItem.Star4UpList) : GetRandomInList(ySUpItem.Star4NonUpList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.三星物品)
            {
                return GetRandomInList(ySUpItem.Star3AllList);
            }
            throw new GoodsNotFoundException($"未能随机获取与{Enum.GetName(typeof(YSProbability), ysProbability.ProbabilityType)}对应物品");
        }

        public YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int role180Surplus = memberInfo.Role180Surplus;
            int role90Surplus = memberInfo.Role90Surplus;
            int role90SurplusBefore = memberInfo.Role90Surplus;
            int role20Surplus = memberInfo.Role20Surplus;
            int role10Surplus = memberInfo.Role10Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, prayCount, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.Role180Surplus = role180Surplus;
            memberInfo.Role90Surplus = role90Surplus;
            memberInfo.Role20Surplus = role20Surplus;
            memberInfo.Role10Surplus = role10Surplus;
            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.ParyFileInfo = prayCount == 1 ? DrawHelper.drawOnePrayImg(sortPrayRecords.First(), imgWidth) : DrawHelper.drawTenPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, role90SurplusBefore);
            return ysPrayResult;
        }

    }
}
