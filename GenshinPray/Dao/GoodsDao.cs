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

        public List<GoodsPO> getByPondType(int pondType)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select g.* from pond_goods pg");
            sqlBuilder.Append(" inner join goods g on g.id=pg.goodsId");
            sqlBuilder.Append(" where pg.pondType=@pondType");
            return Db.Ado.SqlQuery<GoodsPO>(sqlBuilder.ToString(), new { pondType = pondType});
        }

        public List<GoodsPO> getPermGoods(YSGoodsType goodsType, YSRareType rareType)
        {
            return Db.Queryable<GoodsPO>().Where(o => o.GoodsType == goodsType && o.RareType == rareType && o.IsPerm == true).ToList();
        }


    }
}
