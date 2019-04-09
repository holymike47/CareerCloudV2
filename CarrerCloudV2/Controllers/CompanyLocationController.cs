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
    public class CompanyLocationController : Controller
    {
        private CompanyLocationLogic _logic;
        private CareerCloudContext db = new CareerCloudContext();
        public CompanyLocationController()
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        // GET: CompanyLocation
        public ActionResult Index()
        {
            var companyLocation = db.CompanyLocation.Include(c => c.CompanyProfile);
            return View(companyLocation.ToList());
        }

        //here
        public ActionResult NewLocation(Guid id)
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Company = id
            };

            

            return View(poco);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewLocation([Bind(Include = "Company,CountryCode,Province,Street,City,PostalCode")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                companyLocationPoco.Id = Guid.NewGuid();
                _logic.Add(new[] { companyLocationPoco });
                return RedirectToAction("Index");
            }
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocation.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,CountryCode,Province,Street,City,PostalCode,TimeStamp")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                companyLocationPoco.Id = Guid.NewGuid();
                db.CompanyLocation.Add(companyLocationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocation.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // POST: CompanyLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,CountryCode,Province,Street,City,PostalCode,TimeStamp")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyLocationPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocation.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // POST: CompanyLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyLocationPoco companyLocationPoco = db.CompanyLocation.Find(id);
            db.CompanyLocation.Remove(companyLocationPoco);
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
