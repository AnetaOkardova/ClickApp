using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class UserInterestsService : IUserInterestsService
    {
        private readonly IUserInterestsRepository _userInterestsRepository;

        public UserInterestsService(IUserInterestsRepository userInterestsRepository)
        {
            _userInterestsRepository = userInterestsRepository;
        }
        public List<UserInterest> GetAllSkillsForUser(string userId)
        {
            return _userInterestsRepository.GetAllSkillsForUser(userId);
        }

        public UserInterest GetById(int id)
        {
            return _userInterestsRepository.GetById(id);
        }
    }
}
