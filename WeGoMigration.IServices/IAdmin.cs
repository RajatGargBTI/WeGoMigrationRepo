using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;

namespace WeGoMigration.IServices
{
    public interface IAdmin
    {
        List<DataPointForDataWise> GetDataPointForJobPostMonthWise(int datapointId);
        List<DataPointForDataWise> GetDataPointForPostMonthWise(int datapointId);
        List<ManageUsers> GetAllusers(int recordpoint);
        List<GetAllJobPosted> GetAllJobPost(int recordpoint);
        void UpdateUserDetails(UserUpdateModel updateUser);
        void UserDeleteByAdmin(int UserId);
    }
}
