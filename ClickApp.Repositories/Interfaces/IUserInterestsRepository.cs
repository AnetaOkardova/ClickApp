using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IUserInterestsRepository
    {
        List<UserInterest> GetAllSkillsForUser(string userId);

    }
}
