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
    public class ArmPrayController : BasePrayController
    {

        /// <summary>
        /// 单抽武器祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> ArmPrayOne(long memberId)
        {
            return ApiResult<string>.Error("缺少相关素材");
        }

        /// <summary>
        /// 十连武器祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> ArmPrayTen(long memberId)
        {
            return ApiResult<string>.Error("缺少相关素材");
        }

    }
}
