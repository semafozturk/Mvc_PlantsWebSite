using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgOdev.Models;

namespace WebProgOdev.Controllers
{
    
    public class AdminController : Controller
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
            if (kul==null)
            {
                ModelState.AddModelError("", "Hatalı Kullanıcı Adı veya Şifre!");
                return View(kullanici);
            }
            if (kul.yetki == 1) return RedirectToAction("Yonetim", "Admin");
            else if (kul.yetki == 0) return RedirectToAction("Index", "Home");
            else
            {
                return View(kullanici);
            }

            //if (admin.Yetki == 1)
            //{
            //    Session["OturumYetki"] = "1";
            //    string IsDurumu = null;
            //    string durum = "BekleyenIsler";
            //    return RedirectToAction("Index", "Admin", new { IsDurumu = durum });

            //}
            //else if (admin.Yetki == 0)
            //{
            //    Session["OturumYetki"] = "0";
            //    return RedirectToAction("Index", "Kullanici");
            //}


        }
        public ActionResult Yonetim()
        {
            return View();
        }
        public ActionResult KullaniciEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanicilar kullanici)
        {
            db.Kullanicilar.Add(kullanici);
            db.SaveChanges();
            return View();
        }
        
    }
}