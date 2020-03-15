﻿using General.Core;
using General.Framework.Security;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace General.Framework
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 检测是否有权限
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public static bool OwnPermission(this IHtmlHelper helper, string routeName)
        {
           var _adminAuthSerivce = EnginContext.Current.Resolve<ILoginAuthService>();
            return _adminAuthSerivce.authorize(routeName);
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IWorkContext GetWorkContext(this IHtmlHelper helper)
        {
            return Core.EnginContext.Current.Resolve<IWorkContext>();
        }

        /// <summary>
        /// 必填项目信号提醒
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static HtmlString RequiredSpan(this IHtmlHelper htmlHelper)
        { 
            return new HtmlString(@"<span class=required-span>*</span>");
        }

    }
}