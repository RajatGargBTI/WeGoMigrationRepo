using System.Collections.Generic;
using WeGoMigration.Entities;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.IServices
{
    public interface IAppUsers
    {
        bool RegisterUser(AppUser appUser);
        List<Login> UserLogin(LoginModel login);
        AppUser GetUserDetails(string email);
        string RecoverPassword(string email, int appUserId);
        bool IsLinkValid(string cipherText, out int appUserId);
        bool ResetPassword(string newPassword, int appUserId);
        AppUser GetUserDetailById(int userId);
        AppUser GetRecruiterDetail(int jobMasterId);
    }
}
