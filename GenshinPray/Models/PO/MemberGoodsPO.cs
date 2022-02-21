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

        [SugarColumn(IsNullable = false, ColumnDescription = "物品ID")]
        public int GoodsId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "来源蛋池")]
        public YSPondType PondType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "累计消耗N抽")]
        public int Cost { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "获得日期")]
        public DateTime CreateDate { get; set; }

    }
}
