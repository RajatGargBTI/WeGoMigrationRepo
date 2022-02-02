using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class ManageUsers
    {
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public string Userstatus { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class GetAllJobPosted
    {
        public int JobMasterId { get; set; }
        public string CompanyName { get; set; }
        public string Jobdescription { get; set; }
        public string IsFullTime { get; set; }
        public string IsWFH { get; set; }
        public string IsOnsite { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public string RecuterEmail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
