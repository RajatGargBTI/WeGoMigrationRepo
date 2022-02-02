using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGoMigration.Entities;
using static WeGoMigration.Entities.Common;

namespace WeGoMigration.IServices
{
    public interface IPeople
    {
        List<SearchPeopleVM> GetUserSearchList(int userId, string searchValue);
        void AddFriendRequest(int requestTo, int UserId);
        void DeleteRequest(int requestId);
        void AddConnection(int requestId, int UserId);
    }
}
