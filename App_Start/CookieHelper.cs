using Microsoft.AspNetCore.Http;
using System;

namespace KyNiem50NamWeb.App_Start
{
    public static class CookieHelper
    {
        public static void Create(HttpContext context, string name, string value, DateTime expire)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = expire,
                HttpOnly = true,  // Secure against XSS attacks
                Secure = true     // Use only over HTTPS
            };

            context.Response.Cookies.Append(name, value, options);
        }

        public static string Get(HttpContext context, string name)
        {
            context.Request.Cookies.TryGetValue(name, out string value);
            return value;
        }

        public static void Delete(HttpContext context, string name)
        {
            context.Response.Cookies.Delete(name);
        }
    }
}
