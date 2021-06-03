using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int GeneralFieldId { get; set; }
        public GeneralField GeneralField { get; set; }
        public List<UserSkill> Users { get; set; }
    }
}
