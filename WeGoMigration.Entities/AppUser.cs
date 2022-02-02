using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class AppUser
    {
        [Key]
        public int AppUserId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Name is required")]
        public string Email { get; set; }
        public string ReffralCode { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password Name is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Repaet Password Name is required")]
        public string RepaetPassword { get; set; }
        public bool IsWeb { get; set; }
        public bool IsAndroid { get; set; }
        public bool IsIos { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int RoleId { get; set; }
    }
}
