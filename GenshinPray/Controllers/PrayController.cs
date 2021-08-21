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
    public class PrayController : ControllerBase
    {
        /// <summary>
        /// hello word
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> Test()
        {
            return ApiResult<string>.Success("hello word");
        }


    }

}
