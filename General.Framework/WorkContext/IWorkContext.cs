using General.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework
{
    public interface IWorkContext
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        SysUser CurrentUser { get; }

        /// <summary>
        /// 当前登录用户的菜单
        /// </summary>
        List<Category> Categories { get; }
    }
}
