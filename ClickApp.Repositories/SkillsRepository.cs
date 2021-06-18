using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class SkillsRepository : BaseRepository<Skill>, ISkillsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public SkillsRepository(ApplicationDbContext context) : base(context)
        {
        }

       
    }
}
