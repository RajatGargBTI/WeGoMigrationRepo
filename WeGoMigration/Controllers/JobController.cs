using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using WeGoMigration.Models;

namespace WeGoMigration.Controllers
{
    public class JobController : Controller
    {
        IPostJob _iPostJob;
        IAppUsers _IAppUsers;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession session => _httpContextAccessor.HttpContext.Session;
        public JobController(IPostJob postJob, IHttpContextAccessor httpContextAccessor, IAppUsers IAppUsers)
        {
            _iPostJob = postJob;
            _httpContextAccessor = httpContextAccessor;
            _IAppUsers = IAppUsers;
        }
        public IActionResult PostJob()
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            ViewBag.FullName = HttpContext.Session.GetString("Username");
            var jobList = _iPostJob.GetJobPosts(0, userid);
            return View(jobList);
        }

        public int InsertUpdatePostJob(JobMasterVm model)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            
            JobMaster jobMaster = new JobMaster
            {
                JobMasterId = model.JobMasterId,
                CompanyName = model.CompanyName,
                Jobdescription = model.Jobdescription,
                IsFullTime = model.IsFullTime.Equals("yes") ? true : false,
                IsWFH = model.IsWFH.Equals("yes") ? true : false,
                IsOnsite = model.IsOnsite.Equals("yes") ? true : false,
                IsActive = true,
                CreatedBy = userid,
                UpdatedBy = userid,
                CreatedOn = DateTime.Now
            };
            if (model.Image != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    model.Image.CopyTo(stream);
                    var fileBytes = stream.ToArray();
                    jobMaster.JobTemplate = fileBytes;
                }
            }
            _iPostJob.AddUpdatePost(jobMaster);
            return 1;
        }
        public int DeletePostJob(int postId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            
            _iPostJob.DeletePost(postId, userId);
            return 1;
        }
        [HttpGet]
        public JsonResult GetPostJob(int postId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            var posts = _iPostJob.GetJobPosts(postId, userId);
            return Json(posts);
        }

        public IActionResult VerifiedSearchJob(string search="")
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            ViewBag.FullNameV = HttpContext.Session.GetString("Username");
            if(!string.IsNullOrWhiteSpace(search))
            {
                var jobLista = _iPostJob.GetAppliedJobs(userid, search);
                return View(jobLista);
            }
            else
            {
                var jobList = _iPostJob.GetAppliedJobs(userid, string.Empty);
                return View(jobList);
            }
           
        }
        [HttpPost]
        public  IActionResult SearchJob([FromBody] string searchJob)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            var jobList = _iPostJob.GetAppliedJobs(userid, searchJob);
            return PartialView ("_SearchJobPartialView", jobList);
        }
        public int SaveAppliedJob(JobApplied jobApplied)
        {
            if (jobApplied.Resume_CV != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    jobApplied.Resume_CV.CopyTo(stream);
                    var fileBytes = stream.ToArray();
                    jobApplied.Resume = fileBytes;
                }
            }
            jobApplied.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            try
            {
                _iPostJob.AddAppliedJob(jobApplied);
                var recruiterDetail = _IAppUsers.GetRecruiterDetail(jobApplied.JobMasterId);
                Attachment att = new Attachment(new MemoryStream(jobApplied.Resume), $"{jobApplied.FullName}_CV.pdf");
                string body = "Hi " + recruiterDetail.FullName + "," + jobApplied.FullName + " has appliedto your posted job.";
                WeGoMigration.Entities.Common.SendMail(recruiterDetail.Email, recruiterDetail.FullName, "Job Applied", body, att);
            }
            catch (Exception)
            {
                return 1;
            }
            return 1;
        }

        [HttpGet]
        public JsonResult GetPostJobApplied(int postId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            var posts = _iPostJob.GetJobPostToApply(postId, userId).FirstOrDefault();
            var user = _IAppUsers.GetUserDetailById(userId);
            JobPostAppliedVM model = new JobPostAppliedVM
            {
                JobMasterId = posts.JobMasterId,
                CompanyName = posts.CompanyName,
                Jobdescription = posts.Jobdescription,
                FullName = user.FullName,
                Email = user.Email
            };
            return Json(model);
        }
    }
}
