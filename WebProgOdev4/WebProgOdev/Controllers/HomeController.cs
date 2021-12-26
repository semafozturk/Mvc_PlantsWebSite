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
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult UyeOl()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UyeOl(Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                kullanicilar.yetki = 0;
                db.Kullanicilar.Add(kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanicilar);
        }
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(Kullanicilar kullanici)
        {
            Kullanicilar kul = db.Kullanicilar.FirstOrDefault(x => x.kullaniciAdi == kullanici.kullaniciAdi & x.sifre == kullanici.sifre);
            if (kul == null)
            {
                ModelState.AddModelError("", "Hatalı Kullanıcı Adı veya Şifre!");
                return View(kullanici);
            }
            if (kul.yetki == 1)
            {
                Session["OturumYetki"] = "1";
                return RedirectToAction("Yonetim", "Admin");
            }
            else if (kul.yetki == 0)
            {
                Session["OturumYetki"] = "0";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(kullanici);
            }

        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Giris", "Home");
        }
        public ActionResult Kesfet()
        {
            return View(db.Familyalar.ToList());
        }
        public ActionResult KesifDetay(string kesifKodu)
        {
            //Session["kesif"] = kesifKodu;
           // if (Session["kesif"] == null) Session["kesif"] = "sukulent";
            List<TurDetay> fml = db.TurDetay.Where(x => x.Familyalar.familyaKod == kesifKodu).ToList();
            
            return View(fml);
        }
        public ActionResult ResimGoster(int resimID)
        {
            var resim = db.TurDetay.FirstOrDefault(x => x.turDetayId == resimID);
            return View(resim);
        }
    }
}