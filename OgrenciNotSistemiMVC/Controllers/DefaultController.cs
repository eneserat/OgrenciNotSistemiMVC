using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;

namespace OgrenciNotSistemiMVC.Controllers
{
    public class DefaultController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
     
        public ActionResult Index()
        {
            var derslistele = db.TBL_DERSLER.ToList();
            return View(derslistele);
        }
    }
}