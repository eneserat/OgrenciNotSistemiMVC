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
        public ActionResult ogrencıekle()
        {
            ViewBag.Ogrencıler = new SelectList(db.TBL_OGRENCILER, "OGRENCIID", "OGRENCIAD");

            return View();
        }

        [HttpPost]
       
        public ActionResult OgrenciEkle(TBL_OGRENCILER ogrenci, HttpPostedFileBase OgrFoto)
        {
            if (OgrFoto != null && OgrFoto.ContentLength > 0)
            {
                var uploadDir = Server.MapPath("~/Uploads/Ogrenciler/");
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

                var fileName = Path.GetFileName(OgrFoto.FileName);
                var path = Path.Combine(uploadDir, fileName);
                OgrFoto.SaveAs(path);
                ogrenci.OGRENCIFOTOGRAF = "/Uploads/Ogrenciler/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.TBL_OGRENCILER.Add(ogrenci);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Öğrenci Sisteme Başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }

           
            ViewBag.Dersler = new SelectList(db.TBL_DERSLER, "DERSID", "DERSAD");
            return View(ogrenci);
        }
    }
}