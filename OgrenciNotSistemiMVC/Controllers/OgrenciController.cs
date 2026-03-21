using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;
namespace OgrenciNotSistemiMVC.Controllers
{
    public class OgrenciController : Controller
    {
      DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrencılıstele = db.TBL_OGRENCILER.ToList();

            return View(ogrencılıstele);
        }
    }
}