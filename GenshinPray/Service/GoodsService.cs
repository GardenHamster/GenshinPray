using GenshinPray.Common;
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
    public class GoodsService
    {
        private GoodsDao goodsDao;

        public GoodsService()
        {
            this.goodsDao = new GoodsDao();
        }

        public void loadYSUpItem()
        {
            List<GoodsPO> rolePondList = goodsDao.getByPondType((int)YSPondType.角色);
            List<YSGoodsItem> roleItemList = changeToYSGoodsItem(rolePondList);
            List<YSGoodsItem> roleStar5UpList = roleItemList.Where(m => m.RareType == YSRareType.五星).ToList();
            List<YSGoodsItem> roleStar4UpList = roleItemList.Where(m => m.RareType == YSRareType.四星).ToList();
            List<YSGoodsItem> roleStar5NonUpList = getStar5NonUpList(Setting.RoleStar5PermList, roleStar5UpList);
            List<YSGoodsItem> roleStar4NonUpList = getStar4NonUpList(Setting.RoleStar4PermList, roleStar4UpList);
            Setting.RoleStar5UpList = roleStar5UpList;
            Setting.RoleStar4UpList = roleStar4UpList;
            Setting.RoleStar5NonUpList = roleStar5NonUpList;
            Setting.RoleStar4NonUpList = roleStar4NonUpList;
        }

        /// <summary>
        /// 获取5星非up列表
        /// </summary>
        /// <returns></returns>
        private List<YSGoodsItem> getStar5NonUpList(List<YSGoodsItem> star5List, List<YSGoodsItem> star5UpList)
        {
            List<YSGoodsItem> star5NonUpList = new List<YSGoodsItem>();
            foreach (YSGoodsItem goodsItem in star5List)
            {
                if (star5UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) continue;
                star5NonUpList.Add(goodsItem);
            }
            return star5NonUpList;
        }

        /// <summary>
        /// 获取4星非up列表
        /// </summary>
        /// <returns></returns>
        private List<YSGoodsItem> getStar4NonUpList(List<YSGoodsItem> star4List, List<YSGoodsItem> star4UpList)
        {
            List<YSGoodsItem> star4NonUpList = new List<YSGoodsItem>();
            foreach (YSGoodsItem goodsItem in star4List)
            {
                if (star4UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) continue;
                star4NonUpList.Add(goodsItem);
            }
            return star4NonUpList;
        }

        /// <summary>
        /// 将GoodsPO转化为YSGoodsItem
        /// </summary>
        /// <param name="poList"></param>
        /// <returns></returns>
        private List<YSGoodsItem> changeToYSGoodsItem(List<GoodsPO> poList)
        {
            List<YSGoodsItem> goodsItemList = new List<YSGoodsItem>();
            foreach (GoodsPO item in poList)
            {
                YSGoodsItem goodsItem = new YSGoodsItem();
                goodsItem.Probability = Setting.DefaultProbability;
                goodsItem.GoodsName = item.GoodsName;
                goodsItem.RareType = item.RareType;
                goodsItem.GoodsType = item.GoodsType;
                goodsItemList.Add(goodsItem);
            }
            return goodsItemList;
        }

        /// <summary>
        /// 连接两个集合,返回无重复部分
        /// </summary>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <returns></returns>
        private List<YSGoodsItem> concatList(List<YSGoodsItem> listA, List<YSGoodsItem> listB)
        {
            List<YSGoodsItem> returnList = new List<YSGoodsItem>();
            foreach (var item in listA)
            {
                if (returnList.Where(m => m.GoodsName == item.GoodsName).Count() > 0) continue;
                returnList.Add(item);
            }
            foreach (var item in listB)
            {
                if (returnList.Where(m => m.GoodsName == item.GoodsName).Count() > 0) continue;
                returnList.Add(item);
            }
            return returnList;
        }




    }
}
