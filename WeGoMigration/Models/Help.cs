using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeGoMigration.Models
{
    public class Help
    {
        public Help()
        {
            EnquiryList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Complains", Value = "1" },
                new SelectListItem { Text = "Feedback", Value = "2" },
                new SelectListItem { Text = "Enquiry", Value = "3" }
           };
        }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Enquiry is required")]
        public List<SelectListItem> EnquiryList { get; set; }
    }
}
