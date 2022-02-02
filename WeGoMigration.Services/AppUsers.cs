using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.Services
{

    public class AppUsers : IAppUsers
    {

        IOptions<ReadConfig> _ConectionString;
        string connectionstring = @"Server=DESKTOP-9HA1SDJ\SQLEXPRESS; Initial Catalog=WeGoMigration; Integrated Security = true;";
        //string connectionstring = "Data Source=SQL5108.site4now.net;Initial Catalog=db_a7e16e_weyougo;Integrated Security=False;User Id=db_a7e16e_weyougo_admin;Password=WeYouGo@123;MultipleActiveResultSets=True";

        public AppUsers(IOptions<ReadConfig> ConnectionString)
        {
            _ConectionString = ConnectionString;
        }
        public bool RegisterUser(AppUser appUser)
        {
            bool IsRegistered = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
               // SqlTransaction sqltrans = con.BeginTransaction(connectionstring);
                var param = new DynamicParameters();
                param.Add("@FullName", appUser.FullName);
                param.Add("@UserName", appUser.UserName);
                param.Add("@Email", appUser.Email);
                param.Add("@ReffralCode", appUser.ReffralCode);
                param.Add("@PhoneNumber", appUser.PhoneNumber);
                param.Add("@Password", appUser.Password);
                param.Add("@RepaetPassword", appUser.RepaetPassword);
                param.Add("@IsWeb", appUser.IsWeb);
                param.Add("@IsAndroid", appUser.IsAndroid);
                param.Add("@IsIos", appUser.IsIos);
                param.Add("@LastLogin", appUser.LastLogin);
                param.Add("@IsActive", appUser.IsActive);
                param.Add("@CreatedOn", appUser.CreatedOn);
                param.Add("@CreatedBy", appUser.CreatedBy);
                param.Add("@UpdatedOn", appUser.UpdatedOn);
                param.Add("@UpdatedBy", appUser.UpdatedBy);
                param.Add("@RoleId", appUser.RoleId);
                var result = con.Execute("CreateAppUsers", param, null, 0, System.Data.CommandType.StoredProcedure);
                if (result > 0)
                {
                    //sqltrans.Commit();
                    IsRegistered = true;
                    string body = "Hi " + appUser.FullName + "," + " Welcome to WE YOU GO!!.Keep an eye on your inbox as we’ll be sending you the best tips regarding  WE YOU GO!!";
                    WeGoMigration.Entities.Common.SendMail(appUser.Email, appUser.FullName, "Registration", body);
                }
                else
                {
                    //sqltrans.Rollback();
                    return IsRegistered;
                }
                return IsRegistered;
            }
        }
        public List<Login> UserLogin(LoginModel login)
        {
            List<Login> loginresult = new List<Login>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@Email", login.UserName);
                param.Add("@PassWord", login.Password);
                return loginresult = con.Query<Login>("UserLogin", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public AppUser GetUserDetails(string email)
        {
            AppUser appUser = new AppUser();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@email", email);
                appUser = con.Query<AppUser>("GetUserDetailsByEmail", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            return appUser;
        }
        public string RecoverPassword(string email, int appUserId)
        {
            long currentTime = DateTime.UtcNow.Ticks;
            string addedParameter = appUserId + "|" + currentTime;
            string cipherText = WeGoMigration.Entities.Common.EnryptString(addedParameter);
            string recoveryURL = $"https://localhost:44391/AppUsers/ResetPassword?id={cipherText}";
            string body = "Your requested reset password link this link will valid of 10 min ignore if you have not requested and contact to admin:" +"<a href=" + recoveryURL + ">Click Here</a>";
            WeGoMigration.Entities.Common.SendMail(email, string.Empty, "Recover Password", body);
            return string.Empty;

        }

        public bool IsLinkValid(string cipherText, out int appUserId)
        {
            bool isValid = false;
            appUserId = 0;
            string decryptedTicks = WeGoMigration.Entities.Common.DecryptString(cipherText);
            string[] urlArray = decryptedTicks.Split('|');
            appUserId = Convert.ToInt32(urlArray[0]);
            int linkTimeoutTime = 10;
            long currentTime = DateTime.UtcNow.AddMinutes(-linkTimeoutTime).Ticks;
            long timestamp = Convert.ToInt64(urlArray[1]);
            if (timestamp < currentTime)
            {
                return isValid;
            }
            isValid = true;
            return isValid;
        }
        public bool ResetPassword(string newPassword, int appUserId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                // SqlTransaction sqltrans = con.BeginTransaction(connectionstring);
                var param = new DynamicParameters();
                param.Add("@password", newPassword);
                param.Add("@userId", appUserId);
                con.Execute("spc_ResetPassword", param, null, 0, System.Data.CommandType.StoredProcedure);

                return true;
            }
        }

        public AppUser GetUserDetailById(int userId)
        {
            AppUser appUser = new AppUser();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                appUser = con.Query<AppUser>("spc_GetUserDetail", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            return appUser;
        }
        public AppUser GetRecruiterDetail(int jobMasterId)
        {
            AppUser appUser = new AppUser();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@JobMasterId", jobMasterId);
                appUser = con.Query<AppUser>("spc_GetRecruiterDetail", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            return appUser;
        }
    }
}
