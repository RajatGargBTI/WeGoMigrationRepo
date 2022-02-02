using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;

namespace WeGoMigration.Services
{
    public class Admin:IAdmin
    {
        string connectionstring = @"Server=DESKTOP-9HA1SDJ\SQLEXPRESS; Initial Catalog=WeGoMigration; Integrated Security = true;";
        //string connectionstring = "Data Source=SQL5108.site4now.net;Initial Catalog=db_a7e16e_weyougo;Integrated Security=False;User Id=db_a7e16e_weyougo_admin;Password=WeYouGo@123;MultipleActiveResultSets=True";

        public List<DataPointForDataWise> GetDataPointForJobPostMonthWise(int datapointId=0)
         {
            List<DataPointForDataWise> dataPoints = new List<DataPointForDataWise>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@datapointId", datapointId);

                return dataPoints = con.Query<DataPointForDataWise>("GetDataPointForJobPostMonthWise", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<DataPointForDataWise> GetDataPointForPostMonthWise(int datapointId=0)
        {
            List<DataPointForDataWise> dataPoints = new List<DataPointForDataWise>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@datapointId", datapointId);
                return dataPoints = con.Query<DataPointForDataWise>("GetDataPointForPostMonthWise", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<ManageUsers> GetAllusers(int recordpoint)
        {
            List<ManageUsers> GetAllusers = new List<ManageUsers>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@recordpoint", recordpoint);
                return GetAllusers = con.Query<ManageUsers>("ManageUser", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public List<GetAllJobPosted> GetAllJobPost(int recordpoint)
        {
            List<GetAllJobPosted> GetAllPostedJob = new List<GetAllJobPosted>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@recordpoint", recordpoint);
                return GetAllPostedJob = con.Query<GetAllJobPosted>("GetAllPostedJob", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public void UpdateUserDetails(UserUpdateModel userUpdateModel)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@Userid", userUpdateModel.UserId);
                param.Add("@password", userUpdateModel.Password);
                
                con.Query("UserEditByAdmin", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }

        public void UserDeleteByAdmin(int UserId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", UserId);
                con.Query("UserDeleteByAdmin", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }

    }
}
