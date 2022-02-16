using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
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
                memberInfo.Perm90Surplus--;
                memberInfo.Perm10Surplus--;

                if (memberInfo.Perm10Surplus > 0 && memberInfo.Perm90Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem);
                }
                if (memberInfo.Perm10Surplus <= 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem);
                }
                if (memberInfo.Perm90Surplus <= 0)//九十发保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem);
                }

                records[i].OwnCount = memberGoods.Where(m => m.GoodsName == records[i].GoodsItem.GoodsName).Count();

                if (records[i].GoodsItem.RareType == YSRareType.四星)
                {
                    memberInfo.Perm10Surplus = 10;//十连保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星)
                {
                    memberInfo.Perm10Surplus = 10;//十连保底重置
                    memberInfo.Perm90Surplus = 90;//九十发保底重置
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

        public YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, List<MemberGoodsDTO> memberGoods, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int perm90SurplusBefore = memberInfo.Perm90Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(memberInfo, ysUpItem, memberGoods, prayCount);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.PermPrayTimes += prayCount;
            memberInfo.TotalPrayTimes += prayCount;
            memberDao.Update(memberInfo);//更新保底信息

            ysPrayResult.MemberInfo = memberInfo;
            ysPrayResult.ParyFileInfo = DrawPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, perm90SurplusBefore, 90);
            ysPrayResult.Surplus10 = memberInfo.Perm10Surplus;
            return ysPrayResult;
        }

    }
}
