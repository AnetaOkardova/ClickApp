using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int GeneralFieldId { get; set; }
        //public List<string> UserIds { get; set; }
    }
}
