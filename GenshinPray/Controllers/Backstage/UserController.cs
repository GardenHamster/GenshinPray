using GenshinPray.Common;
using GenshinPray.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenshinPray.Controllers.Backstage
{
    [ApiController]
    [Route("backstage/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ApiResult Login(string userName, string userPwd)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPwd))
            {
                return ApiResult.Error("账号或密码错误");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, userName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SiteConfig.JWTSecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: SiteConfig.JWTIssuer,
                audience: SiteConfig.JWTAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return ApiResult.Success(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost]
        public ApiResult Register()
        {
            return ApiResult.Success("ok");
        }


    }

}
