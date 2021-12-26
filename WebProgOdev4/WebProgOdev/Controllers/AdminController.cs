using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        
        public ActionResult Yonetim()
        {
            return View();
        }
        public ActionResult KullaniciListe()
        {
            return View(db.Kullanicilar.ToList());
        }
        public ActionResult KullaniciEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciEkle(Kullanicilar kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Kullanicilar.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("KullaniciListe");
            }
            return View(kullanici);

        }
        public ActionResult FamilyaEkle()
        {
            return View();
        }

        // POST: Familyalars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FamilyaEkle(Familyalar familyalar)
        {
            if (ModelState.IsValid)
            {
                db.Familyalar.Add(familyalar);
                db.SaveChanges();
                return RedirectToAction("FamilyaListele");
            }

            return View(familyalar);
        }
        public ActionResult FamilyaListele()
        {
            return View(db.Familyalar.ToList());
        }
        public ActionResult FamilyaSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familyalar familyalar = db.Familyalar.Find(id);
            if (familyalar == null)
            {
                return HttpNotFound();
            }
            return View(familyalar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FamilyaSil(int id)
        {
            Familyalar familyalar = db.Familyalar.Find(id);
            db.Familyalar.Remove(familyalar);
            db.SaveChanges();
            return RedirectToAction("FamilyaListele");
        }
        public ActionResult FamilyaDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familyalar familyalar = db.Familyalar.Find(id);
            if (familyalar == null)
            {
                return HttpNotFound();
            }
            return View(familyalar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FamilyaDuzenle(Familyalar familyalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FamilyaListele");
            }
            return View(familyalar);
        }
        public ActionResult TurDetayListe()
        {
            var turDetay = db.TurDetay.Include(t => t.Familyalar);
            return View(turDetay.ToList());
        }
        public ActionResult TurDetayEkle()
        {
            List<SelectListItem> familyaList =
                (from Familyalar in db.Familyalar.ToList()
                 select new SelectListItem()
                 {
                     Text = Familyalar.familyaAdi,
                     Value = Familyalar.familyaId.ToString()
                 }
                 ).ToList();


            TempData["familyalar"] = familyaList;
            ViewBag.familyalar = familyaList;
            // ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TurDetayEkle(TurDetay turDetay)
        {
            Familyalar f = db.Familyalar.FirstOrDefault(fa => fa.familyaId==turDetay.Familyalar.familyaId);
            if (ModelState.IsValid)
            {
               turDetay.Familyalar = f;
                db.TurDetay.Add(turDetay);
                db.SaveChanges();
                return RedirectToAction("TurDetayListe");
            }

            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi", turDetay.familyaId);
            return View(turDetay);
        }

        public ActionResult TurDetayDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurDetay turDetay = db.TurDetay.Find(id);
            if (turDetay == null)
            {
                return HttpNotFound();
            }
            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi", turDetay.familyaId);
            return View(turDetay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TurDetayDuzenle(TurDetay turDetay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turDetay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TurDetayListe");
            }
            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi", turDetay.familyaId);
            return View(turDetay);
        }
        public ActionResult TurDetaySil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurDetay turDetay = db.TurDetay.Find(id);
            if (turDetay == null)
            {
                return HttpNotFound();
            }
            return View(turDetay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TurDetaySil(int id)
        {
            TurDetay turDetay = db.TurDetay.Find(id);
            db.TurDetay.Remove(turDetay);
            db.SaveChanges();
            return RedirectToAction("TurDetayListe");
        }
    }
}