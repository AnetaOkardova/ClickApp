using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class UserInterestViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Year when interest developed")]
        public DateTime? YearSkillStarted { get; set; }

        [Display(Name = "Experience")]
        public string? ExperienceDescription { get; set; }

        [Display(Name = "Image")]
        public string? LatestCertificateImageUrl { get; set; }

        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public int InterestId { get; set; }
    }
}
