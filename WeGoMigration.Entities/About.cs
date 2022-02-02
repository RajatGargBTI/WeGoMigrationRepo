using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SkillSet { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }
        public string SpecialAboutYou { get; set; }
        public string Career { get; set; }
        public int UserId { get; set; }

        public IFormFile ImageFile { get; set; }
        public byte[] ImageDataFiles { get; set; }
    }
}
