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
    public class PermPrayService : BasePrayService
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
        /// <param name="floor90Surplus">距离90小保底剩余多少抽</param>
        /// <param name="floor10Surplus">距离4星小保底剩余多少抽</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(YSUpItem ySUpItem, int prayCount, ref int floor90Surplus, ref int floor10Surplus)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                floor90Surplus--;
                floor10Surplus--;

                if (floor10Surplus > 0 && floor90Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem);
                }
                if (floor10Surplus == 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem);
                }
                if (floor90Surplus == 0)//九十发保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem);
                }

                if (records[i].GoodsItem.RareType == YSRareType.四星)
                {
                    floor10Surplus = 10;//十连保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星)
                {
                    floor10Surplus = 10;//十连保底重置
                    floor90Surplus = 90;//九十发保底重置
                }
            }
            return records;
        }


        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ySUpItem)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品)
            {
                return GetRandomInList(ySUpItem.Star5AllList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品)
            {
                return GetRandomInList(ySUpItem.Star4AllList);
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
            int perm90Surplus = memberInfo.Perm90Surplus;
            int perm90SurplusBefore = memberInfo.Perm90Surplus;
            int perm10Surplus = memberInfo.Perm10Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, prayCount, ref perm90Surplus, ref perm10Surplus);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.Perm90Surplus = perm90Surplus;
            memberInfo.Perm10Surplus = perm10Surplus;
            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.ParyFileInfo = prayCount == 1 ? DrawHelper.drawOnePrayImg(sortPrayRecords.First(), imgWidth) : DrawHelper.drawTenPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, perm90SurplusBefore);
            return ysPrayResult;
        }

    }
}
