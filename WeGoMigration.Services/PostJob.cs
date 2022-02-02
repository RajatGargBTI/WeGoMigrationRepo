using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.Services
{
    public class PostJob : IPostJob
    {
        string connectionstring = @"Server=DESKTOP-9HA1SDJ\SQLEXPRESS; Initial Catalog=WeGoMigration; Integrated Security = true;";
        //string connectionstring = "Data Source=SQL5108.site4now.net;Initial Catalog=db_a7e16e_weyougo;Integrated Security=False;User Id=db_a7e16e_weyougo_admin;Password=WeYouGo@123;MultipleActiveResultSets=True";

        public void AddUpdatePost(JobMaster jobMaster)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", jobMaster.JobMasterId);
                param.Add("@CompanyName", jobMaster.CompanyName);
                param.Add("@Jobdescription", jobMaster.Jobdescription);
                param.Add("@IsFullTime", jobMaster.IsFullTime);
                param.Add("@IsWFH", jobMaster.IsWFH);
                param.Add("@IsOnsite", jobMaster.IsOnsite);
                param.Add("@IsActive", jobMaster.IsActive);
                param.Add("@UserId", jobMaster.CreatedBy);
                param.Add("@JobTemplate", jobMaster.JobTemplate);
                con.Query("spc_InsertUpdateJobMaster", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }
        public void DeletePost(int postId, int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", postId);
                param.Add("@UserId", userId);
                con.Query("spc_DeleteJobMaster", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }
        public List<JobMaster> GetJobPosts(int postId, int userId)
        {
            List<JobMaster> jobMasterList = new List<JobMaster>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", postId);
                param.Add("@UserId", userId);
                return jobMasterList = con.Query<JobMaster>("spc_GetJobMaster", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public void AddAppliedJob(JobApplied jobApplied)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", jobApplied.JobMasterId);
                param.Add("@FullName", jobApplied.FullName);
                param.Add("@Email", jobApplied.Email);
                param.Add("@PhoneNumber", jobApplied.PhoneNumber);
                param.Add("@Resume", jobApplied.Resume);
                param.Add("@UserId", jobApplied.CreatedBy);
                con.Query("spc_SaveAppliedJob", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }
        public List<JobMasterVM> GetAppliedJobs(int userId, string searchJob)
        {
            List<JobMasterVM> jobMasterList = new List<JobMasterVM>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                param.Add("@SearchJob", searchJob);
                return jobMasterList = con.Query<JobMasterVM>("spc_GetAppliedJobs", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<JobMaster> GetJobPostToApply(int postId, int userId)
        {
            List<JobMaster> jobMasterList = new List<JobMaster>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", postId);
                param.Add("@UserId", userId);
                return jobMasterList = con.Query<JobMaster>("spc_GetJobMasterToApplyJob", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
