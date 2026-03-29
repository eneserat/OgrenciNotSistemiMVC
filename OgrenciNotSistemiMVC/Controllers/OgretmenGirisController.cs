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
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OgretmenLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string ogretmenAdi, string sifre)
        {

            string hashedPassword = PasswordHelper.HashPassword(sifre); 

            var ogretmen = db.TBL_OGRETMENGIRIS
                             .FirstOrDefault(x => x.KullaniciAdi == ogretmenAdi && x.Sifre == hashedPassword);

            if (ogretmen != null)
            {
                Session["ID"] = ogretmen.ID;
                Session["KullaniciAdi"] = ogretmen.KullaniciAdi;
                return RedirectToAction("Login", "OgretmenGiris");
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