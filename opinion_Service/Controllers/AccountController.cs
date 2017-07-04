using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Security;
using opinion_Service.Models;

namespace opinion_Service.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            ViewBag.Message = user.Username;
            return View();
        }
       
    }
}