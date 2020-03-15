using General.Core;
using General.Framework.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace General.Framework.Filters
{
    /// <summary>
    /// 登录状态过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthFilter : Attribute, IResourceFilter
    { 
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
           var _adminAuthService = EnginContext.Current.Resolve<ILoginAuthService>();
            var user = _adminAuthService.getCurrentUser();
            if (user == null || !user.Enabled)
                context.Result = new RedirectToRouteResult("adminLogin", new { returnUrl = context.HttpContext.Request.Path });
        }
    }
}
