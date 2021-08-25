using GenshinPray.Models;
using GenshinPray.Models.VO;
using GenshinPray.Type;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenshinPray.Controllers
{
    public abstract class BasePrayController : ControllerBase
    {
        [HttpGet]
        public abstract ActionResult<ApiResult<PrayResult>> PrayOne(string authCode, string memberCode);

        [HttpGet]
        public abstract ActionResult<ApiResult<PrayResult>> PrayTen(string authCode, string memberCode);

    }
}
