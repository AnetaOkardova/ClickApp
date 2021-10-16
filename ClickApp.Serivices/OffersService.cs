using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository _offersRepository;

        public OffersService(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }
        [Authorize]
        public void Create(Offer offer)
        {
            _offersRepository.Create(offer);
        }

        public StatusModel Delete(int offerId)
        {
            var response = new StatusModel();
            var offer = _offersRepository.GetById(offerId);
            if (offer == null)
            {
                response.IsSuccessful = false;
                response.Message = $"There is no offer with Id {offerId}";
                return response;
            }
            _offersRepository.Delete(offer);
            response.Message = $"The offer with id {offerId} has been successfully deleted.";
            return response;
        }

        public List<Offer> GetAllOffersForUser(string userId)
        {
            return _offersRepository.GetAllOffersForUser(userId); ;
        }

        public List<Offer> GetAllPublicWithFilter(string title, bool isProffesional)
        {
            return _offersRepository.GetAllPublicWithFilter(title, isProffesional);
        }

        public Offer GetById(int offerId)
        {
            return _offersRepository.GetById(offerId);
        }

        public StatusModel Update(Offer offer)
        {
            var response = new StatusModel();
            var offerFromDB = _offersRepository.GetById(offer.Id);
            if (offerFromDB == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The offer with ID {offer.Id} is not found.";
            }
            else
            {
                offerFromDB.Description = offer.Description;
                offerFromDB.ImageUrl = offer.ImageUrl;
                offerFromDB.ImportantNote = offer.ImportantNote;
                offerFromDB.IsProfessional = offer.IsProfessional;
                offerFromDB.IsPublic = offer.IsPublic;
                offerFromDB.Title = offer.Title;
                offerFromDB.ValidUntil = offer.ValidUntil;
                offerFromDB.DateModified = DateTime.Now;

                _offersRepository.Update(offerFromDB);
                response.Message = $"The offer with ID {offerFromDB.Id} has been successfully updated.";
            }
            return response;
        }
    }
}
