using GenshinPray.Models;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Common
{
    public static class SiteConfig
    {
        /// <summary>
        /// 默认概率
        /// </summary>
        public static readonly decimal DefaultPR = 1;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString = "";

        /// <summary>
        /// 5星角色up
        /// </summary>
        public static List<YSGoodsItem> RoleStar5UpList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星角色up
        /// </summary>
        public static List<YSGoodsItem> RoleStar4UpList = new List<YSGoodsItem>();

        /// <summary>
        /// 5星角色非up
        /// </summary>
        public static List<YSGoodsItem> RoleStar5NonUpList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星角色非up
        /// </summary>
        public static List<YSGoodsItem> RoleStar4NonUpList = new List<YSGoodsItem>();


        /// <summary>
        /// 5星角色常驻列表
        /// </summary>
        public static readonly List<YSGoodsItem> RoleStar5PermList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(DefaultPR,YSGoodsType.雷,YSRareType.五星,"刻晴"),
            new YSGoodsItem(DefaultPR,YSGoodsType.水,YSRareType.五星,"莫娜"),
            new YSGoodsItem(DefaultPR,YSGoodsType.冰,YSRareType.五星,"七七"),
            new YSGoodsItem(DefaultPR,YSGoodsType.火,YSRareType.五星,"迪卢克"),
            new YSGoodsItem(DefaultPR,YSGoodsType.风,YSRareType.五星,"琴")
        };

        /// <summary>
        /// 4星角色常驻列表
        /// </summary>
        public static readonly List<YSGoodsItem> RoleStar4PermList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(DefaultPR,YSGoodsType.风,YSRareType.四星,"砂糖"),
            new YSGoodsItem(DefaultPR,YSGoodsType.雷,YSRareType.四星,"菲谢尔"),
            new YSGoodsItem(DefaultPR,YSGoodsType.水,YSRareType.四星,"芭芭拉"),
            new YSGoodsItem(DefaultPR,YSGoodsType.火,YSRareType.四星,"烟绯"),
            new YSGoodsItem(DefaultPR,YSGoodsType.冰,YSRareType.四星,"罗莎莉亚"),
            new YSGoodsItem(DefaultPR,YSGoodsType.火,YSRareType.四星,"辛焱"),
            new YSGoodsItem(DefaultPR,YSGoodsType.冰,YSRareType.四星,"迪奥娜"),
            new YSGoodsItem(DefaultPR,YSGoodsType.冰,YSRareType.四星,"重云"),
            new YSGoodsItem(DefaultPR,YSGoodsType.岩,YSRareType.四星,"诺艾尔"),
            new YSGoodsItem(DefaultPR,YSGoodsType.火,YSRareType.四星,"班尼特"),
            new YSGoodsItem(DefaultPR,YSGoodsType.岩,YSRareType.四星,"凝光"),
            new YSGoodsItem(DefaultPR,YSGoodsType.水,YSRareType.四星,"行秋"),
            new YSGoodsItem(DefaultPR,YSGoodsType.雷,YSRareType.四星,"北斗"),
            new YSGoodsItem(DefaultPR,YSGoodsType.火,YSRareType.四星,"香菱"),
            new YSGoodsItem(DefaultPR,YSGoodsType.雷,YSRareType.四星,"雷泽")
        };

        /// <summary>
        /// 4星武器常驻列表
        /// </summary>
        public static readonly List<YSGoodsItem> ArmStar4PermList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.四星,"弓藏"),
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.四星,"祭礼弓"),
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.四星,"绝弦"),
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.四星,"西风猎弓"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.四星,"昭心"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.四星,"祭礼残章"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.四星,"流浪乐章"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.四星,"西风秘典"),
            new YSGoodsItem(DefaultPR,YSGoodsType.长柄武器,YSRareType.四星,"西风长枪"),
            new YSGoodsItem(DefaultPR,YSGoodsType.长柄武器,YSRareType.四星,"匣里灭辰"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.四星,"雨裁"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.四星,"祭礼大剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.四星,"钟剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.四星,"西风大剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.四星,"匣里龙吟"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.四星,"祭礼剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.四星,"笛剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.四星,"西风剑")
        };

        /// <summary>
        /// 3星武器常驻列表
        /// </summary>
        public static readonly List<YSGoodsItem> ArmStar3PermList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.三星,"弹弓"),
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.三星,"神射手之誓"),
            new YSGoodsItem(DefaultPR,YSGoodsType.弓,YSRareType.三星,"鸦羽弓"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.三星,"翡玉法球"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.三星,"讨龙英杰谭"),
            new YSGoodsItem(DefaultPR,YSGoodsType.法器,YSRareType.三星,"魔导绪论"),
            new YSGoodsItem(DefaultPR,YSGoodsType.长柄武器,YSRareType.三星,"黑缨枪"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.三星,"以理服人"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.三星,"沐浴龙血的剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.双手剑,YSRareType.三星,"铁影阔剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.三星,"飞天御剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.三星,"黎明神剑"),
            new YSGoodsItem(DefaultPR,YSGoodsType.单手剑,YSRareType.三星,"冷刃")
        };


    }
}
