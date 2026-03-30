using System;
using System.Linq;
using System.Web.Mvc;
using OgrenciNotSistemiMVC.Helpers;
using OgrenciNotSistemiMVC.Models.EntityFramework;

namespace OgrenciNotSistemiMVC.Controllers
{
    public class OgretmenGirisController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();

        [HttpGet]
        public ActionResult OgretmenLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgretmenLogin(string ogretmenAdi, string sifre)
        {
          
            var ogretmen = db.TBL_OGRETMENGIRIS
                             .FirstOrDefault(x => x.KullaniciAdi == ogretmenAdi && x.Sifre == sifre);

            if (ogretmen != null)
            {
                Session["OGRETMENID"] = ogretmen.ID;
                Session["OGRETMENISMI"] = ogretmen.KullaniciAdi;

                return RedirectToAction("Index", "Ogrenci");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        [HttpGet]
        public ActionResult SifreUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifreUnuttum(string ogretmenAdi)
        {
            var ogretmen = db.TBL_OGRETMENGIRIS
                             .FirstOrDefault(x => x.KullaniciAdi == ogretmenAdi);

            if (ogretmen != null)
            {
                TempData["Mesaj"] = $"Şifreniz: {ogretmen.Sifre}";
                return RedirectToAction("OgretmenLogin");
            }

            ViewBag.Hata = "Girilen kullanıcı adı sistemde bulunamadı!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("OgretmenLogin");
        }
    }
}