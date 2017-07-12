using System;
using System.Linq;
using System.Web.Mvc;
using opinion_Service.Models;
using opinion_Service.Services;


namespace opinion_Service.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Logged");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]User user)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new MyDbContext())
                {
                    var dbUser = (from usr in ctx.UserAccount
                                  where usr.Username.Equals(user.Username)
                                  select usr).FirstOrDefault();

                    if (dbUser != null && Security.DecrypteAndCheck(user.Password, dbUser.Salt, dbUser.Password))
                    {


                        Session["User"] = dbUser;
                        return RedirectToAction("Logged");

                    }
                    else
                    {
                        ViewBag.Result = "Login Failed";
                        return View(user);
                    }
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            using (var ctx = new MyDbContext())
            {
                var usr = (from dbUser in ctx.UserAccount
                           where user.Username.Equals(dbUser.Username)
                           select dbUser).FirstOrDefault();

                if (usr == null)
                {
                    String saltText;
                    String saltedHashedPassword = Security.Encrypte(user.Password, out saltText);

                    var securedUser = new User()
                    {
                        Username = user.Username,
                        Salt = saltText,
                        Password = saltedHashedPassword
                    };

                    ctx.UserAccount.Add(securedUser);
                    ctx.SaveChanges();
                    ViewBag.Result = "Registration Completed";
                }
                else
                {
                    ViewBag.Result = "Failed Username already taken!!";
                }
            }
            return View();
        }
        public ActionResult Logged()
        {
            User loggedUser = Session["User"] as User;
            if (loggedUser != null)
            {
                ViewBag.Result = loggedUser.Username;
                return View();
            }
            return HttpNotFound();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login");
        }
    }
}