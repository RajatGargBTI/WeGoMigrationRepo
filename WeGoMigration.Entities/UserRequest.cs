using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class UserRequest
    {
        [Key]
        public int UserRequestID { get; set; }
        public int RequestTo { get; set; }
        public int SendBy { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
