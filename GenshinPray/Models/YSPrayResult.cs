using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSPrayResult
    {
        public MemberPO MemberInfo { get; set; }

        public YSPrayRecord[] PrayRecords { get; set; }

        public YSPrayRecord[] SortPrayRecords { get; set; }

        public int Star5Cost { get; set; }

        public int Surplus10 { get; set; }

        public FileInfo ParyFileInfo { get; set; }

    }
}
