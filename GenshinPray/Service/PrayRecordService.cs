using GenshinPray.Dao;
using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class PrayRecordService
    {
        private PrayRecordDao prayRecordDao;

        public PrayRecordService()
        {
            this.prayRecordDao = new PrayRecordDao();
        }

        public int getPrayTimesToday(int authId)
        {
            return prayRecordDao.getPrayTimesToday(authId);
        }





    }
}
