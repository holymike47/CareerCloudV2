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
    public class ApplicantProfileController : Controller
    {
        private CompanyJobDescriptionLogic _cjdl;
        private ApplicantProfileLogic _logic;
        private CareerCloudContext db = new CareerCloudContext();
        public ApplicantProfileController()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            _cjdl = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        // GET: ApplicantProfile
        public ActionResult Index()
        {
            
            return View(_logic.GetAll());
        }
        //here

        public ActionResult Applications(Guid id)
        {
            IEnumerable<ApplicantJobApplicationPoco> jobs = _logic.Get(id).ApplicantJobApplications;
            List<CompanyJobDescriptionPoco> desc = new List<CompanyJobDescriptionPoco>();
            foreach(var job in jobs)
            {
                var des = _cjdl.Get(job.Job);
                if (des != null)
                    desc.Add(des);
            }
            return PartialView("_AppliedJobs",desc);
        }
   





        // GET: ApplicantProfile/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfile.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfile/Create
        public ActionResult Create()
        {
            ViewBag.Country = new SelectList(db.SystemCountryCode, "Code", "Name");
            return View();
        }

      
        
        // GET: ApplicantProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfile.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.SystemCountryCode, "Code", "Name", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode,TimeStamp")] ApplicantProfilePoco applicantProfilePoco)
        {
            ViewBag.Country = new SelectList(db.SystemCountryCode, "Code", "Name", applicantProfilePoco.Country);
            if (!ModelState.IsValid)
            {
                return View(applicantProfilePoco);
            }

            try
            {
                _logic.Update(new[] { applicantProfilePoco });
                return RedirectToAction("Index");
            }

            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ValidationException ve)
                    {
                        ModelState.AddModelError("", $"Error: {e.Message}");
                    }
                }
                return View(applicantProfilePoco);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Error: {e.Message}");
                return View(applicantProfilePoco);
            }
        }

        // GET: ApplicantProfile/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfile.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantProfilePoco applicantProfilePoco = _logic.Get(id);
            try
            {
                _logic.Delete(new[] { applicantProfilePoco });
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Error: Unable to delete the record");
                return View();
            }
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
