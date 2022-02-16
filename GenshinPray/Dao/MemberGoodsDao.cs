using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class MemberGoodsDao : DbContext<MemberGoodsPO>
    {
        public int CountByMember(int authId, string memberCode, YSRareType rareType)
        {
            return Db.Queryable<MemberGoodsPO>().Where(o => o.AuthId == authId && o.MemberCode == memberCode && o.RareType == rareType).Count();
        }

        public int CountByMember(int authId, string memberCode, YSPondType pondType, YSRareType rareType)
        {
            return Db.Queryable<MemberGoodsPO>().Where(o => o.AuthId == authId && o.MemberCode == memberCode && o.PondType == pondType && o.RareType == rareType).Count();
        }

        public List<LuckRankingDTO> getLuckRanking(int authId, int top, YSRareType rareType, DateTime startDate, DateTime endDate)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select AuthId,MemberCode,RareType,RareCount,");
            sqlBuilder.Append(" TotalPrayTimes, RareCount/TotalPrayTimes as RareRate from(");
            sqlBuilder.Append("     select AuthId,MemberCode,@RareType as RareType,");
            sqlBuilder.Append("     SUM(if(RareType=@RareType,1,0)) AS RareCount, count(ID) as TotalPrayTimes from member_goods");
            sqlBuilder.Append(" 	where AuthId=@AuthId and CreateDate>=@StartDate and CreateDate<@EndDate");
            sqlBuilder.Append(" 	group by AuthId,MemberCode");
            sqlBuilder.Append(" ) temp where RareCount>0 order by rareRate desc limit @Top");
            return Db.Ado.SqlQuery<LuckRankingDTO>(sqlBuilder.ToString(), new { AuthId = authId, Top = top, RareType = (int)rareType, StartDate = startDate, EndDate = endDate });
        }


    }
}
