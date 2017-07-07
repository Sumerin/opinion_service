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
                return View(ctx.Opinions.Include("User").ToList());
            }
        }


        // GET: Opinions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opinions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ip,Description")] Opinion opinion)
        {
            if(Session["User"]!=null)
            {
                User sessionUser = (User)Session["User"];
                using (var ctx = new MyDbContext())
                {
                    var user = from us in ctx.UserAccount
                               where us.UserId == sessionUser.UserId
                               select us;
                    ctx.Opinions.Add(opinion);
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(opinion);
        }
    }
}
