using GenshinPray.Dao;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class MemberGoodsService
    {
        private MemberGoodsDao memberGoodsDao;

        public MemberGoodsService()
        {
            this.memberGoodsDao = new MemberGoodsDao();
        }

        public void AddMemberGoods(YSPrayResult ySPrayResult, int authId, string memberCode)
        {
            foreach (var result in ySPrayResult.PrayRecords)
            {
                if (result.GoodsItem.RareType != YSRareType.四星 && result.GoodsItem.RareType != YSRareType.五星) continue;
                MemberGoodsPO memberGoods = new MemberGoodsPO();
                memberGoods.AuthId = authId;
                memberGoods.GoodsName = result.GoodsItem.GoodsName;
                memberGoods.GoodsType = result.GoodsItem.GoodsType;
                memberGoods.GoodsSubType = result.GoodsItem.GoodsSubType;
                memberGoods.RareType = result.GoodsItem.RareType;
                memberGoods.MemberCode = memberCode;
                memberGoods.CreateDate = DateTime.Now;
                memberGoodsDao.Insert(memberGoods);
            }
        }

    }
}
