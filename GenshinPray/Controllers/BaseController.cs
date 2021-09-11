using GenshinPray.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    public class BaseController : ControllerBase
    {
        protected void checkNullParam(params string[] paramArr)
        {
            if (paramArr == null) return;
            foreach (var item in paramArr)
            {
                if (string.IsNullOrWhiteSpace(item)) throw new ParamException("参数错误");
            }
        }

    }
}
