using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;

namespace OgrenciNotSistemiMVC.Controllers
{
    public class KulupController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities(); 
        public ActionResult Index()
        {
            var kulupler = db.TBL_KULUPLER.ToList();
            return View(kulupler);
        }
        [HttpGet]
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(TBL_KULUPLER p2)
        {
            if (ModelState.IsValid)
            {
                db.TBL_KULUPLER.Add(p2);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Kulüp Sisteme Başarıyla Eklendi!";
                return RedirectToAction("YeniKulup"); 
            }

            return View(p2); 



        }
    }
}