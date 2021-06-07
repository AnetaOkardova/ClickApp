using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class UserSkillsService : IUserSkillsService
    {
        private readonly IUserSkillsRepository _userSkillsRepository;

        public UserSkillsService(IUserSkillsRepository userSkillsRepository)
        {
            _userSkillsRepository = userSkillsRepository;
        }
        public List<UserSkill> GetAllSkillsForUser(string userId)
        {
            return _userSkillsRepository.GetAllSkillsForUser(userId);
        }
    }
}
