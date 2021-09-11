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
        /// 根据Id获取YSGoodsItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YSGoodsItem GetGoodsItemById(int id)
        {
            GoodsPO goodsPO = goodsDao.GetById(id);
            if (goodsPO == null) return null;
            return ChangeToYSGoodsItem(goodsPO);
        }

        /// <summary>
        /// 加载蛋池数据到内存
        /// </summary>
        public void LoadYSPrayItem()
        {
            SiteConfig.DefaultUpItem = new Dictionary<YSPondType, YSUpItem>();

            SiteConfig.ArmStar3PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.三星));//三星常驻武器
            SiteConfig.ArmStar4PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.四星));//四星常驻武器
            SiteConfig.ArmStar5PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.五星));//五星常驻武器
            SiteConfig.RoleStar4PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.四星));//四星常驻角色
            SiteConfig.RoleStar5PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.五星));//五星常驻角色

            YSUpItem PermItem = new YSUpItem();
            List<YSGoodsItem> permStar5AllList = ConcatList(SiteConfig.RoleStar5PermList, SiteConfig.ArmStar5PermList);
            List<YSGoodsItem> permStar4AllList = ConcatList(SiteConfig.RoleStar4PermList, SiteConfig.ArmStar4PermList);
            PermItem.Star5UpList = permStar5AllList;
            PermItem.Star4UpList = permStar4AllList;
            PermItem.Star5NonUpList = new List<YSGoodsItem>();
            PermItem.Star4NonUpList = new List<YSGoodsItem>();
            PermItem.Star5AllList = permStar5AllList;
            PermItem.Star4AllList = permStar4AllList;
            PermItem.Star3AllList = SiteConfig.ArmStar3PermList;
            SiteConfig.DefaultUpItem[YSPondType.常驻] = PermItem;

            List<GoodsPO> rolePondList = goodsDao.getByPondType(0, (int)YSPondType.角色);
            List<YSGoodsItem> roleItemList = ChangeToYSGoodsItem(rolePondList);
            List<YSGoodsItem> roleStar5UpList = roleItemList.Where(m => m.RareType == YSRareType.五星).ToList();
            List<YSGoodsItem> roleStar4UpList = roleItemList.Where(m => m.RareType == YSRareType.四星).ToList();
            List<YSGoodsItem> roleStar5NonUpList = GetNonUpList(SiteConfig.RoleStar5PermList, roleStar5UpList);
            List<YSGoodsItem> roleStar4NonUpList = GetNonUpList(SiteConfig.RoleStar4PermList, roleStar4UpList);
            List<YSGoodsItem> roleStar5AllList = ConcatList(SiteConfig.RoleStar5PermList, roleStar5UpList);
            List<YSGoodsItem> roleStar4AllList = ConcatList(SiteConfig.RoleStar4PermList, roleStar4UpList);
            YSUpItem RoleUpItem = new YSUpItem();
            RoleUpItem.Star5UpList = roleStar5UpList;
            RoleUpItem.Star4UpList = roleStar4UpList;
            RoleUpItem.Star5NonUpList = roleStar5NonUpList;
            RoleUpItem.Star4NonUpList = roleStar4NonUpList;
            RoleUpItem.Star5AllList = roleStar5AllList;
            RoleUpItem.Star4AllList = roleStar4AllList;
            RoleUpItem.Star3AllList = SiteConfig.ArmStar3PermList;
            SiteConfig.DefaultUpItem[YSPondType.角色] = RoleUpItem;

            List<GoodsPO> armPondList = goodsDao.getByPondType(0, (int)YSPondType.武器);
            List<YSGoodsItem> armItemList = ChangeToYSGoodsItem(armPondList);
            List<YSGoodsItem> armStar5UpList = armItemList.Where(m => m.RareType == YSRareType.五星).ToList();
            List<YSGoodsItem> armStar4UpList = armItemList.Where(m => m.RareType == YSRareType.四星).ToList();
            List<YSGoodsItem> armStar5NonUpList = GetNonUpList(SiteConfig.ArmStar5PermList, armStar5UpList);
            List<YSGoodsItem> armStar4NonUpList = GetNonUpList(SiteConfig.ArmStar4PermList, armStar4UpList);
            List<YSGoodsItem> armStar5AllList = ConcatList(SiteConfig.ArmStar5PermList, armStar5UpList);
            List<YSGoodsItem> armStar4AllList = ConcatList(SiteConfig.ArmStar4PermList, armStar4UpList);
            YSUpItem ArmUpItem = new YSUpItem();
            ArmUpItem.Star5UpList = armStar5UpList;
            ArmUpItem.Star4UpList = armStar4UpList;
            ArmUpItem.Star5NonUpList = armStar5NonUpList;
            ArmUpItem.Star4NonUpList = armStar4NonUpList;
            ArmUpItem.Star5AllList = armStar5AllList;
            ArmUpItem.Star4AllList = armStar4AllList;
            ArmUpItem.Star3AllList = SiteConfig.ArmStar3PermList;
            SiteConfig.DefaultUpItem[YSPondType.武器] = ArmUpItem;
        }

        /// <summary>
        /// 返回非up列表
        /// </summary>
        /// <returns></returns>
        private List<YSGoodsItem> GetNonUpList(List<YSGoodsItem> AllList, List<YSGoodsItem> UpList)
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
        private YSGoodsItem ChangeToYSGoodsItem(GoodsPO goodsPO)
        {
            YSGoodsItem goodsItem = new YSGoodsItem();
            goodsItem.GoodsID = goodsPO.Id;
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
        private List<YSGoodsItem> ChangeToYSGoodsItem(List<GoodsPO> poList)
        {
            List<YSGoodsItem> goodsItemList = new List<YSGoodsItem>();
            foreach (GoodsPO goodsPO in poList)
            {
                YSGoodsItem goodsItem = ChangeToYSGoodsItem(goodsPO);
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

            List<YSGoodsItem> Star5UpList = upList.Where(o => o.RareType == YSRareType.五星).Select(m => ChangeToYSGoodsItem(m)).ToList();
            List<YSGoodsItem> Star4UpList = upList.Where(o => o.RareType == YSRareType.四星).Select(m => ChangeToYSGoodsItem(m)).ToList();
            List<YSGoodsItem> Star5NonUpList = GetNonUpList(defaultUpItem.Star5AllList, Star5UpList);
            List<YSGoodsItem> Star4NonUpList = GetNonUpList(defaultUpItem.Star4AllList, Star4UpList);
            List<YSGoodsItem> Star5AllList = ConcatList(defaultUpItem.Star5AllList, Star5UpList);
            List<YSGoodsItem> Star4AllList = ConcatList(defaultUpItem.Star4AllList, Star4UpList);

            YSUpItem ySUpItem = new YSUpItem();
            ySUpItem.Star5UpList = Star5UpList;
            ySUpItem.Star4UpList = Star4UpList;
            ySUpItem.Star5NonUpList = Star5NonUpList;
            ySUpItem.Star4NonUpList = Star4NonUpList;
            ySUpItem.Star5AllList = Star5AllList;
            ySUpItem.Star4AllList = Star4AllList;
            ySUpItem.Star3AllList = defaultUpItem.Star3AllList;
            return ySUpItem;
        }

        /// <summary>
        /// 根据定轨物品id，返回YSUpItem，如果当前Up池中不包含该定轨物品id，返回null
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="armAssignId"></param>
        /// <returns></returns>
        public YSGoodsItem getAssignGoodsItem(YSUpItem ySUpItem, int armAssignId)
        {
            if (armAssignId == 0) return null;
            GoodsPO goodsInfo = goodsDao.GetById(armAssignId);
            if (goodsInfo == null) return null;
            if (ySUpItem.Star5UpList.Where(o => o.GoodsID == armAssignId).Count() == 0) return null;
            return ChangeToYSGoodsItem(goodsInfo);
        }

        /// <summary>
        /// 连接两个集合,返回无重复部分
        /// </summary>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <returns></returns>
        private List<YSGoodsItem> ConcatList(List<YSGoodsItem> listA, List<YSGoodsItem> listB)
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
