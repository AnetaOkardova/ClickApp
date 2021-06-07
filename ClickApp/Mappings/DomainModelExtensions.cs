using ClickApp.Models;
using ClickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Mappings
{
    public static class DomainModelExtensions
    {
        public static UserViewModel ToUserViewModel(this ApplicationUser entity)
        {
            return new UserViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                Description = entity.Description,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                ProfilePhotoURL = entity.ProfilePhotoURL,
            };
        }

        public static UserSkillViewModel ToUserSkillViewModel(this UserSkill entity)
        {
            return new UserSkillViewModel()
            {
                Id = entity.Id,
                SkillId = entity.SkillId,
                ExperienceDescription = entity.ExperienceDescription,
                ExperienceLevel = entity.ExperienceLevel,
                LatestCertificateImageUrl = entity.LatestCertificateImageUrl,
                YearSkillStarted = entity.YearSkillStarted
            };
        }

        public static UserInterestViewModel ToUserInterestViewModel(this UserInterest entity)
        {
            return new UserInterestViewModel()
            {
                Id = entity.Id,
                InterestId = entity.InterestId,
                LatestCertificateImageUrl = entity.LatestCertificateImageUrl,
                YearSkillStarted = entity.YearSkillStarted,
                ExperienceDescription = entity.ExperienceDescription
            };
        }
        public static OfferViewModel ToOfferViewModel(this Offer entity)
        {
            return new OfferViewModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                ImportantNote = entity.ImportantNote,
                IsProfessional = entity.IsProfessional,
                IsPublic = entity.IsPublic,
                ValidUntil = entity.ValidUntil,
                UserId = entity.UserId
            };
        }

        public static GeneralFieldViewModel ToGeneralFieldViewModel(this GeneralField entity)
        {
            return new GeneralFieldViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public static SkillViewModel ToSkillViewModel(this Skill entity)
        {
            return new SkillViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                GeneralFieldId = entity.GeneralFieldId
            };
        }
    }
}
