using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeGoMigration.Models
{
    public class AboutVM
    {
        public int AboutId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SkillSet { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }
        public string SpecialAboutYou { get; set; }
        public string Career { get; set; }
    }
}
