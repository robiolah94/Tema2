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
    [Authorize(Roles = "Admin")]
    public class SpectacoleController : Controller
    {
        public SpectacoleDBContext sdb = new SpectacoleDBContext();

        // GET: /Spectacole/
        public ActionResult Index()
        {
            return View(sdb.Spectacole.ToList());
        }

        // GET: /Spectacole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacole spectacole = sdb.Spectacole.Find(id);
            if (spectacole == null)
            {
                return HttpNotFound();
            }
            return View(spectacole);
        }

        // GET: /Spectacole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Spectacole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SpectacoleiD,Titlu,Regia,Distributia,Data,NumarBilete")] Spectacole spectacole)
        {
            if (ModelState.IsValid)
            {
                sdb.Spectacole.Add(spectacole);
                sdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spectacole);
        }

        // GET: /Spectacole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacole spectacole = sdb.Spectacole.Find(id);
            if (spectacole == null)
            {
                return HttpNotFound();
            }
            return View(spectacole);
        }

        // POST: /Spectacole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SpectacoleiD,Titlu,Regia,Distributia,Data,NumarBilete")] Spectacole spectacole)
        {
            if (ModelState.IsValid)
            {
                sdb.Entry(spectacole).State = EntityState.Modified;
                sdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spectacole);
        }

        // GET: /Spectacole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacole spectacole = sdb.Spectacole.Find(id);
            if (spectacole == null)
            {
                return HttpNotFound();
            }
            return View(spectacole);
        }

        // POST: /Spectacole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spectacole spectacole = sdb.Spectacole.Find(id);
            sdb.Spectacole.Remove(spectacole);
            sdb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
