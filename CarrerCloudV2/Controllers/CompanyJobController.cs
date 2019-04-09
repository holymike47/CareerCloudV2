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
using CareerCloud.DataAccessLayer;
using CareerCloud.BusinessLogicLayer;
using CarrerCloudV2.Models;
using Microsoft.AspNet.Identity;

namespace CarrerCloudV2.Controllers
{
    public class CompanyJobController : Controller
    {
        private CompanyProfileLogic _cpl;
        private CompanyJobLogic _cjl;
        private CompanyJobDescriptionLogic _cjdl;
        private CompanyJobSkillLogic _cjsl;
        private CompanyJobEducationLogic _cjel;
       
        

        private CareerCloudContext db = new CareerCloudContext();
        public CompanyJobController()
        {
            _cpl = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
            _cjl = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            _cjdl = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
            _cjsl = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
            _cjel = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        // GET: CompanyJob
        public ActionResult Index()
        {
            List<CompanyJobDescriptionPoco> pocos = _cjdl.GetAll();

            return View(pocos);
        }
        [Authorize]
        public ActionResult Apply(Guid id)
        {
            var userId = User.Identity.GetUserId();
            CompanyJobPoco cjp = _cjl.Get(id);
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco
            {
                Id = Guid.NewGuid(),
                Job = cjp.Id,
                Applicant = Guid.Parse(userId),
                ApplicationDate=DateTime.Now
            };
            
            db.ApplicantJobApplication.Add(poco);
            db.SaveChanges();
            return View();
        }
        //here
        public ActionResult PostedJobs(Guid id)
        {
            TempData["id"] = id;
            IEnumerable<CompanyJobPoco> jobs = _cjl.GetAll().Where(c => c.Company == id);
            List<CompanyJobDescriptionPoco> pocos = new List<CompanyJobDescriptionPoco>();
            foreach (var j in jobs)
            {
                var jd = _cjdl.Get(j.Id);
                if(jd!=null)
                pocos.Add(jd);
            }
            
            return View(pocos);
        }
        public ActionResult PostJob(Guid id)
        {
            TempData["pid"] = id;
            CompanyJobViewModel cj = new CompanyJobViewModel()
            {
                Company = id,
                
                
            };
            return View(cj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(CompanyJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid job = Guid.NewGuid();
                model.Id=job;
                CompanyJobPoco cjp = new CompanyJobPoco
                {
                    Id = job,
                    Company = model.Company,
                    ProfileCreated = DateTime.Now,
                    IsCompanyHidden = false,
                    IsInactive = false
                };
                _cjl.Add(new[] { cjp });
                CompanyJobDescriptionPoco cjd = new CompanyJobDescriptionPoco
                {
                    Id = job,
                    Job = job,
                    JobName = model.Title,
                    JobDescriptions = model.Description
                };
                _cjdl.Add(new[] { cjd });
                
                string[] skills = {model.Skill1, model.Skill2, model.Skill3, model.Skill4, model.Skill5 };
                int[] skillLevel = {model.SkillLevel1, model.SkillLevel2, model.SkillLevel3, model.SkillLevel4, model.SkillLevel5 };
                int[] importance = {model.Importance1, model.Importance2, model.Importance3, model.Importance4, model.Importance5 };
                CompanyJobSkillPoco cjs;
               for(int i = 0; i < skills.Length; i++)
                {
                        cjs = new CompanyJobSkillPoco
                        {
                            Id = Guid.NewGuid(),
                            Job = job,
                            Skill = skills[i]?? " ",
                            SkillLevel = skillLevel[i],
                            Importance = importance[i]
                        };
                        _cjsl.Add(new[] { cjs });
                        
                    
                      
                }
                string[] majors = {model.Major1, model.Major2, model.Major3, model.Major4, model.Major5 };
                int[] majorImportance = {model.MajorImportance1, model.MajorImportance2,
                    model.MajorImportance3, model.MajorImportance4,model.MajorImportance5};
                CompanyJobEducationPoco cje;
                for(int i = 0; i < majors.Length; i++)
                {
                    
                        cje = new CompanyJobEducationPoco
                        {
                            Id = Guid.NewGuid(),
                            Job=job,
                            Major = majors[i]?? " ",
                            Importance = majorImportance[i]
                        };
                        _cjel.Add(new[] { cje });
                    
                    
                }

                return RedirectToAction("PostedJobs", new { id = model.Company });
            }

            
            return View(model);
        }

        // GET: CompanyJob/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            CompanyJobPoco cjp = _cjl.Get(id);
            Guid cid = cjp.CompanyProfile.Id;
            CompanyJobDescriptionPoco cjdp = _cjdl.Get(id);
            CompanyJobSkillPoco[]  cjsp = _cjsl.GetAll().OrderByDescending(c=>c.Skill).Where(c=>c.Job==id).ToArray();
            CompanyJobEducationPoco[]  cjep = _cjel.GetAll().OrderByDescending(c=>c.Major).Where(c => c.Job == id).ToArray();


            if (cjp == null|| cjdp==null)
            {
                return HttpNotFound();
            }

            CompanyJobViewModel model = new CompanyJobViewModel
            {
                Id = cjp.Id,
                Company=cjp.Company,
                Title = cjdp.JobName,
                Description=cjdp.JobDescriptions
                
            };
            TempData["ts"] = cjp.TimeStamp; TempData["pd"] = cjp.ProfileCreated;
            model.DescId = cjdp.Id;
            TempData["tsd"] = cjdp.TimeStamp;
            // skills assignment
            model.Skill1 = cjsp[0].Skill; TempData["s1"] = cjsp[0].Id;TempData["ts1"] = cjsp[0].TimeStamp;
            model.Skill2 = cjsp[1].Skill; TempData["s2"] = cjsp[1].Id; TempData["ts2"] = cjsp[1].TimeStamp;
            model.Skill3 = cjsp[2].Skill; TempData["s3"] = cjsp[2].Id; TempData["ts3"] = cjsp[2].TimeStamp;
            model.Skill4 = cjsp[3].Skill; TempData["s4"] = cjsp[3].Id; TempData["ts4"] = cjsp[3].TimeStamp;
            model.Skill5 = cjsp[4].Skill; TempData["s5"] = cjsp[4].Id; TempData["ts5"] = cjsp[4].TimeStamp;


            model.SkillLevel1 = cjsp[0].SkillLevel;
            model.SkillLevel2 = cjsp[1].SkillLevel;
            model.SkillLevel3 = cjsp[2].SkillLevel;
            model.SkillLevel4 = cjsp[3].SkillLevel;
            model.SkillLevel5 = cjsp[4].SkillLevel;

            model.Importance1 = cjsp[0].Importance;
            model.Importance2 = cjsp[1].Importance;
            model.Importance3 = cjsp[2].Importance;
            model.Importance4 = cjsp[3].Importance;
            model.Importance5 = cjsp[4].Importance;
           
            //education
            model.Major1 = cjep[0].Major; TempData["m1"] = cjep[0].Id; TempData["tm1"] = cjep[0].TimeStamp;
            model.Major2 = cjep[1].Major; TempData["m2"] = cjep[1].Id; TempData["tm2"] = cjep[1].TimeStamp;
            model.Major3 = cjep[2].Major; TempData["m3"] = cjep[2].Id; TempData["tm3"] = cjep[2].TimeStamp;
            model.Major4 = cjep[3].Major; TempData["m4"] = cjep[3].Id; TempData["tm4"] = cjep[3].TimeStamp;
            model.Major5 = cjep[4].Major; TempData["m5"] = cjep[4].Id; TempData["tm5"] = cjep[4].TimeStamp;

            model.MajorImportance1 = cjep[0].Importance;
            model.MajorImportance2 = cjep[1].Importance;
            model.MajorImportance3 = cjep[2].Importance;
            model.MajorImportance4 = cjep[3].Importance;
            model.MajorImportance5 = cjep[4].Importance;
            
            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid job = model.Id;
                    CompanyJobPoco cjp = new CompanyJobPoco
                    {
                        Id = job,
                        Company = model.Company,
                        TimeStamp = (byte[])TempData["ts"],
                        ProfileCreated=(DateTime)TempData["pd"]
                    };
                    _cjl.Update(new[] { cjp });
                    CompanyJobDescriptionPoco cjd = new CompanyJobDescriptionPoco
                    {
                        Id = model.DescId,
                        Job = job,
                        JobName = model.Title,
                        JobDescriptions = model.Description,
                        TimeStamp = (byte[])TempData["tsd"]
                        
                    };
                    _cjdl.Update(new[] { cjd });
                  
                    List<CompanyJobSkillPoco> cjsp = new List<CompanyJobSkillPoco>
                    {
                        new CompanyJobSkillPoco{Id=(Guid)TempData["s1"],TimeStamp=(byte[])TempData["ts1"],
                        Job = job,Skill=model.Skill1,SkillLevel=model.SkillLevel1,Importance=model.Importance1},

                        new CompanyJobSkillPoco{Id=(Guid)TempData["s2"],TimeStamp=(byte[])TempData["ts2"],
                        Job = job,Skill=model.Skill2,SkillLevel=model.SkillLevel2,Importance=model.Importance2},

                        new CompanyJobSkillPoco{Id=(Guid)TempData["s3"],TimeStamp=(byte[])TempData["ts3"],
                        Job = job,Skill=model.Skill3,SkillLevel=model.SkillLevel3,Importance=model.Importance3},

                        new CompanyJobSkillPoco{Id=(Guid)TempData["s4"],TimeStamp=(byte[])TempData["ts4"],
                        Job = job,Skill=model.Skill4,SkillLevel=model.SkillLevel4,Importance=model.Importance4},

                        new CompanyJobSkillPoco{Id=(Guid)TempData["s5"],TimeStamp=(byte[])TempData["ts5"],
                        Job = job,Skill=model.Skill5,SkillLevel=model.SkillLevel5,Importance=model.Importance5},
                    };
                    
                            _cjsl.Update(cjsp.ToArray());

                    List<CompanyJobEducationPoco> cjep = new List<CompanyJobEducationPoco>
                    {
                        new CompanyJobEducationPoco{Id=(Guid)TempData["m1"],Job=job,Major=model.Major1,
                        Importance=model.Importance1,TimeStamp=(byte[])TempData["tm1"]},

                        new CompanyJobEducationPoco{Id=(Guid)TempData["m2"],Job=job,Major=model.Major2,
                        Importance=model.Importance2,TimeStamp=(byte[])TempData["tm2"]},

                        new CompanyJobEducationPoco{Id=(Guid)TempData["m3"],Job=job,Major=model.Major3,
                        Importance=model.Importance3,TimeStamp=(byte[])TempData["tm3"]},

                        new CompanyJobEducationPoco{Id=(Guid)TempData["m4"],Job=job,Major=model.Major4,
                        Importance=model.Importance4,TimeStamp=(byte[])TempData["tm4"]},

                        new CompanyJobEducationPoco{Id=(Guid)TempData["m5"],Job=job,Major=model.Major5,
                        Importance=model.Importance5,TimeStamp=(byte[])TempData["tm5"]},
                    };
                    _cjel.Update(cjep.ToArray());
                    ViewBag.success = "Record Updated";
                    return RedirectToAction("PostedJobs", new {id= model.Company });
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", "Error: "+e);
                    return View(model);
                }
            }


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "Keyword,City_Province")]SearchModel sm)
        {
            if(string.IsNullOrEmpty(sm.Keyword) && string.IsNullOrEmpty(sm.City_Province))
            {
                return View("Index");
            }
            else if (!string.IsNullOrEmpty(sm.Keyword) && string.IsNullOrEmpty(sm.City_Province))
            {
                return View();
            }
            else if (string.IsNullOrEmpty(sm.Keyword) && !string.IsNullOrEmpty(sm.City_Province))
            {
                return View();
            }

            else{
                return View();
            }
            
        }

        // GET: CompanyJob/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            CompanyJobPoco cjp = _cjl.Get(id);
            CompanyJobDescriptionPoco cjdp = _cjdl.Get(id);
            CompanyJobSkillPoco[] cjsp = _cjsl.GetAll().OrderByDescending(c => c.Skill).Where(c => c.Job == id).ToArray();
            CompanyJobEducationPoco[] cjep = _cjel.GetAll().OrderByDescending(c => c.Major).Where(c => c.Job == id).ToArray();


            if (cjp == null || cjdp == null)
            {
                return HttpNotFound();
            }

            CompanyJobViewModel model = new CompanyJobViewModel
            {
                Id = cjp.Id,
                Company = cjp.Company,
                Title = cjdp.JobName,
                Description = cjdp.JobDescriptions,
                Skill1 = cjsp[0].Skill,
                Skill2 = cjsp[1].Skill,
                Skill3 = cjsp[2].Skill,
                Skill4 = cjsp[3].Skill,
                Skill5 = cjsp[4].Skill,
                Major1 = cjep[0].Major,
                Major2 = cjep[1].Major,
                Major3 = cjep[2].Major,
                Major4 = cjep[3].Major,
                Major5 = cjep[4].Major
            };

            return View(model);
        }

        
        // GET: CompanyJob/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            CompanyJobDescriptionPoco cjdp = _cjdl.Get(id);
            if (cjdp == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = (Guid)TempData.Peek("id");
            return View(cjdp);
        }

        // POST: CompanyJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobPoco cjp = _cjl.Get(id);
            CompanyJobDescriptionPoco cjdp = _cjdl.Get(id);
            CompanyJobSkillPoco[] cjsp = _cjsl.GetAll().OrderByDescending(c => c.Skill).Where(c => c.Job == id).ToArray();
            CompanyJobEducationPoco[] cjep = _cjel.GetAll().OrderByDescending(c => c.Major).Where(c => c.Job == id).ToArray();
            ViewBag.id = (Guid)TempData.Peek("id");
            

            try
            {
                _cjsl.Delete(cjsp);
                _cjel.Delete(cjep);
                _cjdl.Delete(new[] { cjdp });
                _cjl.Delete(new[] { cjp });
                return RedirectToAction("PostedJobs", new { id = ViewBag.id});
            }
            catch(Exception e)
            {
                ViewBag.error = e.Message;
                return View(cjdp);
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
