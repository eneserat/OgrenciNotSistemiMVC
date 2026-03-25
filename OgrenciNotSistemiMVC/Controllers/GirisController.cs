using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Models.EntityFramework;

namespace OgrenciNotSistemiMVC.Controllers
{
    public class GirisController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
      
        public ActionResult Index()
        {
            var liste = db.TBL_OGRENCIGIRIS.ToList();
            return View(liste);
           
        }
        [HttpGet]
        public ActionResult OgrenciEkle()
        {
          
            ViewBag.Cinsiyetler = new List<SelectListItem>
        {
            new SelectListItem { Text = "Erkek", Value = "Erkek" },
            new SelectListItem { Text = "Kız", Value = "Kız" }
        };

           
            ViewBag.Kulupler = db.TBL_KULUPLER
                                 .AsEnumerable()
                                 .Select(x => new SelectListItem
                                 {
                                     Text = x.KULUPAD,
                                     Value = x.KULUPID.ToString()
                                 }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult OgrenciEkle(TBL_OGRENCILER p, HttpPostedFileBase OgrFoto)
        {
            if (OgrFoto != null)
            {
                var yol = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(yol)) Directory.CreateDirectory(yol);

                var dosyaAdi = Path.GetFileName(OgrFoto.FileName);
                var tamYol = Path.Combine(yol, dosyaAdi);
                OgrFoto.SaveAs(tamYol);

                p.OGRENCIFOTOGRAF = "/Uploads/" + dosyaAdi;
            }

            db.TBL_OGRENCILER.Add(p);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Öğrenci eklendi 🔥";
            return RedirectToAction("OgrenciEkle");
        }
    }
}
