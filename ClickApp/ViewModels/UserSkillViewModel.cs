using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class UserSkillViewModel
    {
        public int Id { get; set; }

        
        [Display(Name = "Year when skill developed")]
        public DateTime? YearSkillStarted { get; set; }

        [Display(Name = "Experience")]
        public string? ExperienceDescription { get; set; }
        [Required]
        [Display(Name = "Experience level")]
        public int ExperienceLevel { get; set; }

        [Display(Name = "Latest certificate")]
        public string? LatestCertificateImageUrl { get; set; }

        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public int SkillId { get; set; }
    }
}
