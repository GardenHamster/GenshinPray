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
        public List<GoodsPO> getByPondType(int authId, int pondType)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.* from pond_goods pg");
            sqlBuilder.Append(" inner join goods g on g.id=pg.goodsId");
            sqlBuilder.Append(" where pg.PondType=@pondType and pg.AuthId=@authId and g.isDisable=0");
            return Db.Ado.SqlQuery<GoodsPO>(sqlBuilder.ToString(), new { authId = authId, pondType = pondType });
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
