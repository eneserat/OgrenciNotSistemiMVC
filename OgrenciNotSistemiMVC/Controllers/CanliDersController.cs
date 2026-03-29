using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;

namespace OgrenciNotSistemiMVC.Controllers
{
    public class CanliDersController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CanliDers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DersEkle(TBL_ONLINE_DERSLER d)
        {
            db.TBL_ONLINE_DERSLER.Add(d);
            db.SaveChanges();
            return RedirectToAction("OnlineDersler");
        }

    }
}