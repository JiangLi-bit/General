﻿using Microsoft.AspNetCore.Mvc;
using General.Framework.Controllers;

namespace General.Mvc.Controllers
{
    public class HomeController : BaseContoller
    { 
        public IActionResult Index()
        { 
            return View();
        }
         
        

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
