using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.Dto
{
    public class CustomUpDto
    {
        /// <summary>
        /// 5星up物品名称列表
        /// </summary>
        public List<string> Star5UpItems { get; set; }

        /// <summary>
        /// 4星up物品名称列表
        /// </summary>
        public List<string> Star4UpItems { get; set; }

    }
}
