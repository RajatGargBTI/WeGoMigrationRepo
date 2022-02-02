using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeGoMigration.Models;

namespace WeGoMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public List<SelectListItem> EnquiryList { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult Mission()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            EnquiryList = new List<SelectListItem>
            {
            new SelectListItem { Text = "Feedback", Value = "1" },
                new SelectListItem { Text = "Complains", Value = "2" },
                new SelectListItem { Text = "Enquiry", Value = "3" }
            };
            foreach (var item in EnquiryList)
            {
                if (item.Value == "Feedback")
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.enquiryList = EnquiryList;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Help()
        {
            EnquiryList = new List<SelectListItem>
            {
            new SelectListItem { Text = "Feedback", Value = "1" },
                new SelectListItem { Text = "Complains", Value = "2" },
                new SelectListItem { Text = "Enquiry", Value = "3" }
            };
            foreach (var item in EnquiryList)
            {
                if (item.Value == "Feedback")
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.enquiryList = EnquiryList;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
