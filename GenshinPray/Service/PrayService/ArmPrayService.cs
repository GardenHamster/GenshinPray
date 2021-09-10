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
        
        protected override YSPrayRecord GetActualItem(YSProbability ySProbability, YSUpItem ySUpItem, int assignValue, int floor20Surplus)
        {
            if (prayRecord.GoodsItem.GoodsName == "5星物品")
            {
                bool isGetUp = floor160Surplus < 80 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? GetRandomGoodsInList(ySUpItem.Star5UpList) : GetRandomGoodsInList(ySUpItem.Star5NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "4星物品")
            {
                bool isGetUp = floor20Surplus < 10 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? GetRandomGoodsInList(ySUpItem.Star4UpList) : GetRandomGoodsInList(ySUpItem.Star4NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "3星物品")
            {
                return GetRandomGoodsInList(ySUpItem.Star3AllList);
            }
            return prayRecord;
        }

        public override YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int arm160Surplus = memberInfo.Arm160Surplus;
            int arm80Surplus = memberInfo.Arm80Surplus;
            int arm80SurplusBefore = memberInfo.Arm80Surplus;
            int arm20Surplus = memberInfo.Arm20Surplus;
            int arm10Surplus = memberInfo.Arm10Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(ysUpItem, prayCount, ref arm160Surplus, ref arm80Surplus, ref arm20Surplus, ref arm10Surplus);
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
