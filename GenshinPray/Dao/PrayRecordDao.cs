using GenshinPray.Models.PO;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class PrayRecordDao : DbContext<PrayRecordPO>
    {
        public int getPrayTimesToday(int authId)
        {
            return Db.Queryable<PrayRecordPO>().Where(o => o.AuthId == authId && o.CreateDate >= DateTimeHelper.getTodayStart() && o.CreateDate < DateTimeHelper.getTodayEnd()).Count();
        }



    }
}
