using General.Entity;
using System;
using System.Collections.Generic;

namespace General.IService
{
    public interface ISysRoleService
    {
        /// <summary>
        /// 获取所有的roles数据
        /// 并缓存
        /// </summary>
        /// <returns></returns>
        List<SysRole> getAllRoles();
         
        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysRole getRole(Guid id);

        /// <summary>
        /// 保存新增角色
        /// </summary>
        /// <param name="role"></param>
        void inserRole(SysRole role);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        void deleteRole(Guid roleId);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        void updateRole(SysRole role);

    }
}
