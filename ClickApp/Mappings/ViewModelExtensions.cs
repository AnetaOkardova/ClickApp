﻿using ClickApp.Models;
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

        public static Offer ToModel(this OfferViewModel entity)
        {
            return new Offer()
            {
                Id = entity.Id,
                Title = entity.Title,
                UserId = entity.UserId,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                ImportantNote = entity.ImportantNote,
                IsProfessional = entity.IsProfessional,
                IsPublic = entity.IsPublic,
                ValidUntil = Convert.ToDateTime(entity.ValidUntil),
            };
        }

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
        public static CarpoolOffer ToModel(this CreateCarpoolOfferViewModel entity)
        {
            return new CarpoolOffer()
            {
                DriverId = entity.DriverId,
                LeavingFrom = entity.LeavingFrom,
                ArrivingAt = entity.ArrivingAt,
                LeavingHour = entity.LeavingHour,
                LeavingMinutes = entity.LeavingMinutes,
                LeavingNote = entity.LeavingNote,
                SeatsAvailable = entity.SeatsAvailable,
                ReturnFrom = entity.ReturnFrom,
                ReturnAt = entity.ReturnAt,
                ReturnHour = entity.ReturnHour,
                ReturnMinutes = entity.ReturnMinutes,
                ReturnNote = entity.ReturnNote,
                ReturnSeatsAvailable = entity.ReturnSeatsAvailable,
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

        public static CarpoolOffer ToModel(this CarpoolOfferViewModel entity)
        {
            return new CarpoolOffer()
            {
                Id = entity.Id,
                DriverId = entity.DriverId,
                LeavingFrom = entity.LeavingFrom,
                ArrivingAt = entity.ArrivingAt,
                LeavingHour = entity.LeavingHour,
                LeavingMinutes = entity.LeavingMinutes,
                SeatsAvailable = entity.SeatsAvailable,
                LeavingNote = entity.LeavingNote,
                DateCreated = Convert.ToDateTime(entity.DateCreated),
                ReturnFrom = entity.ReturnFrom,
                ReturnAt = entity.ReturnAt,
                ReturnHour = entity.ReturnHour,
                ReturnMinutes = entity.ReturnMinutes,
                ReturnSeatsAvailable = entity.ReturnSeatsAvailable,
                ReturnNote = entity.ReturnNote,
                RequestingPassengers = entity.RequestingPassengers,
                AcceptedPassengers = entity.AcceptedPassengers
            };
        }
        public static Message ToModel(this CreateMessageViewModel entity)
        {
            return new Message()
            {
                UserFromId = entity.UserFromId,
                UserToId = entity.UserToId,
                Content = entity.Content
            };
        }
        public static JournalEntry ToModel(this CreateJournalEntryViewModel entity)
        {
            return new JournalEntry()
            {
                UserId = entity.UserId,
                Theme = entity.Theme,
                Title = entity.Title,
                Content = entity.Content,
                Public = entity.Public
            };
        }
        public static JournalEntry ToModel(this JournalEntryViewModel entity)
        {
            return new JournalEntry()
            {
                Id = entity.Id,
                DateCreated = entity.DateCreated,
                Content = entity.Content,
                Theme = entity.Theme,
                Public = entity.Public,
                UserId = entity.UserId,
                Title = entity.Title
            };
        }

    }
}
