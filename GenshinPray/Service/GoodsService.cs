using GenshinPray.Common;
using GenshinPray.Dao;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System.Collections.Generic;
using System.Linq;

namespace GenshinPray.Service
{
    public class GoodsService
    {
        private GoodsDao goodsDao;

        public GoodsService()
        {
            this.goodsDao = new GoodsDao();
        }

        /// <summary>
        /// 加载蛋池数据到内存
        /// </summary>
        public void loadYSPrayItem()
        {
            SiteConfig.ArmStar3PermList = changeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.三星));//三星常驻武器
            SiteConfig.ArmStar4PermList = changeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.四星));//四星常驻武器
            SiteConfig.RoleStar4PermList = changeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.四星));//四星常驻角色
            SiteConfig.RoleStar5PermList = changeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.五星));//五星常驻角色

            List<GoodsPO> rolePondList = goodsDao.getByPondType(0, (int)YSPondType.角色);
            List<YSGoodsItem> roleItemList = changeToYSGoodsItem(rolePondList);
            List<YSGoodsItem> roleStar5UpList = roleItemList.Where(m => m.RareType == YSRareType.五星).ToList();
            List<YSGoodsItem> roleStar4UpList = roleItemList.Where(m => m.RareType == YSRareType.四星).ToList();
            List<YSGoodsItem> roleStar5NonUpList = getNonUpList(SiteConfig.RoleStar5PermList, roleStar5UpList);
            List<YSGoodsItem> roleStar4NonUpList = getNonUpList(SiteConfig.RoleStar4PermList, roleStar4UpList);
            List<YSGoodsItem> Star5AllList = concatList(SiteConfig.RoleStar5PermList, roleStar5UpList);
            List<YSGoodsItem> Star4AllList = concatList(SiteConfig.RoleStar4PermList, roleStar4UpList);

            YSUpItem RoleUpItem = new YSUpItem();
            RoleUpItem.Star5UpList = roleStar5UpList;
            RoleUpItem.Star4UpList = roleStar4UpList;
            RoleUpItem.Star5NonUpList = roleStar5NonUpList;
            RoleUpItem.Star4NonUpList = roleStar4NonUpList;
            RoleUpItem.Star5AllList = Star5AllList;
            RoleUpItem.Star4AllList = Star4AllList;
            RoleUpItem.Star3PermList = SiteConfig.ArmStar3PermList;
            SiteConfig.DefaultUpItem[YSPondType.角色] = RoleUpItem;


        }

        /// <summary>
        /// 返回非up列表
        /// </summary>
        /// <returns></returns>
        private List<YSGoodsItem> getNonUpList(List<YSGoodsItem> AllList, List<YSGoodsItem> UpList)
        {
            List<YSGoodsItem> NonUpList = new List<YSGoodsItem>();
            foreach (YSGoodsItem goodsItem in AllList)
            {
                if (UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) continue;
                NonUpList.Add(goodsItem);
            }
            return NonUpList;
        }


        /// <summary>
        /// 将GoodsPO转化为YSGoodsItem
        /// </summary>
        /// <param name="goodsPO"></param>
        /// <returns></returns>
        private YSGoodsItem changeToYSGoodsItem(GoodsPO goodsPO)
        {
            YSGoodsItem goodsItem = new YSGoodsItem();
            goodsItem.Probability = SiteConfig.DefaultPR;
            goodsItem.GoodsName = goodsPO.GoodsName;
            goodsItem.RareType = goodsPO.RareType;
            goodsItem.GoodsType = goodsPO.GoodsType;
            goodsItem.GoodsSubType = goodsPO.GoodsSubType;
            return goodsItem;
        }

        /// <summary>
        /// 将GoodsPO转化为YSGoodsItem
        /// </summary>
        /// <param name="poList"></param>
        /// <returns></returns>
        private List<YSGoodsItem> changeToYSGoodsItem(List<GoodsPO> poList)
        {
            List<YSGoodsItem> goodsItemList = new List<YSGoodsItem>();
            foreach (GoodsPO goodsPO in poList)
            {
                YSGoodsItem goodsItem = changeToYSGoodsItem(goodsPO);
                goodsItemList.Add(goodsItem);
            }
            return goodsItemList;
        }


        /// <summary>
        /// 读取自定义up信息,如果没有,使用默认up信息
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="pondType"></param>
        /// <returns></returns>
        public YSUpItem GetUpItem(int authId, YSPondType pondType)
        {
            YSUpItem defaultUpItem = SiteConfig.DefaultUpItem[pondType];
            List<GoodsPO> upList = goodsDao.getByPondType(authId, (int)pondType);
            if (upList == null || upList.Count == 0) return defaultUpItem;

            List<YSGoodsItem> Star5UpList = upList.Where(o => o.RareType == YSRareType.五星).Select(m => changeToYSGoodsItem(m)).ToList();
            List<YSGoodsItem> Star4UpList = upList.Where(o => o.RareType == YSRareType.四星).Select(m => changeToYSGoodsItem(m)).ToList();
            List<YSGoodsItem> Star5NonUpList = getNonUpList(defaultUpItem.Star5AllList, Star5UpList);
            List<YSGoodsItem> Star4NonUpList = getNonUpList(defaultUpItem.Star4AllList, Star4UpList);
            List<YSGoodsItem> Star5AllList = concatList(defaultUpItem.Star5AllList, Star5UpList);
            List<YSGoodsItem> Star4AllList = concatList(defaultUpItem.Star4AllList, Star4UpList);

            YSUpItem ySUpItem = new YSUpItem();
            ySUpItem.Star5UpList = Star5UpList;
            ySUpItem.Star4UpList = Star4UpList;
            ySUpItem.Star5NonUpList = Star5NonUpList;
            ySUpItem.Star4NonUpList = Star4NonUpList;
            ySUpItem.Star5AllList = Star5AllList;
            ySUpItem.Star4AllList = Star4AllList;
            ySUpItem.Star3PermList = defaultUpItem.Star3PermList;
            return ySUpItem;
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
