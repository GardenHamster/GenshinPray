using GenshinPray.Dao;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service.PrayService;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service.PrayService
{
    public class ArmPrayService : BasePrayService
    {
        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSProbability> AllList = new List<YSProbability>()
        {
            new YSProbability(0.7m, YSProbabilityType.五星物品),
            new YSProbability(6.0m, YSProbabilityType.四星物品),
            new YSProbability(93.3m,YSProbabilityType.三星物品)
        };

        /// <summary>
        /// 小保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor80List = new List<YSProbability>()
        {
            new YSProbability(100, YSProbabilityType.五星物品)
        };

        /// <summary>
        /// 十连保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor10List = new List<YSProbability>()
        {
            new YSProbability(0.7m, YSProbabilityType.五星物品),
            new YSProbability(99.3m,YSProbabilityType.四星物品)
        };


        /// <summary>
        /// 模拟抽卡,获取祈愿记录
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="assignGoodsItem"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <param name="assignValue">命定值</param>
        /// <param name="floor80Surplus">距离80小保底剩余多少抽</param>
        /// <param name="floor20Surplus">距离4星大保底剩余多少抽</param>
        /// <param name="floor10Surplus">距离4星小保底剩余多少抽</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(YSUpItem ySUpItem, YSGoodsItem assignGoodsItem, int prayCount, ref int assignValue, ref int floor80Surplus, ref int floor20Surplus, ref int floor10Surplus)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                floor80Surplus--;
                floor20Surplus--;
                floor10Surplus--;

                if (floor10Surplus > 0 && floor80Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus >= 10)//当祈愿获取到4星物品时，有75.000%的概率为本期4星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus < 10)//如果本次祈愿获取的4星物品非本期4星UP武器，下次祈愿获取的4星物品必定为本期4星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor80Surplus == 0 && assignValue < 2)//当祈愿获取到5星武器时，有75.000%的概率为本期5星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor80Surplus == 0 && assignValue >= 2)//命定值达到满值后，在本祈愿中获得的下一把5星武器必定为当前定轨武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }

                bool isUpItem = IsUpItem(ySUpItem, records[i].GoodsItem);//判断是否为本期up的物品
                bool isAssignItem = assignGoodsItem != null && records[i].GoodsItem.GoodsID == assignGoodsItem.GoodsID;//判断是否为本期定轨物品

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
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor80Surplus = 80;//八十发保底重置
                    if (assignGoodsItem != null) assignValue++;//如果已经定轨，命定值+1
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor80Surplus = 80;//八十发保底重置
                    if (assignGoodsItem != null) assignValue = 0;//命定值重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int assignValue, int floor20Surplus)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品)
            {
                //当祈愿获取到5星武器时，有75.000%的概率为本期5星UP武器
                bool isGetAssign = assignGoodsItem != null && assignValue >= 2;
                bool isGetUp = RandomHelper.getRandomBetween(1, 100) <= 75;
                if (isGetAssign) return new YSPrayRecord(assignGoodsItem);
                return isGetUp ? GetRandomInList(ysUpItem.Star5UpList) : GetRandomInList(ysUpItem.Star5NonUpList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品)
            {
                //当祈愿获取到4星物品时，有75.000%的概率为本期4星UP武器
                bool isGetUp = floor20Surplus < 10 ? true : RandomHelper.getRandomBetween(1, 100) <= 75;
                return isGetUp ? GetRandomInList(ysUpItem.Star4UpList) : GetRandomInList(ysUpItem.Star4NonUpList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.三星物品)
            {
                return GetRandomInList(ysUpItem.Star3AllList);
            }
            throw new GoodsNotFoundException($"未能随机获取与{Enum.GetName(typeof(YSProbability), ysProbability.ProbabilityType)}对应物品");
        }

        public YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int assignValue = memberInfo.ArmAssignValue;
            int arm80Surplus = memberInfo.Arm80Surplus;
            int arm80SurplusBefore = memberInfo.Arm80Surplus;
            int arm20Surplus = memberInfo.Arm20Surplus;
            int arm10Surplus = memberInfo.Arm10Surplus;
            
            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, assignGoodsItem, prayCount, ref assignValue, ref arm80Surplus, ref arm20Surplus, ref arm10Surplus);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.Arm80Surplus = arm80Surplus;
            memberInfo.Arm20Surplus = arm20Surplus;
            memberInfo.Arm10Surplus = arm10Surplus;
            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.ParyFileInfo = prayCount == 1 ? DrawHelper.drawOnePrayImg(sortPrayRecords.First(), imgWidth) : DrawHelper.drawTenPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, arm80SurplusBefore);
            return ysPrayResult;
        }




    }
}
