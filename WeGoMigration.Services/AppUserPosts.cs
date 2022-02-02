using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using WeGoMigration.IServices;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.Services
{
    public class AppUserPosts : IAppUserPosts
    {
        //string connectionstring = "Data Source=SQL5108.site4now.net;Initial Catalog=db_a7e16e_weyougo;Integrated Security=False;User Id=db_a7e16e_weyougo_admin;Password=WeYouGo@123;MultipleActiveResultSets=True";

        string connectionstring = @"Server=DESKTOP-9HA1SDJ\SQLEXPRESS; Initial Catalog=WeGoMigration; Integrated Security = true;";
        public List<GetUserPosts> GetUserPostsById(int UserId)
        {
            List<GetUserPosts> getUserPosts;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@AppUserId", UserId);
                return getUserPosts = con.Query<GetUserPosts>("GetUserPostById", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<GetUserPosts> GetUserPostByOthers(int UserId)
        {
            List<GetUserPosts> getUserPosts;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@AppUserId", UserId);
                return getUserPosts = con.Query<GetUserPosts>("GetUserPostByOthers", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        public int SavePosts(Post post)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", post.PostId);
                param.Add("@PostText", post.PostText);
                param.Add("@CreatedBy", post.CreatedBy);
                if (post.ImageDataFiles !=null)
                {
                    param.Add("@ImageDataFiles", post.ImageDataFiles);
                    con.Query("spc_SavePosts", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    con.Query("spc_SavePostsWithoutImage", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                }
                return 1;
            }
        }
        public int DeletePostsByPostId(int PostId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", PostId);
                con.Query("spc_DeletePostsByPostId", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }
        public GetUserPosts GetUserPostsByPostId(int PostId)
        {
            GetUserPosts getUserPosts;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", PostId);
                return getUserPosts = con.Query<GetUserPosts>("GetUserPostByPostId", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int SaveAboutDetail(About about)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@AboutId", about.AboutId);
                param.Add("@Name", about.Name);
                param.Add("@DateOfBirth", about.DateOfBirth);
                param.Add("@SkillSet", about.SkillSet);
                param.Add("@Education", about.Education);
                param.Add("@Address", about.Address);
                param.Add("@SpecialAboutYou", about.SpecialAboutYou);
                param.Add("@Career", about.Career);
                param.Add("@UserId", about.UserId);
                param.Add("@ImageDataFiles", about.ImageDataFiles);
                con.Query("spc_SaveAboutDetail", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }

        }
        public int SvaePostLikes(int UserId, int PostId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", PostId);
                param.Add("@PostLikeBy", UserId);
                con.Query("spc_SavePostLikes", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }
        public About GetAboutByUser(int UserId)
        {
            About getUserPosts;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", UserId);
                return getUserPosts = con.Query<About>("spc_GetAboutDetail", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int SaveUpdateCommentByPostId(int PostId, string comment, int createdBy, int commentId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", PostId);
                param.Add("@Comment", comment);
                param.Add("@Createdby", createdBy);
                param.Add("@commentId", commentId);
                con.Query("spc_SaveUpdateComment", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }

        public int DeleteCommentByPostId(int PostId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@PostId", PostId);
                con.Query("spc_DeleteCommentByPostId", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }

        public int DeleteCommentByCommentId(int commentId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@CommentId", commentId);
                con.Query("spc_DeleteCommentByCommentId", param, null, true, 0, System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }

        public List<PostComments> GetCommentsList(int UserId)
        {
            List<PostComments> comments;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", UserId);
                return comments = con.Query<PostComments>("spc_GetCommentsList", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
