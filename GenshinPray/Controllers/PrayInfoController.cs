using GenshinPray.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PrayInfoController
    {
        /// <summary>
        /// 获取当前所有祈愿池的up内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> GetPrayGoods()
        {
            return ApiResult<string>.Success("hello word");
        }

    }
}
