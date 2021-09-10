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
    public class RolePrayService : BaseFloorPrayService
    {
        protected override YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
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

        public override YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
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
