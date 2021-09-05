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
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult ApplyAuth(int userId, int type)
        {
            return Ok(ApiResult.Success(123));
        }

    }
}
