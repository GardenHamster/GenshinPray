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
    public class ArmPrayService : BaseAssignPrayService
    {
        protected override YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int assignValue, int floor20Surplus)
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

        public override YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int arm160Surplus = memberInfo.Arm160Surplus;
            int arm80Surplus = memberInfo.Arm80Surplus;
            int arm80SurplusBefore = memberInfo.Arm80Surplus;
            int arm20Surplus = memberInfo.Arm20Surplus;
            int arm10Surplus = memberInfo.Arm10Surplus;
            
            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, assignGoodsItem, prayCount, ref arm160Surplus, ref arm80Surplus, ref arm20Surplus, ref arm10Surplus);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.Arm160Surplus = arm160Surplus;
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
