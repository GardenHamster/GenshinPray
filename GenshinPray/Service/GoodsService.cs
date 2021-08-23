using GenshinPray.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class GoodsService
    {
        private GoodsDao goodsDao;

        public GoodsService()
        {
            this.goodsDao = new GoodsDao();
        }

    }
}
