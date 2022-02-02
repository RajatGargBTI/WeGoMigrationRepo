using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class Common
    {
        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class GetUserPosts
        {
            public string PostText { get; set; }
            public byte [] Picture { get; set; }
            public int PostId { get; set; }
            public int CreatedBy { get; set; }
            public int LikeCount { get; set; }
            public string CommentText { get; set; }
            public int CommentId { get; set; }
            public string FullName { get; set; }
            public List<PostComments> postComments { get; set; }
        }

        public class ModelList
        {
            public GetUserPosts m1 { get; set; }
            public PostComments m2 { get; set; }
        }

        public class PostComments
        {
            public int CommentId { get; set; }
            public int PostId { get; set; }
            public string CommentText { get; set; }
            public string CommentImage { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedOn { get; set; }
            public int CreatedBy { get; set; }
            public DateTime UpdatedOn { get; set; }
            public int UpdatedBy { get; set; }
        }
        public class PostVm
        {
            public string PostText { get; set; }
            public IFormFile ImageFile { get; set; }
            public byte[] Picture { get; set; }
        }

        public  static bool SendMail(string ToMail, string ToName, string SubJect, string Body, Attachment Attachment = null)
        {
            bool IsmailSent = false;
            var fromAddress = new MailAddress("wegopvtltd@gmail.com", "We You Go!!");
            var toAddress = new MailAddress(ToMail, ToName);
             string fromPassword = "wegopvtltd@123";
             string subject = SubJect;
             string body = Body;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body
                
            })
            {
                try
                {
                    if (Attachment != null)
                    {
                        message.Attachments.Add(Attachment);
                    }
                    smtp.Send(message);
                    return IsmailSent;
                }
                catch (Exception ex)
                {
                    return IsmailSent;
                }
            }
        }

        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }
        public class JobMasterVM
        {
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
            public int IsJobApplied { get; set; }
        }
        public class SearchPeopleVM
        {
            public int AppUserId { get; set; }
            public int UserRequestID { get; set; }
            public string FullName { get; set; }
            public byte[] ImageDataFiles { get; set; }
            public string ButtonType { get; set; }
            public int ButtonTypeID { get; set; }
            public bool IsAccepted { get; set; }
        }
        public static string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}
