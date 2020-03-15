using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Librs;
using General.Framework.Controllers;
using General.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseContoller
    {
        private readonly ISysUserService sysUserService;
        private readonly IAuthenticateService authenticateService;

        public LoginController(ISysUserService service, IAuthenticateService authenticate)
        {
            this.sysUserService = service;
            this.authenticateService = authenticate;
        }

        [AllowAnonymous,HttpPost]
        //[HttpPost, Route("api")]
        public ActionResult Login(string Account, string Password)
        {
            string r = EncryptorHelper.GetMD5(Guid.NewGuid().ToString());

            if (!ModelState.IsValid)
            {
                AjaxData.Message = "请输入用户账号和密码";
                return Json(AjaxData);
            }

            var result = sysUserService.validateUser(Account, Password, r,true);
            AjaxData.Status = result.Status;
            AjaxData.Message = result.Message;

            string token = "";
            if (result.Status)
            {
                authenticateService.IsAuthenticated(result.Token, out token);
                AjaxData.Data = token;
            }
            return Json(AjaxData);
        }
    }
}