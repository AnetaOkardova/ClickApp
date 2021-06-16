using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class GeneralField
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<Interest>? Interests { get; set; }
        public GeneralFieldCode Code { get; set; }

    }
}
