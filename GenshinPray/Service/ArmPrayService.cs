using GenshinPray.Models;
using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class ArmPrayService : BasePrayService
    {
        protected override YSPrayRecord GetActualItem(YSPrayRecord prayRecord, YSUpItem ySUpItem, int floor180Surplus, int floor20Surplus)
        {
            throw new NotImplementedException();
        }

        public override YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
        {
            throw new NotImplementedException();
        }
    }
}
