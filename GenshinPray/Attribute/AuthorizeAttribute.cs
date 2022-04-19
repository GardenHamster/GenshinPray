using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
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
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        private AuthorizeService authorizeService;
        private PrayRecordService prayRecordService;
        private bool PrayLimit = false;
        private bool PublicLimit = false;


        public AuthorizeAttribute(bool prayTimesLimit = false, bool publicLimit = false)
        {
            this.authorizeService = new AuthorizeService();
            this.prayRecordService = new PrayRecordService();
            this.PublicLimit = publicLimit;
            this.PrayLimit = prayTimesLimit;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string authCode = context.HttpContext.Request.Headers["authorzation"];
            if (string.IsNullOrWhiteSpace(authCode))
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(ApiResult.Unauthorized);
                return;
            }
            authCode = authCode.Trim();
            AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
            if (authorizePO == null || authorizePO.IsDisable || authorizePO.ExpireDate <= DateTime.Now)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(ApiResult.Unauthorized);
                return;
            }
            if (PublicLimit && authCode == SiteConfig.PublicAuthCode)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = new JsonResult(ApiResult.PermissionDenied);
                return;
            }
            int prayTimesToday = prayRecordService.GetPrayTimesToday(authorizePO.Id);
            if (PrayLimit && prayTimesToday >= authorizePO.DailyCall)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = new JsonResult(ApiResult.ApiMaximum);
                return;
            }
            context.ActionArguments["authorize"] = new AuthorizeDTO(authorizePO, prayTimesToday);
        }

    }
}
