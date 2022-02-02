using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeGoMigration.Models
{
    public class JobMasterVm
    {
        public int JobMasterId { get; set; }
        public string CompanyName { get; set; }
        public string Jobdescription { get; set; }
        public string IsFullTime { get; set; }
        public string IsWFH { get; set; }
        public string IsOnsite { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Image { get; set; }

    }
}
