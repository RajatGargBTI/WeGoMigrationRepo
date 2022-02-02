using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.IServices
{
    public interface IAppUserPosts
    {
        List<GetUserPosts> GetUserPostsById(int UserId);
        List<GetUserPosts> GetUserPostByOthers(int UserId);
       
        int SavePosts(Post post);
        GetUserPosts GetUserPostsByPostId(int PostId);
        int DeletePostsByPostId(int PostId);
        int SaveAboutDetail(About about);
        int SvaePostLikes(int UserId,int PostId);
        About GetAboutByUser(int UserId);
        int SaveUpdateCommentByPostId(int PostId, string comment, int createdBy, int commentId);
        int DeleteCommentByPostId(int postID);
        int DeleteCommentByCommentId(int commentId);
        List<PostComments> GetCommentsList(int UserId);
    }
}
