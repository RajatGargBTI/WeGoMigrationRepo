using System;

namespace WeGoMigration.Entities
{
    public class Login
    {
        public int AppUserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
