using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;

namespace CarrerCloudV2.Controllers
{
    public class ApplicantResumeController : Controller
    {
        private ApplicantResumeLogic _logic;
        private CareerCloudContext db = new CareerCloudContext();
        public ApplicantResumeController()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        // GET: ApplicantResume
        public ActionResult Index()
        {
           
            return View(_logic.GetAll());
        }

        public ActionResult NewResume(Guid id)
        {
            ApplicantResumePoco poco = new ApplicantResumePoco
            {
                Applicant = id
            };
            return View(poco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewResume([Bind(Include = "Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();
                applicantResumePoco.LastUpdated = DateTime.Now;
                _logic.Add(new[] { applicantResumePoco });
                return RedirectToAction("Index", "Manage");
            }
            return View(applicantResumePoco);
        }
        // GET: ApplicantResume/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResume.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login");
            return View();
        }
       

        // POST: ApplicantResume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();
                db.ApplicantResume.Add(applicantResumePoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResume.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // POST: ApplicantResume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantResumePoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResume.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantResumePoco);
        }

        // POST: ApplicantResume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantResumePoco applicantResumePoco = db.ApplicantResume.Find(id);
            db.ApplicantResume.Remove(applicantResumePoco);
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
