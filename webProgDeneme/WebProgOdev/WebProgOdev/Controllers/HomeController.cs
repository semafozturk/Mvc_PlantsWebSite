using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgOdev.Models;

namespace WebProgOdev.Controllers
{
    
    public class HomeController : Controller
    {
        BitkilerEntities db = new BitkilerEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(Kullanicilar kullanici)
        {
            Kullanicilar kul = db.Kullanicilar.FirstOrDefault(x=> x.kullaniciAdi == kullanici.kullaniciAdi & x.sifre == kullanici.sifre);
            if (kul != null) return RedirectToAction("Index", "Home");
            else
            {
                return View(kullanici);
                ModelState.AddModelError("", "Hatalı Kullanıcı Adı veya Şifre!");

            }
            
        }
        public ActionResult Ekle(Kullanicilar kullanici)
        {
            db.Kullanicilar.Add(kullanici);
            db.SaveChanges();
           /* talep.Adminler = admin;
            talep.Aciliyetler = aciliyetler;
            talep.Kategoriler = kategoriler;
            talep.TalepTarihi = DateTime.Now;
            talep.TalepDurumu = "Yeni";
            talep.IsYuzde = 0;
            talep.IsDurumu = "Beklemede";
            talep.yanitId = null;
            db.AdminTalepler.Add(talep);
            int sonuc = db.SaveChanges();*/
            return View();
        }
    }
}