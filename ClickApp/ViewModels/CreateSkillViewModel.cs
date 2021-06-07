using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CreateSkillViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int GeneralFieldId { get; set; }
        public List<GeneralFieldViewModel> GeneralFields { get; set; }
    }
}
