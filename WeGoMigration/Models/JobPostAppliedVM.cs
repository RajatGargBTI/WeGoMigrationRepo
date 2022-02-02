using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeGoMigration.Models
{
    public class JobPostAppliedVM
    {
        public int JobMasterId { get; set; }
        public string CompanyName { get; set; }
        public string Jobdescription { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
