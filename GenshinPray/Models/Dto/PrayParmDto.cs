using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.Dto
{
    public class PrayParmDto
    {
        /// <summary>
        /// 自定义图片宽度,默认为1920*1080
        /// </summary>
        [DefaultValue(1920)]
        public int ImgWidth { get; set; } = 1920;

        /// <summary>
        /// 是否返回base64格式图片字符串,默认为false
        /// </summary>
        [DefaultValue(false)]
        public bool ImgToBase64 { get; set; } = false;

        /// <summary>
        /// 自定义卡池信息
        /// </summary>
        public CustomUpDto CustomUp { get; set; }

    }
}
