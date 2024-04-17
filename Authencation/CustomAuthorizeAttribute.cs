using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CF_HOATUOIBASANH.Authencation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string Roles { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var serializedAccount = session.GetString("LoggedInAccount");
            var roleName = session.GetString("RoleName");

            if (string.IsNullOrEmpty(serializedAccount))
            {
                HandleUnauthorizedRequest(context);
                return;
            }

            var account = JsonSerializer.Deserialize<Account>(serializedAccount);

            if (account == null)
            {
                HandleUnauthorizedRequest(context);
                return;
            }

            var isValid = false;
            CustomPrincipal mp = new CustomPrincipal(account, roleName);
            if (Roles != null)
            {
                foreach (var item in Roles.Split(';'))
                {
                    if (mp.IsInRole(item))
                    {
                        isValid = true;

                        break;
                    }
                }
            }

            if (isValid)
            {
                return;
            }
            if (!isValid)
            {
                {
                    HandleUnauthorizedRequest(context);
                    return;
                }
            }

            if (IsAjaxRequest(context.HttpContext.Request))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            else
            {
                context.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { Controller = "Home", action = "Index" }));
                return;
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationFilterContext context)
        {
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Home", controller = "Account", action = "Login" }));
            }
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
