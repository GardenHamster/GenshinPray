using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("authorize")]
    public class AuthorizePO : BasePO
    {
        [SugarColumn(IsNullable = false, Length = 16, ColumnDescription = "授权码")]
        public string Code { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "每日可调用次数")]
        public int DailyCall { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "添加时间")]
        public DateTime CreateDate { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "过期时间")]
        public DateTime ExpireDate { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0", ColumnDescription = "获得角色时使用皮肤展示的概率，0~100")]
        public int SkinRate { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "tinyint", ColumnDescription = "是否被禁用")]
        public bool IsDisable { get; set; }
    }

}
