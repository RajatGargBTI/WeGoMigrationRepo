using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using WeGoMigration.Models;
using static WeGoMigration.Entities.Common;
namespace WeGoMigration.Controllers
{
    public class AppUsersController : Controller
    {
        IAppUsers _IAppUsers;
        IAppUserPosts _appUserPosts;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUsersController(IAppUsers IAppUsers, IAppUserPosts appUserPosts, IHttpContextAccessor httpContextAccessor)
        {
            _IAppUsers = IAppUsers;
            _appUserPosts = appUserPosts;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            try
            {
                string decrptypass = EnryptString(register.Password);
                if (ModelState.IsValid)
                {
                    var appUsers = new AppUser
                    {
                        FullName = register.FullName,
                        UserName = register.UserName,
                        Email = register.Email,
                        ReffralCode = register.ReffralCode,
                        Password = decrptypass,
                        RepaetPassword = decrptypass,
                        PhoneNumber = register.PhoneNumber,
                        IsWeb = true,
                        IsAndroid = false,
                        IsIos = false,
                        IsActive = true,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = 1,
                        UpdatedOn = DateTime.Now,
                        LastLogin = DateTime.Now,
                        RoleId =1,
                    };
                    _IAppUsers.RegisterUser(appUsers);
                    return RedirectToAction("Index", new { Message = "You have registered succesfully!!" });
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public IActionResult getUserPosts(int UserId)
        {
            List<GetUserPosts> userPosts = _appUserPosts.GetUserPostsById(UserId);
            List<PostComments> PostComments = _appUserPosts.GetCommentsList(UserId);
            List<GetUserPosts> userPostsNew = new List<GetUserPosts>();
            foreach (var item in userPosts)
            {
                userPostsNew.Add(new GetUserPosts
                {
                    PostText = item.PostText,
                    Picture = item.Picture,
                    PostId = item.PostId,
                    CommentText = item.CommentText,
                    CommentId = item.CommentId,
                    postComments = PostComments.Where(x => x.PostId == item.PostId).ToList()
                }); 
            }
            ViewBag.UserPostById = userPostsNew;
            ViewBag.UserPostDetails = userPostsNew.ToList();
            return PartialView("_loadAllPost");
        }
        public IActionResult Login(LoginVm loginVm)
        {
            string message = "";
            List<Login> loginresult = new List<Login>();
            try
            {
                if (ModelState.IsValid)
                {
                    //string plaintext = DecryptString("d2V5b3Vnb0AxMjM=");
                    string EncrptedPassword = EnryptString(loginVm.Password);
                    var loginModel = new LoginModel
                    {
                        UserName = loginVm.UserName,
                        Password = EncrptedPassword
                    };
                    loginresult = _IAppUsers.UserLogin(loginModel);
                }
                var lgnResult = loginresult.Where(p => p.AppUserId != 0).SingleOrDefault();
                

                if (lgnResult != null && lgnResult.RoleId ==1)
                {
                    HttpContext.Session.SetInt32("SessionUserId", lgnResult.AppUserId);
                    HttpContext.Session.SetString("Username", lgnResult.FullName);
                    if (lgnResult.AppUserId == 0)
                    {
                        ViewBag.Message = "Email or password is wrong please try again";
                        return RedirectToAction("Index");
                    }
                    getUserPosts(lgnResult.AppUserId);
                    ViewBag.ProfileDetails = lgnResult;
                    return View();
                }
                if (lgnResult != null && lgnResult.RoleId == 2)
                {
                    HttpContext.Session.SetInt32("SessionUserId", lgnResult.AppUserId);
                    if (lgnResult.AppUserId == 0)
                    {
                         message = "Email or password is wrong please try again";
                        return RedirectToAction("Index", new { Message = message });
                    }
                    ViewBag.ProfileDetails = lgnResult;
                    return RedirectToAction("Index", "Admin");
                }
                 message = "Email or password is wrong please try again";
                return RedirectToAction("Index", new { Message = message });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public IActionResult CreatePost()
        {
            return PartialView("_CreatePost");
        }
        [HttpGet]
        public IActionResult GetAbout(int UserId)
        {
            var about = _appUserPosts.GetAboutByUser(UserId);
            return PartialView("_AboutPartialView", about);
        }
        [HttpPost]
        public JsonResult SavePost(string posttexts, IFormFile file,int PostId)
        {
            if (!string.IsNullOrEmpty(posttexts) || file.Length > 0)
            {
                var post = new Post();
                post.PostText = posttexts;
                post.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
                post.PostId = PostId;
                if (file != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        var fileBytes = stream.ToArray();
                        post.ImageDataFiles = fileBytes;
                    }
                }
                _appUserPosts.SavePosts(post);
            }
            return Json("");
        }

        public IActionResult getUserPostsByPostId(int PostId)
        {
            var post =  _appUserPosts.GetUserPostsByPostId(PostId);
            var modal = new WeGoMigration.Entities.Common.PostVm()
            {
                PostText = post.PostText,
                Picture = post.Picture

            };
            return PartialView("_CreatePost", modal);
        }
        public int DeletePostsByPostId(int PostId)
        {
            var post = _appUserPosts.DeletePostsByPostId(PostId);
            return post;
        }

        public IActionResult About(int UserId)
        {
            var about = _appUserPosts.GetAboutByUser(UserId);
            return View(about);
        }

        public int SaveAbout(About about)
        {
            if (about.ImageFile != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    about.ImageFile.CopyTo(stream);
                    var fileBytes = stream.ToArray();
                    about.ImageDataFiles = fileBytes;
                }
            }
            about.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            var post = _appUserPosts.SaveAboutDetail(about);
            return 1;
        }

        public int PostLikes()
        {
            var post = _appUserPosts.SaveAboutDetail(new About());
            return 1;
        }


        public int SaveUpdateComment(int postId, string commentValue,int commentId, int UserId)
        {
            var post = _appUserPosts.SaveUpdateCommentByPostId(postId, commentValue, UserId, commentId);
            return 1;
        }
        public int DeleteComment(int commentId)
        {
            var post = _appUserPosts.DeleteCommentByCommentId(commentId);
            return 1;
        }
        public int DeleteCommentByPostId(int postId)
        {
            var post = _appUserPosts.DeleteCommentByPostId(postId);
            return 1;
        }

        public IActionResult PostJob()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            ViewBag.NotValidEmail = false;
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var appUser = _IAppUsers.GetUserDetails(email)
;
                if (appUser != null && !string.IsNullOrEmpty(appUser.Email))
                {
                    _IAppUsers.RecoverPassword(appUser.Email, appUser.AppUserId);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.NotValidEmail = true;
            return View();
        }
        public IActionResult ResetPassword()
        {
            string ciphertext = _httpContextAccessor.HttpContext.Request.Query["id"];
            if (_IAppUsers.IsLinkValid(ciphertext, out int appUserId))
            {
                ViewBag.IsValidLink = true;
                ViewBag.AppUserId = appUserId;
            }
            else
            {
                ViewBag.IsValidLink = false;
                ViewBag.AppUserId = 0;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ResetPassword(string newPassword, int appUserId)
        {
            string decrptypass = EnryptString(newPassword);
            _IAppUsers.ResetPassword(decrptypass, appUserId);
            return Json("");
        }

        public IActionResult RecentPostByOthers()
        {
            ViewBag.FullNameO= HttpContext.Session.GetString("Username");
            int userid = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            ViewBag.UserId= Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            getUserPostsByOthers(userid);
            return View();
        }
        public IActionResult getUserPostsByOthers(int UserId)
        {
            List<GetUserPosts> userPosts = _appUserPosts.GetUserPostByOthers(UserId);
            List<PostComments> PostComments = _appUserPosts.GetCommentsList(UserId);
            List<GetUserPosts> userPostsNew = new List<GetUserPosts>();
            foreach (var item in userPosts)
            {
                userPostsNew.Add(new GetUserPosts
                {
                    PostText = item.PostText,
                    Picture = item.Picture,
                    PostId = item.PostId,
                    CommentText = item.CommentText,
                    CommentId = item.CommentId,
                    FullName = item.FullName,
                    postComments = PostComments.Where(x => x.PostId == item.PostId).ToList()
                });
            }
            ViewBag.UserPostById = userPostsNew;
            ViewBag.UserPostDetails = userPostsNew.ToList();
            return PartialView("_loadAllPostByOthers");
        }
    }
}
