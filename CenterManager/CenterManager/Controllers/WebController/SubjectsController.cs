using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CenterManager.Models;

namespace CenterManager.Controllers
{
    public class SubjectsController : Controller
    {
        private WebSubjectDAO dao = new WebSubjectDAO();

        // GET: WebSubject
        public ActionResult Index()
        {
            return View(dao.GetAllSubjects());
        }

        // GET: WebSubject/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = dao.GetSubjectByID(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: WebSubject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebSubject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,subject_id,name")] subject subject)
        {
            if (ModelState.IsValid)
            {
                dao.AddSubject(subject);
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: WebSubject/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = dao.GetSubjectByID(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: WebSubject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,subject_id,name")] subject subject)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();*/
                dao.UpdateSubject(subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: WebSubject/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool rs = dao.DeleteSubject(id);
            if (rs == false)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: WebSubject/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subject subject = db.subjects.Find(id);
            db.subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
