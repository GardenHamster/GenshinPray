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
    public class PermPrayService : BaseFloorPrayService
    {
        protected override YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
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

        public override YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int perm180Surplus = memberInfo.Perm180Surplus;
            int perm90Surplus = memberInfo.Perm90Surplus;
            int perm90SurplusBefore = memberInfo.Perm90Surplus;
            int perm20Surplus = memberInfo.Perm20Surplus;
            int perm10Surplus = memberInfo.Perm10Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, prayCount, ref perm180Surplus, ref perm90Surplus, ref perm20Surplus, ref perm10Surplus);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.Perm180Surplus = perm180Surplus;
            memberInfo.Perm90Surplus = perm90Surplus;
            memberInfo.Perm20Surplus = perm20Surplus;
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
