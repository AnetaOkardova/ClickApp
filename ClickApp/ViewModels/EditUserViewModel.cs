using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickApp.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ProfilePhotoURL { get; set; }
        public List<SkillViewModel> Skills { get; set; }
        public List<SkillViewModel> AllSkills { get; set; }

        public List<InterestViewModel> Interests { get; set; }
        public List<InterestViewModel> AllInterests { get; set; }


    }
}
