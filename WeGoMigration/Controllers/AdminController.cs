using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using WeGoMigration.Models;

namespace WeGoMigration.Controllers
{
    public class AdminController : Controller
    {
		IAppUsers _IAppUsers;
		IAppUserPosts _appUserPosts;
		IAdmin _admin;
        IPostJob _iPostJob;
      
        public AdminController(IAppUsers appUsers, IAppUserPosts appUserPosts, IAdmin admin, IPostJob postJob)
        {
			_IAppUsers = appUsers;
			_appUserPosts = appUserPosts;
			_admin = admin;
            _iPostJob = postJob;

        }
        public IActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPointForDataWise> dataPointsPost = new List<DataPointForDataWise>();
            List<DataPointForDataWise> dataPointsPost1 = new List<DataPointForDataWise>();
            dataPointsPost = _admin.GetDataPointForPostMonthWise(0);
            dataPointsPost1 = _admin.GetDataPointForJobPostMonthWise(0);
            foreach (var item in dataPointsPost)
            {
                dataPoints.Add(new DataPoint(item.Month, item.TotalPost));
            }
            foreach (var item in dataPointsPost1)
            {
                dataPoints1.Add(new DataPoint(item.Month, item.TotalPost));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);

            return View();
        }
        public IActionResult ManageUsers()
        {
            List<ManageUsers> getAllUsers = new List<ManageUsers>();
            getAllUsers = _admin.GetAllusers(0);
            return View(getAllUsers);
        }

        public IActionResult ManageJobPosted()
        {
            List<GetAllJobPosted> getAllJobs = new List<GetAllJobPosted>();
            getAllJobs = _admin.GetAllJobPost(0);
            return View(getAllJobs);
        }
        [HttpPost]
        public int InsertUpdatePostJob([FromBody] JobMasterVm model)
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
            _iPostJob.AddUpdatePost(jobMaster);
            return 1;
        }
        [HttpGet]
        public JsonResult EditUserDetails(int UserId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            var user = _IAppUsers.GetUserDetailById(UserId);
            EditUser model = new EditUser
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                AppuserId =  user.AppUserId,
            };
            return Json(model);
        }

        public int EditUserDetails([FromBody] UserUpdateModel model)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            try
            {
                _admin.UpdateUserDetails(model);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UserDeleteByAdmin(int UserId)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            try
            {
                _admin.UserDeleteByAdmin(UserId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
