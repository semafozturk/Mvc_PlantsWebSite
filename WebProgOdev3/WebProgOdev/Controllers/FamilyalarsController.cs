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
    public class FamilyalarsController : Controller
    {
        private BitkilerEntities db = new BitkilerEntities();

        // GET: Familyalars
        public ActionResult Index()
        {
            return View(db.Familyalar.ToList());
        }

        // GET: Familyalars/Details/5
        public ActionResult Details(int? id)
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

        // GET: Familyalars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Familyalars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "familyaId,familyaAdi,familyaKod")] Familyalar familyalar)
        {
            if (ModelState.IsValid)
            {
                db.Familyalar.Add(familyalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(familyalar);
        }

        // GET: Familyalars/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Familyalars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "familyaId,familyaAdi,familyaKod")] Familyalar familyalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(familyalar);
        }

        // GET: Familyalars/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Familyalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familyalar familyalar = db.Familyalar.Find(id);
            db.Familyalar.Remove(familyalar);
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
