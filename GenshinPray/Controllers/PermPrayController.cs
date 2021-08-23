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
    public class PermPrayController
    {
        /// <summary>
        /// 单抽常祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> PermPrayOne(long memberId)
        {
            return ApiResult<string>.Error("缺少相关素材");
        }

        /// <summary>
        /// 十连常驻祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> PermPrayTen(long memberId)
        {
            return ApiResult<string>.Error("缺少相关素材");
        }

        


    }
}
