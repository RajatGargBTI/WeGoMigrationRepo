using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class JobMaster
    {
        [Key]
        public int JobMasterId { get; set; }
        public string CompanyName { get; set; }
        public string Jobdescription { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsWFH { get; set; }
        public bool IsOnsite { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public byte[] JobTemplate { get; set; }
    }
}
