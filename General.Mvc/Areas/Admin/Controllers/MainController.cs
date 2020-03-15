using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Framework.Controllers;
using General.Framework.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace General.Mvc.Areas.Admin.Controllers
{ 
    [Route("admin/main")]
    public class MainController : PublicController
    {
        private ILoginAuthService _adminAuthService;

        public MainController(ILoginAuthService adminAuthService)
        {
            this._adminAuthService = adminAuthService;
        }
         
        [Route("",Name ="index")]
        public IActionResult Index()
        {   
            return View();
        }

        [Route("out",Name ="signOut")]
        public IActionResult SignOut()
        {
            _adminAuthService.signOut();
            return RedirectToRoute("login");
        }

    }
}