using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class MemberDao : DbContext<MemberPO>
    {
        public MemberPO getMember(int authId, string memberCode)
        {
            return Db.Queryable<MemberPO>().Where(o => o.AuthId == authId && o.MemberCode == memberCode).First();
        }

    }



}
