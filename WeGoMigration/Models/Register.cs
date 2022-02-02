using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeGoMigration.Models
{
    public class Register
    {
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
    }
}
