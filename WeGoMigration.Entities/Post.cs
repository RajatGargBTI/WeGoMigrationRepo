using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string PostText { get; set; }
        public byte[] ImageDataFiles { get; set; }
        public string PostImage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
