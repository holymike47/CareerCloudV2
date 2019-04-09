﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CarrerCloudV2.Controllers
{
    public class ApplicantEducationController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantEducation
        public ActionResult Index()
        {
            var applicantEducation = db.ApplicantEducation.Include(a => a.ApplicantProfile);
            return View(applicantEducation.ToList());
        }

        public ActionResult NewEducation(Guid id)
        {
            ApplicantEducationPoco poco = new ApplicantEducationPoco
            {
                Applicant = id
            };
            return View(poco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewEducation([Bind(Include = "Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantEducationPoco.Id = Guid.NewGuid();
                db.ApplicantEducation.Add(applicantEducationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducation.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login");
            return View();
        }

        // POST: ApplicantEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantEducationPoco.Id = Guid.NewGuid();
                db.ApplicantEducation.Add(applicantEducationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducation.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantEducationPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfile, "Id", "Login", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducation.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducation.Find(id);
            db.ApplicantEducation.Remove(applicantEducationPoco);
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
