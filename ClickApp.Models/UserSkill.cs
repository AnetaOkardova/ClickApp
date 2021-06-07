using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class UserSkill
    {
        public int Id { get; set; }

        [Display(Name = "Year when skill developed")]
        public DateTime? YearSkillStarted { get; set; }

        
        [Display(Name = "Experience description")]
        public string? ExperienceDescription { get; set; }
        [Required]
        [Display(Name = "Experience level")]
        public int ExperienceLevel { get; set; }

        [Display(Name = "Latest certificate")]
        public string? LatestCertificateImageUrl { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
       
    }
}
