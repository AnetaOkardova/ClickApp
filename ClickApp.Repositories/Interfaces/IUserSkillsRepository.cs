using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IUserSkillsRepository
    {
        List<UserSkill> GetAllSkillsForUser(string userId);
        UserSkill GetById(int id);
    }
}
