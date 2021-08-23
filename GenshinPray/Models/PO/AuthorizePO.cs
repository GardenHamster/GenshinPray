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
        public string Code { get; set; }

        public int DailyCall { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool IsDisable { get; set; }
    }

}
