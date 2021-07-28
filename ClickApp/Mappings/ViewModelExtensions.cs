using ClickApp.Models;
using ClickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Mappings
{
    public static class ViewModelExtensions
    {
        public static ApplicationUser ToModel(this UserViewModel entity)
        {
            return new ApplicationUser()
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
                //Offers = entity.Offers
            };
        }
        public static ApplicationUser ToModel(this EditUserViewModel entity)
        {
            return new ApplicationUser()
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
        public static UserSkill ToModel(this UserSkillViewModel entity)
        {
            return new UserSkill()
            {
                Id = entity.Id,
                SkillId = entity.SkillId,
                ExperienceDescription = entity.ExperienceDescription,
                ExperienceLevel = entity.ExperienceLevel,
                LatestCertificateImageUrl = entity.LatestCertificateImageUrl,
                YearSkillStarted = entity.YearSkillStarted
            };
        }

        public static UserInterest ToModel(this UserInterestViewModel entity)
        {
            return new UserInterest()
            {
                Id = entity.Id,
                InterestId = entity.InterestId,
                LatestCertificateImageUrl = entity.LatestCertificateImageUrl,
                YearSkillStarted = entity.YearSkillStarted,
                ExperienceDescription = entity.ExperienceDescription
            };
        }

        //public static Offer ToModel(this OfferViewModel entity)
        //{
        //    return new Offer()
        //    {
        //        Id = entity.Id,
        //        Title = entity.Title,
        //        DateCreated = entity.DateCreated,
        //        DateModified = entity.DateModified,
        //        Description = entity.Description,
        //        ImageUrl = entity.ImageUrl,
        //        ImportantNote = entity.ImportantNote,
        //        IsProfessional = entity.IsProfessional,
        //        IsPublic = entity.IsPublic,
        //        ValidUntil = entity.ValidUntil,  ToString("dd-MM-yyyy")???
        //        UserId = entity.UserId
        //    };
        //}

        public static Offer ToModel(this CreateOfferViewModel entity)
        {
            return new Offer()
            {
                Title = entity.Title,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                ImportantNote = entity.ImportantNote,
                IsProfessional = entity.IsProfessional,
                IsPublic = entity.IsPublic,
                ValidUntil = entity.ValidUntil,
            };
        }
        public static GeneralField ToModel(this GeneralFieldViewModel entity)
        {
            return new GeneralField()
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code
            };
        }

        public static GeneralField ToModel(this CreateGeneralFieldViewModel entity)
        {
            return new GeneralField()
            {
                Name = entity.Name,
                Code = entity.Code
            };
        }
        public static Skill ToModel(this SkillViewModel entity)
        {
            return new Skill()
            {
                Id = entity.Id,
                Name = entity.Name,
                GeneralFieldId = entity.GeneralFieldId
            };
        }
        public static UserSkill ToUserSkillModel(this SkillViewModel entity)
        {
            return new UserSkill()
            {
                SkillId = entity.Id
            };
        }
        public static Skill ToModel(this CreateSkillViewModel entity)
        {
            return new Skill()
            {
                Name = entity.Name,
                GeneralFieldId = entity.GeneralFieldId
            };
        }
        public static Interest ToModel(this InterestViewModel entity)
        {
            return new Interest()
            {
                Id = entity.Id,
                Name = entity.Name,
                GeneralFieldId = entity.GeneralFieldId
            };
        }
        public static Interest ToModel(this CreateInterestViewModel entity)
        {
            return new Interest()
            {
                Name = entity.Name,
                GeneralFieldId = entity.GeneralFieldId
            };
        }
    }
}
