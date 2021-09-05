using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GenshinPray.Attribute
{
    public class AuthCodeAttribute : ActionFilterAttribute
    {
        private AuthorizeService authorizeService;

        public AuthCodeAttribute()
        {
            this.authorizeService = new AuthorizeService();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authorzation = context.HttpContext.Request.Headers["authorzation"];
            if (string.IsNullOrWhiteSpace(authorzation))
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(ApiResult.Unauthorized);
                return;
            }
            AuthorizePO authorizePO = authorizeService.GetAuthorize(authorzation);
            if (authorizePO == null || authorizePO.IsDisable || authorizePO.ExpireDate <= DateTime.Now)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(ApiResult.Unauthorized);
                return;
            }
        }





    }
}
