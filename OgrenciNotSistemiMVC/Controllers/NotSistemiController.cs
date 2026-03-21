using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;


namespace OgrenciNotSistemiMVC.Controllers
{
    public class NotSistemiController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var notlistesi = db.TBL_NOTLAR.ToList();
            return View(notlistesi);
        }
    }
}