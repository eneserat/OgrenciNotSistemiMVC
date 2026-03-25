using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;
using PagedList;
namespace OgrenciNotSistemiMVC.Controllers
{
    public class OgrenciController : Controller
    {
      DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var ogrencılıstele = db.TBL_OGRENCILER.ToList().ToPagedList(sayfa, 10);

            return View(ogrencılıstele);
        }
        [HttpGet]
        public ActionResult ogrenciekle()
        {


            List<SelectListItem> items = (from i in db.TBL_KULUPLER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KULUPAD,
                                              Value = i.KULUPID.ToString()
                                          }).ToList();
            ViewBag.ıtms = items;
            return View();
                                          


        }

       

        [HttpPost]
       
        public ActionResult ogrenciekle(TBL_OGRENCILER p4)
        {
            var klp = db.TBL_KULUPLER.Where(m=>m.KULUPID==p4.TBL_KULUPLER.KULUPID).FirstOrDefault();
            p4.TBL_KULUPLER = klp;
            if(ModelState.IsValid)
            {
                db.TBL_OGRENCILER.Add(p4);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Öğrenci  Sisteme Başarıyla Eklendi!";
                return RedirectToAction("ogrenciekle");
            }

            return View(p4);

        }
    }
}