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
    public class SkillsService : ISkillsService
    {

        private readonly ISkillsRepository _skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public List<Skill> GetAll()
        {
            return _skillsRepository.GetAll();
        }
        public StatusModel Create(Skill skill)
        {
            var response = new StatusModel();

            var checkIfExists = _skillsRepository.GetAll().Any((x => x.Name.ToLower() == skill.Name.ToLower()));

            if (checkIfExists)
            {
                response.IsSuccessful = false;
                response.Message = $"The skill {skill.Name} already exists.";
                return response;
            }
            skill.Users = new List<UserSkill>();

            _skillsRepository.Create(skill);
            return response;
        }

        public StatusModel Delete(int id)
        {
            var response = new StatusModel();
            var skill = _skillsRepository.GetById(id);
            if (skill == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The skill with ID {id} is not found.";
            }
            else
            {
                _skillsRepository.Delete(skill);
                response.Message = $"The skill with ID {id} has been successfully deleted.";
            }
            return response;
        }

        public Skill GetById(int id)
        {
            return _skillsRepository.GetById(id);
        }

        public StatusModel Update(Skill skill)
        {
            var response = new StatusModel();
            var skillFromDB = _skillsRepository.GetById(skill.Id);
            if (skillFromDB == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The skill with ID {skill.Id} is not found.";
            }
            else
            {
                skillFromDB.Name = skill.Name;
                skillFromDB.GeneralFieldId = skill.GeneralFieldId;
                skillFromDB.GeneralField = skill.GeneralField;
                skillFromDB.Users = skill.Users;
                _skillsRepository.Update(skillFromDB);
                response.Message = $"The skill with ID {skill.Id} has been successfully updated.";
            }
            return response;
        }
    }
}
