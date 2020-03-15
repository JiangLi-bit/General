using General.Entity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Security
{
    public interface ILoginAuthService
    {
        /// <summary>
        /// 保存等状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        void signIn(string token, string name);

        /// <summary>
        /// 退出登录
        /// </summary>
        void signOut();

        /// <summary>
        /// 获取当前登录用户
        /// （缓存）
        /// </summary>
        /// <returns></returns>
        SysUser getCurrentUser();

        /// <summary>
        /// 获取我的权限数据
        /// </summary>
        /// <returns></returns>
        List<Category> getMyCategories();

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool authorize(ActionExecutingContext context);

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="routeName"></param> 
        /// <returns></returns>
        bool authorize(string routeName);
    }
}
