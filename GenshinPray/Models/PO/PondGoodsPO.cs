using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("pond_goods")]
    public class PondGoodsPO : BasePO
    {
        [SugarColumn(IsNullable = false, ColumnDescription = "授权码ID，0表示admin配置的默认蛋池，非0时表示该授权码的自定义蛋池")]
        public int AuthId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "蛋池类型")]
        public YSPondType PondType { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0", ColumnDescription = "蛋池编号,用于标识多限定卡池")]
        public int PondIndex { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品ID")]
        public int GoodsId { get; set; }

    }
}
