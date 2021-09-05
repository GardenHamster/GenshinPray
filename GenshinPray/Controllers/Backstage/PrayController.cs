using GenshinPray.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers.Backstage
{
    [ApiController]
    [Route("backstage/[controller]/[action]")]
    public class PrayController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult PondInfo(string authCode)
        {
            return Ok(ApiResult.Success(""));
        }

    }
}
