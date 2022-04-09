using GenshinPray.Models;
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
        public List<YSGoodsItem> getByGoodsType(YSGoodsType goodsType)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.GoodsName,g.RareType,g.GoodsType,g.GoodsSubType from goods g");
            sqlBuilder.Append(" where g.GoodsType=@goodsType and g.isDisable=0");
            return Db.Ado.SqlQuery<YSGoodsItem>(sqlBuilder.ToString(), new { goodsType = goodsType});
        }

        public List<YSGoodsItem> getByPondType(int authId, YSPondType pondType)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.GoodsName,g.RareType,g.GoodsType,g.GoodsSubType,pg.PondIndex,pg.GoodsId from pond_goods pg");
            sqlBuilder.Append(" inner join goods g on g.id=pg.goodsId");
            sqlBuilder.Append(" where pg.AuthId=@authId and pg.PondType=@pondType and g.isDisable=0");
            return Db.Ado.SqlQuery<YSGoodsItem>(sqlBuilder.ToString(), new { authId = authId, pondType = pondType });
        }

        public List<YSGoodsItem> getByPondType(int authId, YSPondType pondType, int pondIndex)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.GoodsName,g.RareType,g.GoodsType,g.GoodsSubType,pg.PondIndex,pg.GoodsId from pond_goods pg");
            sqlBuilder.Append(" inner join goods g on g.id=pg.goodsId");
            sqlBuilder.Append(" where pg.AuthId=@authId and pg.PondType=@pondType and pg.PondIndex=@pondIndex and g.isDisable=0");
            return Db.Ado.SqlQuery<YSGoodsItem>(sqlBuilder.ToString(), new { authId = authId, pondType = pondType, pondIndex = pondIndex });
        }


        public List<MemberGoodsDTO> GetMemberGoods(int authId, string memberCode)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.GoodsName,count(g.GoodsName) Count,g.GoodsType,g.RareType from member_goods mg");
            sqlBuilder.Append(" inner join goods g on g.Id=mg.GoodsId");
            sqlBuilder.Append(" where mg.AuthId=@authId and mg.MemberCode=@memberCode");
            sqlBuilder.Append(" group by g.GoodsName,g.GoodsType,g.RareType");
            sqlBuilder.Append(" order by g.RareType desc,Count desc");
            return Db.Ado.SqlQuery<MemberGoodsDTO>(sqlBuilder.ToString(), new { authId = authId, memberCode = memberCode });
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
