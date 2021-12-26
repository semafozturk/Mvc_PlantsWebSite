using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProgOdev.Models;

namespace WebProgOdev.Controllers
{
    public class TurDetaysController : Controller
    {
        private BitkilerEntities db = new BitkilerEntities();

        // GET: TurDetays
        public ActionResult Index()
        {
            var turDetay = db.TurDetay.Include(t => t.Familyalar);
            return View(turDetay.ToList());
        }

        // GET: TurDetays/Details/5
        public ActionResult Details(int? id)
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

        // GET: TurDetays/Create
        public ActionResult Create()
        {
            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi");
            return View();
        }

        // POST: TurDetays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "turDetayId,familyaId,turAdi,turDetay,dikimYeri,toprak,buyumeHizi,resim")] TurDetay turDetay)
        {
            if (ModelState.IsValid)
            {
                db.TurDetay.Add(turDetay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi", turDetay.familyaId);
            return View(turDetay);
        }

        // GET: TurDetays/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TurDetays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "turDetayId,familyaId,turAdi,turDetay,dikimYeri,toprak,buyumeHizi,resim")] TurDetay turDetay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turDetay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.familyaId = new SelectList(db.Familyalar, "familyaId", "familyaAdi", turDetay.familyaId);
            return View(turDetay);
        }

        // GET: TurDetays/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TurDetays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TurDetay turDetay = db.TurDetay.Find(id);
            db.TurDetay.Remove(turDetay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
