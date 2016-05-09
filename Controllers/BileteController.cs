using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "User")]
    public class BileteController : Controller
    {
        private BileteDBContext db = new BileteDBContext();

        // GET: /Bilete/
        public ActionResult Index()
        {
            return View(db.Bilete.ToList());
        }

        // GET: /Bilete/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilete bilete = db.Bilete.Find(id);
            if (bilete == null)
            {
                return HttpNotFound();
            }
            return View(bilete);
        }

        // GET: /Bilete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Bilete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BileteiD,Spectacol,rand,numar")] Bilete bilete)
        {
            SpectacoleDBContext specdb = new SpectacoleDBContext();
            if (ModelState.IsValid)
            {
                string q = bilete.Spectacol;
                var spectacole1 = from m in specdb.Spectacole
                                 select m;

                var spectacole2 = from m in db.Bilete
                                  select m;

                var num = from m in specdb.Spectacole
                          where m.Titlu == bilete.Spectacol
                          select m.NumarBilete;

                int bileteramase = num.First();
                spectacole1 = spectacole1.Where(s => s.Titlu.Contains(q));
                spectacole2 = spectacole2.Where(s => s.Spectacol.Contains(q));

                int count = spectacole1.Count();
                int bileteVandute = spectacole2.Count();


                if (count != 0 && (bileteramase > bileteVandute))
                {
                    db.Bilete.Add(bilete);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return View(bilete);
        }

        // GET: /Bilete/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilete bilete = db.Bilete.Find(id);
            if (bilete == null)
            {
                return HttpNotFound();
            }
            return View(bilete);
        }

        // POST: /Bilete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BileteiD,Spectacol,rand,numar")] Bilete bilete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bilete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bilete);
        }

        // GET: /Bilete/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilete bilete = db.Bilete.Find(id);
            if (bilete == null)
            {
                return HttpNotFound();
            }
            return View(bilete);
        }

        // POST: /Bilete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bilete bilete = db.Bilete.Find(id);
            db.Bilete.Remove(bilete);
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
