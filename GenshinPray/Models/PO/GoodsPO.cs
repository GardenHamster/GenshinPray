using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("goods")]
    public class GoodsPO : BasePO
    {
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "物品名称")]
        public string GoodsName { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品类型")]
        public YSGoodsType GoodsType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品子类型")]
        public YSGoodsSubType GoodsSubType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "稀有类型")]
        public YSRareType RareType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "tinyint", ColumnDescription = "是否常驻")]
        public bool IsPerm { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "添加日期")]
        public DateTime CreateDate { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "tinyint", ColumnDescription = "是否被禁用，缺少相关素材时请将此值设为1")]
        public bool IsDisable { get; set; }

        public GoodsPO() { }

        public GoodsPO(string goodsName, YSGoodsType goodsType, YSGoodsSubType goodsSubType, YSRareType rareType, bool isPerm)
        {
            this.GoodsName = goodsName;
            this.GoodsType = goodsType;
            this.GoodsSubType = goodsSubType;
            this.RareType = rareType;
            this.IsPerm = isPerm;
            this.CreateDate = DateTime.Now;
        }


    }
}
