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
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;

namespace CarrerCloudV2.Controllers
{
    public class CompanyDescriptionController : Controller
    {
        private CompanyDescriptionLogic _logic;
        private CareerCloudContext db = new CareerCloudContext();
        public CompanyDescriptionController()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        // GET: CompanyDescriptionPoco
        public ActionResult Index()
        {
            var companyDescription = db.CompanyDescription.Include(c => c.CompanyProfile).Include(c => c.SystemLanguageCode);
            return View(companyDescription.ToList());
        }
        //here
        
        public ActionResult NewDescription(Guid id)
        {
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
            {
                Company = id,
                LanguageId = "CAN",
                CompanyName = " ",
                CompanyDescription = " "
            };

            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name");
            
            return View(poco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewDescription([Bind(Include = "Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
                _logic.Add(new[] { companyDescriptionPoco });
               
                return RedirectToAction("Index");
            }

            ViewBag.company = companyDescriptionPoco.Company;
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }


        // GET: CompanyDescriptionPoco/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescription.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite");
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name");
            return View();
        }

        // POST: CompanyDescriptionPoco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,LanguageId,CompanyName,CompanyDescription,TimeStamp")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
                db.CompanyDescription.Add(companyDescriptionPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescription.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPoco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,LanguageId,CompanyName,CompanyDescription,TimeStamp")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDescriptionPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCode, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPoco/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescription.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPoco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescription.Find(id);
            db.CompanyDescription.Remove(companyDescriptionPoco);
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
