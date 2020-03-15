using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entity;
using General.Framework.Controllers;
using General.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class RoleController : BaseContoller
    {
        private readonly ISysRoleService sysRoleService;

        public RoleController(ISysRoleService service)
        {
            this.sysRoleService = service;
        }

        [HttpGet("getRoleList")]
        public IEnumerable<SysRole> getRoleList()
        {
            return sysRoleService.getAllRoles().ToArray();
        }
    }
}