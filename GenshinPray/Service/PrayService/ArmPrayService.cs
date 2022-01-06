﻿using GenshinPray.Dao;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
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
        /// <param name="memberInfo"></param>
        /// <param name="ySUpItem"></param>
        /// <param name="assignGoodsItem"></param>
        /// <param name="memberGoods"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(MemberPO memberInfo, YSUpItem ySUpItem, YSGoodsItem assignGoodsItem, List<MemberGoodsDTO> memberGoods, int prayCount)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                memberInfo.Arm80Surplus--;
                memberInfo.Arm20Surplus--;
                memberInfo.Arm10Surplus--;

                if (memberInfo.Arm10Surplus > 0 && memberInfo.Arm80Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem, assignGoodsItem, memberInfo.ArmAssignValue, memberInfo.Arm20Surplus);
                }
                if (memberInfo.Arm10Surplus == 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, assignGoodsItem, memberInfo.ArmAssignValue, memberInfo.Arm20Surplus);
                }
                if (17 - memberInfo.Arm80Surplus >= RandomHelper.getRandomBetween(0, 17))//八十发保底，从63发往后每次出货率大幅提升
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem, assignGoodsItem, memberInfo.ArmAssignValue, memberInfo.Arm20Surplus);
                }

                bool isUpItem = IsUpItem(ySUpItem, records[i].GoodsItem);//判断是否为本期up的物品
                bool isAssignItem = assignGoodsItem != null && records[i].GoodsItem.GoodsID == assignGoodsItem.GoodsID;//判断是否为本期定轨物品
                records[i].OwnCount = memberGoods.Where(m => m.GoodsName == records[i].GoodsItem.GoodsName).Count();

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == false)
                {
                    memberInfo.Arm10Surplus = 10;//十连小保底重置
                    memberInfo.Arm20Surplus = 10;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == true)
                {
                    memberInfo.Arm10Surplus = 10;//十连小保底重置
                    memberInfo.Arm20Surplus = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == false)
                {
                    memberInfo.Arm10Surplus = 10;//十连小保底重置
                    memberInfo.Arm20Surplus = 20;//十连大保底重置
                    memberInfo.Arm80Surplus = 80;//八十发保底重置
                    if (assignGoodsItem != null) memberInfo.ArmAssignValue++;//如果已经定轨，命定值+1
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == true)
                {
                    memberInfo.Arm10Surplus = 10;//十连小保底重置
                    memberInfo.Arm20Surplus = 20;//十连大保底重置
                    memberInfo.Arm80Surplus = 80;//八十发保底重置
                    if (assignGoodsItem != null) memberInfo.ArmAssignValue = 0;//命定值重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int assignValue, int floor20Surplus)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品)
            {
                //命定值达到满值后，在本祈愿中获得的下一把5星武器必定为当前定轨武器
                bool isGetAssign = assignGoodsItem != null && assignValue >= 2;
                if (isGetAssign) return new YSPrayRecord(assignGoodsItem);
                //当祈愿获取到5星武器时，有75.000%的概率为本期5星UP武器
                bool isGetUp = RandomHelper.getRandomBetween(1, 100) <= 75;
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

        public YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, List<MemberGoodsDTO> memberGoods, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int arm80SurplusBefore = memberInfo.Arm80Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(memberInfo, ysUpItem, assignGoodsItem, memberGoods, prayCount);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.ArmPrayTimes += prayCount;
            memberInfo.TotalPrayTimes += prayCount;
            memberDao.Update(memberInfo);//更新保底信息

            ysPrayResult.MemberInfo = memberInfo;
            ysPrayResult.ParyFileInfo = DrawPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, arm80SurplusBefore);
            ysPrayResult.Surplus10 = memberInfo.Arm10Surplus;
            return ysPrayResult;
        }

        


    }
}
