using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("member_goods")]
    public class MemberGoodsPO : BasePO
    {
        [SugarColumn(IsNullable = false, ColumnDescription = "授权码ID")]
        public int AuthId { get; set; }

        [SugarColumn(IsNullable = false, Length = 32, ColumnDescription = "成员编号")]
        public string MemberCode { get; set; }

        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "物品名称")]
        public string GoodsName { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品类型")]
        public YSGoodsType GoodsType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品子类型")]
        public YSGoodsSubType GoodsSubType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "稀有类型")]
        public YSRareType RareType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "添加日期")]
        public DateTime CreateDate { get; set; }
    }
}
