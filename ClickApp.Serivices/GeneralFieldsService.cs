using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class GeneralFieldsService : IGeneralFieldsService
    {

        private readonly IGeneralFieldsRepository _generalFieldsRepository;

        public GeneralFieldsService(IGeneralFieldsRepository generalFieldsRepository)
        {
            _generalFieldsRepository = generalFieldsRepository;
        }

        public StatusModel Create(GeneralField generalField)
        {
            var response = new StatusModel();
            var generalFields = _generalFieldsRepository.GetAll();

            var checkIfExists = generalFields.Any(x => x.Name.ToLower() == generalField.Name.ToLower() && x.Code == generalField.Code);

            if (checkIfExists)
            {
                response.IsSuccessful = false;
                response.Message = $"The general field {generalField.Name} already exists.";
                return response;
            }
            generalField.Skills = new List<Skill>();
            generalField.Interests = new List<Interest>();

            _generalFieldsRepository.Create(generalField);
            return response;
        }

        public StatusModel Delete(int id)
        {
            var response = new StatusModel();
            var generalField = _generalFieldsRepository.GetById(id);
            if (generalField == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The general field with ID {id} is not found.";
            }
            else
            {
                _generalFieldsRepository.Delete(generalField);
                response.Message = $"The general field with ID {id} has been successfully deleted.";
            }
            return response;
        }

        public List<GeneralField> GetAll()
        {
            return _generalFieldsRepository.GetAll();
        }

        public GeneralField GetById(int id)
        {
            return _generalFieldsRepository.GetById(id);
        }

        public List<GeneralField> GetAllWithFilter(string name, GeneralFieldCode codeSearch)
        {
            return _generalFieldsRepository.GetAllWithFilter(name, codeSearch);
        }

        public StatusModel Update(GeneralField generalField)
        {
            var response = new StatusModel();
            var generalFieldFromDb = _generalFieldsRepository.GetById(generalField.Id);
            if (generalFieldFromDb == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The general field with ID {generalField.Id} is not found.";
            }
            else
            {
                generalFieldFromDb.Name = generalField.Name;
                _generalFieldsRepository.Update(generalFieldFromDb);
                response.Message = $"The general field with ID {generalField.Id} has been successfully updated.";
            }
            return response;
        }
    }
}
