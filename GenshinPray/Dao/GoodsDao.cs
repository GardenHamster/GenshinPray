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
    public class GoodsDao: DbContext<GoodsPO>
    {
        public List<GoodsPO> getByPondType(int authId, int pondType, int pool = 0)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select * from pond_goods pg");
            sqlBuilder.Append(" inner join goods g on g.id=pg.goodsId");
            sqlBuilder.Append(" where pg.PondType=@pondType and pg.AuthId=@authId and pg.Pool=@pool and g.isDisable=0");
            return Db.Ado.SqlQuery<GoodsPO>(sqlBuilder.ToString(), new { authId = authId, pondType = pondType, pool = pool });
        }

        public List<MemberGoodsDTO> GetMemberGoods(int authId, string memberCode)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select GoodsName,count(GoodsName) as Count,GoodsType,RareType from member_goods");
            sqlBuilder.Append(" where AuthId=@authId and MemberCode=@memberCode");
            sqlBuilder.Append(" group by GoodsName,GoodsType,RareType");
            sqlBuilder.Append(" order by RareType desc,count desc");
            return Db.Ado.SqlQuery<MemberGoodsDTO>(sqlBuilder.ToString(), new { authId = authId, memberCode = memberCode });
        }

        public List<MemberGoodsPO> GetMemberGoodsAll(int authId, string memberCode)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select * from member_goods");
            sqlBuilder.Append(" where AuthId=@authId and MemberCode=@memberCode");
            return Db.Ado.SqlQuery<MemberGoodsPO>(sqlBuilder.ToString(), new { authId = authId, memberCode = memberCode });
        }

        public List<GoodsPO> getPermGoods(YSGoodsType goodsType, YSRareType rareType)
        {
            return Db.Queryable<GoodsPO>().Where(o => o.GoodsType == goodsType && o.RareType == rareType && o.IsPerm == true && o.IsDisable == false).ToList();
        }

        public GoodsPO getByGoodsName(string goodsName)
        {
            return Db.Queryable<GoodsPO>().Where(o => o.GoodsName == goodsName && o.IsDisable == false).First();
        }


    }
}
