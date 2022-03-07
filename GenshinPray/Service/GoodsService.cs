using GenshinPray.Common;
using GenshinPray.Dao;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System.Collections.Generic;
using System.Linq;

namespace GenshinPray.Service
{
    public class GoodsService : BaseService
    {
        private GoodsDao goodsDao;
        private PondGoodsDao pondGoodsDao;

        public GoodsService()
        {
            this.goodsDao = new GoodsDao();
            this.pondGoodsDao = new PondGoodsDao();
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
        /// 根据物品id获取GoodsPO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsPO GetGoodsById(int id)
        {
            return goodsDao.GetById(id);
        }

        /// <summary>
        /// 根据物品名称获取GoodsPO
        /// </summary>
        /// <param name="goodsName"></param>
        /// <returns></returns>
        public GoodsPO GetGoodsByName(string goodsName)
        {
            return goodsDao.getByGoodsName(goodsName);
        }


        /// <summary>
        /// 加载蛋池数据到内存
        /// </summary>
        public void LoadYSPrayItem()
        {
            DataCache.DefaultUpItem = new Dictionary<YSPondType, Dictionary<int, YSUpItem>>();
            DataCache.ArmStar3PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.三星));//三星常驻武器
            DataCache.ArmStar4PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.四星));//四星常驻武器
            DataCache.ArmStar5PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.武器, YSRareType.五星));//五星常驻武器
            DataCache.RoleStar4PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.四星));//四星常驻角色
            DataCache.RoleStar5PermList = ChangeToYSGoodsItem(goodsDao.getPermGoods(YSGoodsType.角色, YSRareType.五星));//五星常驻角色
            DataCache.Star5PermList = ConcatList(DataCache.RoleStar5PermList, DataCache.ArmStar5PermList);
            DataCache.Star4PermList = ConcatList(DataCache.RoleStar4PermList, DataCache.ArmStar4PermList);

            //加载默认常驻池
            YSUpItem PermItem = new YSUpItem();
            PermItem.Star5UpList = DataCache.Star5PermList;
            PermItem.Star4UpList = DataCache.Star4PermList;
            PermItem.Star5NonUpList = new List<YSGoodsItem>();
            PermItem.Star4NonUpList = new List<YSGoodsItem>();
            PermItem.Star5AllList = DataCache.Star5PermList;
            PermItem.Star4AllList = DataCache.Star4PermList;
            PermItem.Star3AllList = DataCache.ArmStar3PermList;
            DataCache.DefaultUpItem[YSPondType.常驻] = new Dictionary<int, YSUpItem>();
            DataCache.DefaultUpItem[YSPondType.常驻][0] = PermItem;

            //加载默认角色池
            DataCache.DefaultUpItem[YSPondType.角色] = new Dictionary<int, YSUpItem>();
            List<YSGoodsItem> roleItemList = goodsDao.getByPondType(0, YSPondType.角色);
            List<int> roleIndexList = roleItemList.Select(m => m.PondIndex).Distinct().ToList();
            foreach (int pondIndex in roleIndexList)
            {
                List<YSGoodsItem> roleStar5UpList = roleItemList.Where(m => m.RareType == YSRareType.五星 && m.PondIndex == pondIndex).ToList();
                List<YSGoodsItem> roleStar4UpList = roleItemList.Where(m => m.RareType == YSRareType.四星 && m.PondIndex == pondIndex).ToList();
                List<YSGoodsItem> roleStar5NonUpList = GetNonUpList(DataCache.RoleStar5PermList, roleStar5UpList);
                List<YSGoodsItem> roleStar4NonUpList = GetNonUpList(DataCache.Star4PermList, roleStar4UpList);
                List<YSGoodsItem> roleStar5AllList = ConcatList(DataCache.RoleStar5PermList, roleStar5UpList);
                List<YSGoodsItem> roleStar4AllList = ConcatList(DataCache.RoleStar4PermList, DataCache.ArmStar4PermList, roleStar4UpList);
                YSUpItem roleUpItem = new YSUpItem();
                roleUpItem.Star5UpList = roleStar5UpList;
                roleUpItem.Star4UpList = roleStar4UpList;
                roleUpItem.Star5NonUpList = roleStar5NonUpList;
                roleUpItem.Star4NonUpList = roleStar4NonUpList;
                roleUpItem.Star5AllList = roleStar5AllList;
                roleUpItem.Star4AllList = roleStar4AllList;
                roleUpItem.Star3AllList = DataCache.ArmStar3PermList;
                DataCache.DefaultUpItem[YSPondType.角色][pondIndex] = roleUpItem;
            }

            //加载默认武器池
            DataCache.DefaultUpItem[YSPondType.武器] = new Dictionary<int, YSUpItem>();
            List<YSGoodsItem> armItemList = goodsDao.getByPondType(0, YSPondType.武器);
            List<int> armIndexList = armItemList.Select(m => m.PondIndex).Distinct().ToList();
            foreach (int pondIndex in armIndexList)
            {
                List<YSGoodsItem> armStar5UpList = armItemList.Where(m => m.RareType == YSRareType.五星 && m.PondIndex == pondIndex).ToList();
                List<YSGoodsItem> armStar4UpList = armItemList.Where(m => m.RareType == YSRareType.四星 && m.PondIndex == pondIndex).ToList();
                List<YSGoodsItem> armStar5NonUpList = GetNonUpList(DataCache.ArmStar5PermList, armStar5UpList);
                List<YSGoodsItem> armStar4NonUpList = GetNonUpList(DataCache.ArmStar4PermList, armStar4UpList);
                List<YSGoodsItem> armStar5AllList = ConcatList(DataCache.ArmStar5PermList, armStar5UpList);
                List<YSGoodsItem> armStar4AllList = ConcatList(DataCache.ArmStar4PermList, armStar4UpList);
                YSUpItem armUpItem = new YSUpItem();
                armUpItem.Star5UpList = armStar5UpList;
                armUpItem.Star4UpList = armStar4UpList;
                armUpItem.Star5NonUpList = armStar5NonUpList;
                armUpItem.Star4NonUpList = armStar4NonUpList;
                armUpItem.Star5AllList = armStar5AllList;
                armUpItem.Star4AllList = armStar4AllList;
                armUpItem.Star3AllList = DataCache.ArmStar3PermList;
                DataCache.DefaultUpItem[YSPondType.武器][pondIndex] = armUpItem;
            }

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
        public Dictionary<int, YSUpItem> GetUpItem(int authId, YSPondType pondType)
        {
            List<YSGoodsItem> upList = goodsDao.getByPondType(authId, pondType);
            if (upList == null || upList.Count == 0) return DataCache.DefaultUpItem[pondType];
            YSUpItem defaultUpItem = DataCache.DefaultUpItem[pondType][0];
            Dictionary<int, YSUpItem> upItemDic = new Dictionary<int, YSUpItem>();
            List<int> indexList = upList.Select(m => m.PondIndex).Distinct().ToList();
            foreach (int pondIndex in indexList)
            {
                List<YSGoodsItem> Star5UpList = upList.Where(o => o.RareType == YSRareType.五星 && o.PondIndex == pondIndex).ToList();
                List<YSGoodsItem> Star4UpList = upList.Where(o => o.RareType == YSRareType.四星 && o.PondIndex == pondIndex).ToList();
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
                upItemDic[pondIndex] = ySUpItem;
            }
            return upItemDic;
        }

        /// <summary>
        /// 获取群员已有物品级数量
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="memberCode"></param>
        /// <returns></returns>
        public List<MemberGoodsDTO> GetMemberGoods(int authId, string memberCode)
        {
            return goodsDao.GetMemberGoods(authId, memberCode);
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
        /// 连接所有集合,返回无重复部分
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        private List<YSGoodsItem> ConcatList(params List<YSGoodsItem>[] lists)
        {
            List<YSGoodsItem> returnList = new List<YSGoodsItem>();
            foreach (var list in lists)
            {
                foreach (var item in list)
                {
                    if (returnList.Where(m => m.GoodsName == item.GoodsName).Any()) continue;
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        /// <summary>
        /// 清理蛋池
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="pondType"></param>
        /// <param name="pondIndex"></param>
        /// <returns></returns>
        public int ClearPondGoods(int authId, YSPondType pondType, int pondIndex)
        {
            return pondGoodsDao.clearPondGoods(authId, pondType, pondIndex);
        }

        /// <summary>
        /// 清理蛋池
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="authId"></param>
        /// <param name="pondType"></param>
        /// <param name="pondIndex"></param>
        /// <returns></returns>
        public void AddPondGoods(List<GoodsPO> goods, int authId, YSPondType pondType, int pondIndex)
        {
            foreach (var good in goods)
            {
                PondGoodsPO pondGoods = new PondGoodsPO();
                pondGoods.AuthId = authId;
                pondGoods.PondIndex = pondIndex;
                pondGoods.GoodsId = good.Id;
                pondGoods.PondType = pondType;
                pondGoodsDao.Insert(pondGoods);
            }
        }



    }
}
