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
    public class CompanyProfileController : Controller
    {
        private CompanyProfileLogic _logic;
        private CareerCloudContext db = new CareerCloudContext();
        public CompanyProfileController()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        // GET: CompanyProfile
        public ActionResult Index()
        {
            return View(_logic.GetAll());
        }
        //here
        public ActionResult Location(Guid id)
        {
            return View();
        }
        public ActionResult Description(Guid id)
        {
            return View();
        }
        public ActionResult Jobs(Guid id)
        {
            return View();
        }


        // GET: CompanyProfile/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);
            if (companyProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Create
        

        // GET: CompanyProfile/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);
            if (companyProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(companyProfilePoco);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyWebsite,ContactPhone,ContactName,RegistrationDate,TimeStamp")] CompanyProfilePoco companyProfilePoco)
        {
            if (!ModelState.IsValid)
            {
                return View(companyProfilePoco);
            }
            
              try
                {
                    _logic.Update(new[] { companyProfilePoco });
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
                    return View(companyProfilePoco);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", $"Error: {e.Message}");
                    return View(companyProfilePoco);
                }
            }
            
        

            public ActionResult Delete(Guid id) { 
        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);
            if (companyProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(companyProfilePoco);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyProfilePoco companyProfilePoco = _logic.Get(id);
            try
            {
                _logic.Delete(new[] { companyProfilePoco });
                return RedirectToAction("Index");
            }
            catch(Exception e)
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
