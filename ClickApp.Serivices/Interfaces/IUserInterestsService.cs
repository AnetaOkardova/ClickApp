using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IUserInterestsService
    {
        List<UserInterest> GetAllSkillsForUser(string userId);
        UserInterest GetById(int id);
    }
}
