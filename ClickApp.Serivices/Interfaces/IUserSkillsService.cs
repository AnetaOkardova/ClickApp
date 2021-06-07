using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IUserSkillsService
    {
        List<UserSkill> GetAllSkillsForUser(string userId);
    }
}
