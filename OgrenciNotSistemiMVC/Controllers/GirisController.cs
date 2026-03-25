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
          
            return View();  
           
        }
        [HttpGet]
        public ActionResult OgrenciLogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult OgrenciLogin(string ogrNo, string sifre)
        {
            var ogrenci = db.TBL_OGRENCIGIRIS
                            .FirstOrDefault(x => x.OGRENCINO == ogrNo && x.SIFRE == sifre);

            if (ogrenci != null)
            {
                Session["OGRENCIID"] = ogrenci.OGRENCIID;
                Session["OGRENCIAD"] = ogrenci.OGRENCIAD;
                return RedirectToAction("Index", "Ogrenci"); 
            }

            ViewBag.Hata = "Öğrenci numarası veya şifre yanlış!";
            return View();
        }

        
        [HttpGet]
        public ActionResult SifreUnuttum()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult SifreUnuttum(string ogrNo)
        {
            var ogrenci = db.TBL_OGRENCIGIRIS
                            .FirstOrDefault(x => x.OGRENCINO == ogrNo);

            if (ogrenci != null)
            {
                TempData["Mesaj"] = $"Şifreniz: {ogrenci.SIFRE}";
                return RedirectToAction("OgrenciLogin");
            }

            ViewBag.Hata = "Girilen öğrenci numarası sistemde bulunamadı!";
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("OgrenciLogin");
        }
    }
}

