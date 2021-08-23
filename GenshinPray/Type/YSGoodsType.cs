using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Type
{
    public enum YSGoodsType
    {
        其他 = 0, 
        火 = 1, 水 = 2, 风 = 3, 雷 = 4, 草 = 5, 冰 = 6, 岩 = 7,
        单手剑 = 8, 双手剑 = 9, 长柄武器 = 10, 法器 = 11, 弓 = 12
    }

    public static class YSGoodsTypes
    {
        public static bool isRole(this YSGoodsType goodsType)
        {
            return goodsType == YSGoodsType.火
                || goodsType == YSGoodsType.水
                || goodsType == YSGoodsType.风
                || goodsType == YSGoodsType.雷
                || goodsType == YSGoodsType.草
                || goodsType == YSGoodsType.冰
                || goodsType == YSGoodsType.岩;
        }

        public static bool isArm(this YSGoodsType goodsType)
        {
            return goodsType == YSGoodsType.单手剑
                || goodsType == YSGoodsType.双手剑
                || goodsType == YSGoodsType.长柄武器
                || goodsType == YSGoodsType.法器
                || goodsType == YSGoodsType.弓;
        }
    }

}
