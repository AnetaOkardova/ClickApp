using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public SkillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Skill> GetAll()
        {
            return _context.Skills.ToList();
        }

        public bool CheckIfExists(string name)
        {
            return _context.Skills.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public void Create(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }

        public Skill GetById(int id)
        {
            return _context.Skills.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Skill skill)
        {
            _context.Skills.Remove(skill);
            _context.SaveChanges();
        }

        public void Update(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
        }
    }
}
