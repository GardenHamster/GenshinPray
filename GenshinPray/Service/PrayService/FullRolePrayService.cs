using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenshinPray.Service.PrayService
{
    public class FullRolePrayService : BasePrayService
    {
        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSProbability> SingleList = new List<YSProbability>()
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
        /// <param name="memberInfo"></param>
        /// <param name="ySUpItem"></param>
        /// <param name="memberGoods"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(MemberPO memberInfo, YSUpItem ySUpItem, List<MemberGoodsDTO> memberGoods, int prayCount)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                memberInfo.FullRole90Surplus--;
                memberInfo.FullRole10Surplus--;

                if (memberInfo.FullRole10Surplus > 0)//无保底
                {
                    records[i] = GetActualItem(GetRandomInList(SingleList), ySUpItem);
                }
                if (memberInfo.FullRole10Surplus <= 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem);
                }

                //角色池从第74抽开始,每抽出5星概率提高6%(基础概率),直到第90抽时概率上升到100%
                if (memberInfo.FullRole90Surplus < 16 && RandomHelper.getRandomBetween(1, 100) < (16 - memberInfo.FullRole90Surplus + 1) * 0.06 * 100)//低保
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem);
                }

                records[i].IsNew = CheckIsNew(memberGoods, records, records[i]);//判断是否为New
                records[i].OwnCountBefore = GetOwnCountBefore(memberGoods, records, records[i]);//统计已拥有数量

                if (records[i].GoodsItem.RareType == YSRareType.四星)
                {
                    records[i].Cost = 10 - memberInfo.FullRole10Surplus;
                    memberInfo.FullRole10Surplus = 10;//十连小保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星)
                {
                    records[i].Cost = 90 - memberInfo.FullRole90Surplus;
                    memberInfo.FullRole10Surplus = 10;//十连小保底重置
                    memberInfo.FullRole90Surplus = 90;//九十发小保底重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品) return GetRandomInList(ysUpItem.Star5AllList);
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品) return GetRandomInList(ysUpItem.Star4AllList);
            if (ysProbability.ProbabilityType == YSProbabilityType.三星物品) return GetRandomInList(ysUpItem.Star3AllList);
            throw new GoodsNotFoundException($"未能随机获取与{Enum.GetName(typeof(YSProbability), ysProbability.ProbabilityType)}类型对应的物品");
        }

        public YSPrayResult GetPrayResult(AuthorizePO authorize, MemberPO memberInfo, YSUpItem ysUpItem, List<MemberGoodsDTO> memberGoods, int prayCount)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int role90SurplusBefore = memberInfo.FullRole90Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(memberInfo, ysUpItem, memberGoods, prayCount);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.MemberInfo = memberInfo;
            ysPrayResult.Authorize = authorize;
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, role90SurplusBefore, 90);
            ysPrayResult.Surplus10 = memberInfo.FullRole10Surplus;
            return ysPrayResult;
        }

    }
}
