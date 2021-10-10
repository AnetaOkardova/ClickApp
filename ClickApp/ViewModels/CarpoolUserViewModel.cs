using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CarpoolUserViewModel
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
        //public List<OfferViewModel> Offers { get; set; }
        //public List<SkillViewModel> Skills { get; set; }
        //public List<InterestViewModel> Interests { get; set; }
        //public List<UserCardViewModel> Friends { get; set; }
        //public List<UserCardViewModel> RequestingUsers { get; set; }
    }
}
