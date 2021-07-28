using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class UserInterestsRepository : IUserInterestsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public UserInterestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<UserInterest> GetAllSkillsForUser(string userId)
        {
            return _context.UserInterests.Where(x => x.UserId == userId).ToList();
        }

        public UserInterest GetById(int id)
        {
            return _context.UserInterests.FirstOrDefault(x => x.Id == id);
        }
    }
}
