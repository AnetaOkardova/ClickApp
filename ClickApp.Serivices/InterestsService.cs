using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly IInterestsRepository _interestsRepository;

        public InterestsService(IInterestsRepository interestsRepository)
        {
            _interestsRepository = interestsRepository;
        }
        public StatusModel Create(Interest interest)
        {
            var response = new StatusModel();

            var checkIfExists = _interestsRepository.CheckIfExists(interest.Name);
            if (checkIfExists)
            {
                response.IsSuccessful = false;
                response.Message = $"The interest {interest.Name} already exists.";
                return response;
            }
            interest.Users = new List<UserInterest>();

            _interestsRepository.Create(interest);
            return response;
        }

        public StatusModel Delete(int id)
        {
            var response = new StatusModel();
            var interest = _interestsRepository.GetById(id);
            if (interest == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The interest with ID {id} is not found.";
            }
            else
            {
                _interestsRepository.Delete(interest);
                response.Message = $"The interest with ID {id} has been successfully deleted.";
            }
            return response;
        }

        public List<Interest> GetAll()
        {
            return _interestsRepository.GetAll();
        }

        public Interest GetById(int id)
        {
            return _interestsRepository.GetById(id);
        }

        public StatusModel Update(Interest interest)
        {
            var response = new StatusModel();
            var interestFromDB = _interestsRepository.GetById(interest.Id);
            if (interestFromDB == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The interest with ID {interest.Id} is not found.";
            }
            else
            {
                interestFromDB.Name = interest.Name;
                interestFromDB.GeneralFieldId = interest.GeneralFieldId;
                interestFromDB.GeneralField = interest.GeneralField;
                interestFromDB.Users = interest.Users;
                _interestsRepository.Update(interestFromDB);
                response.Message = $"The interest with ID {interest.Id} has been successfully updated.";
            }
            return response;
        }
    }
}
