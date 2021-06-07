using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class UserSkillsRepository : IUserSkillsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public UserSkillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<UserSkill> GetAllSkillsForUser(string userId)
        {
            return _context.UserSkills.Where(x=>x.UserId == userId).ToList();
        }
    }
}
