using General.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.IService
{
    public interface ISysUserRoleService
    {
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        List<SysUserRole> getAll();

        /// <summary>
        /// 移除缓存
        /// </summary>
        void removeCache();
    }
}
