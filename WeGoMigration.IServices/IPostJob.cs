using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.IServices
{
    public interface IPostJob
    {
        void AddUpdatePost(JobMaster jobMaster);
        void DeletePost(int postId, int userId);
        List<JobMaster> GetJobPosts(int postId, int userId);
        void AddAppliedJob(JobApplied jobApplied);
        List<JobMasterVM> GetAppliedJobs(int userId, string searchJob);
        List<JobMaster> GetJobPostToApply(int postId, int userId);
    }
}
