using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RedNb.Core.Contracts;
using System.Web;
using Newtonsoft.Json;

namespace RedNb.Core.Extensions
{
    public static class HttpContextExtensions
    {
        public static LoginUserDto GetLoginUser(this IHttpContextAccessor httpContextAccessor)
        {
            var loginUser = new LoginUserDto();

            if (httpContextAccessor.HttpContext != null)
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;

                if (headers != null &&
                    headers.ContainsKey("LoginUser"))
                {
                    var json = HttpUtility.UrlDecode(headers["LoginUser"]);

                    loginUser = JsonConvert.DeserializeObject<LoginUserDto>(json);
                }
            }

            return loginUser;
        }

        public static string GetFlag(this IHttpContextAccessor httpContextAccessor)
        {
            var headers = httpContextAccessor.HttpContext.Request.Headers;

            if (headers.ContainsKey("Flag"))
            {
                return headers["Flag"];
            }

            return "admin";
        }
    }
}
