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
                memberInfo.Role180Surplus--;
                memberInfo.Role90Surplus--;
                memberInfo.Role20Surplus--;
                memberInfo.Role10Surplus--;

                if (memberInfo.Role10Surplus > 0 && memberInfo.Role90Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem, memberInfo.Role180Surplus, memberInfo.Role20Surplus);
                }
                if (memberInfo.Role10Surplus == 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, memberInfo.Role180Surplus, memberInfo.Role20Surplus);
                }
                if (17 - memberInfo.Role90Surplus >= RandomHelper.getRandomBetween(0, 17))//九十发保底，从73发往后每次出货率大幅提升
                {
                    records[i] = GetActualItem(GetRandomInList(Floor90List), ySUpItem, memberInfo.Role180Surplus, memberInfo.Role20Surplus);
                }

                bool isUpItem = IsUpItem(ySUpItem, records[i].GoodsItem);//判断是否为本期up的物品
                records[i].OwnCount = memberGoods.Where(m => m.GoodsName == records[i].GoodsItem.GoodsName).Count();

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == false)
                {
                    memberInfo.Role10Surplus = 10;//十连小保底重置
                    memberInfo.Role20Surplus = 10;//十连大保底重置为10
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == true)
                {
                    memberInfo.Role10Surplus = 10;//十连小保底重置
                    memberInfo.Role20Surplus = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isUpItem == false)
                {
                    memberInfo.Role10Surplus = 10;//十连小保底重置
                    memberInfo.Role20Surplus = 20;//十连大保底重置
                    memberInfo.Role90Surplus = 90;//九十发小保底重置
                    memberInfo.Role180Surplus = 90;//九十发大保底重置为90
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isUpItem == true)
                {
                    memberInfo.Role10Surplus = 10;//十连小保底重置
                    memberInfo.Role20Surplus = 20;//十连大保底重置
                    memberInfo.Role90Surplus = 90;//九十发小保底重置
                    memberInfo.Role180Surplus = 180;//九十发大保底重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品)
            {
                //当祈愿获取到5星角色时，有50.000%的概率为本期5星UP角色
                bool isGetUp = floor180Surplus < 90 ? true : RandomHelper.getRandomBetween(1, 100) <= 50;
                return isGetUp ? GetRandomInList(ySUpItem.Star5UpList) : GetRandomInList(ySUpItem.Star5NonUpList);
            }
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品)
            {
                //当祈愿获取到4星物品时，有50.000%的概率为本期4星UP角色
                bool isGetUp = floor20Surplus < 10 ? true : RandomHelper.getRandomBetween(1, 100) <= 50;
                return isGetUp ? GetRandomInList(ySUpItem.Star4UpList) : GetRandomInList(ySUpItem.Star4NonUpList);
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
            int role90SurplusBefore = memberInfo.Role90Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(memberInfo, ysUpItem, memberGoods, prayCount);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.RolePrayTimes += prayCount;
            memberInfo.TotalPrayTimes += prayCount;
            memberDao.Update(memberInfo);//更新保底信息

            ysPrayResult.MemberInfo = memberInfo;
            ysPrayResult.ParyFileInfo = DrawPrayImg(sortPrayRecords, imgWidth, memberInfo.MemberCode);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, role90SurplusBefore);
            ysPrayResult.Surplus10 = memberInfo.Role10Surplus;
            return ysPrayResult;
        }

    }
}
