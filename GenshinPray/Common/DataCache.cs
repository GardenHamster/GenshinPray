using GenshinPray.Models;
using GenshinPray.Models.VO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Common
{
    public static class DataCache
    {
        /// <summary>
        /// 5星角色常驻列表
        /// </summary>
        public static List<YSGoodsItem> RoleStar5PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星角色常驻列表
        /// </summary>
        public static List<YSGoodsItem> RoleStar4PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 5星武器常驻列表
        /// </summary>
        public static List<YSGoodsItem> ArmStar5PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星武器常驻列表
        /// </summary>
        public static List<YSGoodsItem> ArmStar4PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 3星武器常驻列表
        /// </summary>
        public static List<YSGoodsItem> ArmStar3PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 默认UP
        /// </summary>
        public static Dictionary<YSPondType, List<YSUpItem>> DefaultUpItem = new Dictionary<YSPondType, List<YSUpItem>>();

        /// <summary>
        /// 排行缓存
        /// </summary>
        private static Dictionary<int, LuckRankingVO> RankingCache = new Dictionary<int, LuckRankingVO>();

        /// <summary>
        /// 从缓存中获取欧气排行
        /// </summary>
        /// <param name="authId"></param>
        /// <returns></returns>
        public static LuckRankingVO GetLuckRankingCache(int authId)
        {
            if (RankingCache.ContainsKey(authId) == false) return null;
            if (RankingCache[authId] == null) return null;
            LuckRankingVO luckRankingVO = RankingCache[authId];
            if (luckRankingVO.CacheDate.AddMinutes(SiteConfig.RankingCacheMinutes) < DateTime.Now) return null;
            return luckRankingVO;
        }

        /// <summary>
        /// 将欧气排行放入缓存
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="luckRankingVO"></param>
        public static void SetLuckRankingCache(int authId, LuckRankingVO luckRankingVO)
        {
            luckRankingVO.CacheDate = DateTime.Now;
            RankingCache[authId] = luckRankingVO;
        }

    }
}
