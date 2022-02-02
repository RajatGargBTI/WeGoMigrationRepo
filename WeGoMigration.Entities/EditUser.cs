using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class EditUser
    {
        public int AppuserId  { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
