using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class RolePrayService : BasePrayService
    {

        /// <summary>
        /// 根据名称随机实际补给项目
        /// </summary>
        /// <param name="prayRecord"></param>
        /// <param name="ySUpItem"></param>
        /// <param name="floor180Surplus"></param>
        /// <param name="floor20Surplus"></param>
        /// <returns></returns>
        protected override YSPrayRecord getPrayRecord(YSPrayRecord prayRecord, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
        {
            if (prayRecord.GoodsItem.GoodsName == "5星物品")
            {
                bool isGetUp = floor180Surplus < 90 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? getRandomGoodsInList(ySUpItem.Star5UpList) : getRandomGoodsInList(ySUpItem.Star5NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "4星物品")
            {
                bool isGetUp = floor20Surplus < 10 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? getRandomGoodsInList(ySUpItem.Star4UpList) : getRandomGoodsInList(ySUpItem.Star4NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "3星物品")
            {
                return getRandomGoodsInList(ySUpItem.Star3PermList);
            }
            return prayRecord;
        }

    }
}
