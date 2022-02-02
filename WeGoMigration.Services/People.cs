using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.Services
{
    public class People : IPeople
    {
        string connectionstring = @"Server=DESKTOP-9HA1SDJ\SQLEXPRESS; Initial Catalog=WeGoMigration; Integrated Security = true;";
        public List<SearchPeopleVM> GetUserSearchList(int userId, string searchValue)
        {
            List<SearchPeopleVM> jobMasterList = new List<SearchPeopleVM>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@LoggedInUserid", userId);
                param.Add("@Value", searchValue);
                return jobMasterList = con.Query<SearchPeopleVM>("SearchPeopleForConnection", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public void AddFriendRequest(int requestTo, int UserId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@RequestTo", requestTo);
                param.Add("@UserId", UserId);
                con.Query("spc_InsertUserRequest", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }

        public void DeleteRequest(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserRequestID", requestId);
                con.Query("spc_DeleteRequest", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }

        public void AddConnection(int requestId, int UserId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@LoggedInUserid", UserId);
                param.Add("@UserRequestID", requestId);
                con.Query("spc_InsertConnection", param, null, true, 0, System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
