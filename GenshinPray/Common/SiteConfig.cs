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
        /// JWT密钥
        /// </summary>
        public static string JWTSecurityKey = "3014c672-066b-f45d-9bd4-aadf9111d308";

        /// <summary>
        /// JWT Issuer
        /// </summary>
        public static string JWTIssuer = "GardenHamster";

        /// <summary>
        /// JWT Audience
        /// </summary>
        public static string JWTAudience = "Audience";

        /// <summary>
        /// 默认概率
        /// </summary>
        public static readonly decimal DefaultPR = 1;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString = "";

        /// <summary>
        /// 祈愿结果图片保存目录
        /// </summary>
        public static string PrayImgSavePath = "";

        /// <summary>
        /// 祈愿素材保存路径
        /// </summary>
        public static string PrayMaterialSavePath = "";

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
        public static List<YSGoodsItem> RoleStar5PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星角色常驻列表
        /// </summary>
        public static List<YSGoodsItem> RoleStar4PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 4星武器常驻列表
        /// </summary>
        public static List<YSGoodsItem> ArmStar4PermList = new List<YSGoodsItem>();

        /// <summary>
        /// 3星武器常驻列表
        /// </summary>
        public static List<YSGoodsItem> ArmStar3PermList = new List<YSGoodsItem>();


    }
}
