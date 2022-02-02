using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;

namespace WeGoMigration.Controllers
{
    public class PeopleController : Controller
    {
        IPeople _iPeople;
        public PeopleController(IPeople iPeople)
        {
            _iPeople = iPeople;
        }

        public IActionResult VerifiedSearchPeople()
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            ViewBag.FullName = HttpContext.Session.GetString("Username");
            ViewBag.UserList = _iPeople.GetUserSearchList(userid,string.Empty);
            return View();
        }

        public IActionResult SearchPeople([FromBody] string searchPeople)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            ViewBag.FullName = HttpContext.Session.GetString("Username");
            ViewBag.UserList = _iPeople.GetUserSearchList(userid, searchPeople);
            return PartialView("_LoadUserList");
        }

        [HttpPost]
        public IActionResult SendFriendRequest([FromBody] int requestUserId)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            if (userid > 0)
            {
                _iPeople.AddFriendRequest(requestUserId, userid);
            }
            ViewBag.UserList = _iPeople.GetUserSearchList(userid,string.Empty);
            return PartialView("_LoadUserList");
        }

        [HttpPost]
        public IActionResult DeleteRequest([FromBody] int requestId)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            if (userid > 0)
            {
                _iPeople.DeleteRequest(requestId);
            }
            ViewBag.UserList = _iPeople.GetUserSearchList(userid, string.Empty);
            return PartialView("_LoadUserList");
        }

        [HttpPost]
        public IActionResult AddConnection([FromBody] int requestId)
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            if (userid > 0)
            {
                _iPeople.AddConnection(requestId, userid);
            }
            ViewBag.UserList = _iPeople.GetUserSearchList(userid, string.Empty);
            return PartialView("_LoadUserList");
        }
    }
}
