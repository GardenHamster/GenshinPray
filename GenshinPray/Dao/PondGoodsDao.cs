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
    public class PondGoodsDao : DbContext<PondGoodsPO>
    {
        public int clearPondGoods(int authId, YSPondType pondType, int pondIndex)
        {
            return Db.Deleteable<PondGoodsPO>().Where(o => o.AuthId == authId && o.PondType == pondType && o.PondIndex == pondIndex).ExecuteCommand();
        }

        public int clearPondGoods(int authId, YSPondType pondType)
        {
            return Db.Deleteable<PondGoodsPO>().Where(o => o.AuthId == authId && o.PondType == pondType).ExecuteCommand();
        }

    }
}
