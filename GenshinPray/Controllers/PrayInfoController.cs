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

        /// <summary>
        /// 更新当前up池
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResult<string>> SetUpGoods()
        {
            return ApiResult<string>.Success("hello word");
        }

        /// <summary>
        /// 获取群员祈愿详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> GetMemberPrayDetail(string memberCode)
        {
            return ApiResult<string>.Success("hello word");
        }

        /// <summary>
        /// 获取群内抽卡统计排行
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> GetRankingByAuth(string authCode)
        {
            return ApiResult<string>.Success("hello word");
        }

    }
}
