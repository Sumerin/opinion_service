using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using opinion_Service.Models;

namespace opinion_Service.Controllers
{
    public class OpinionsController : Controller
    {

        // GET: Opinions
        public ActionResult Index()
        {
            using (var ctx = new MyDbContext())
            {
                return View(ctx.Opinions.Include("User").Include("Site").ToList());
            }
        }


        // GET: Opinions/Create
        public ActionResult Create()
        {
            ViewBag.Result = "";
            return View();
        }

        // POST: Opinions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Site, Description")]Opinion opinion)
        {
            if(Session["User"]!=null)
            {
                User sessionUser = (User)Session["User"];
                using (var ctx = new MyDbContext())
                {
                    var user = (from us in ctx.UserAccount
                                where us.UserId == sessionUser.UserId
                                select us).FirstOrDefault();

                    if(user==null)
                    {
                        ViewBag.Result = "Wrong UserName!!";
                        return View(opinion);
                    }

                    var site = (from si in ctx.Sites
                               where si.DomainName == opinion.Site.DomainName
                               select si).FirstOrDefault();

                    if (site == null)
                    {
                        ViewBag.Result = "Site does not exist in database!!";
                        return View(opinion);
                    }
                    opinion.User = user;
                    opinion.Site = site;

                    ctx.Opinions.Add(opinion);
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Result = "Login Before adding opinion";
            return RedirectToAction("Login","Account");
        }
    }
}
