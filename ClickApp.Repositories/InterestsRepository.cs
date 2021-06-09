using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class InterestsRepository : IInterestsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public InterestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CheckIfExists(string name)
        {
            return _context.Interests.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public void Create(Interest interest)
        {
            _context.Interests.Add(interest);
            _context.SaveChanges();
        }

        public void Delete(Interest interest)
        {
            _context.Interests.Remove(interest);
            _context.SaveChanges();
        }

        public List<Interest> GetAll()
        {
            return _context.Interests.ToList();
        }

        public Interest GetById(int id)
        {
            return _context.Interests.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Interest interest)
        {
            _context.Interests.Update(interest);
            _context.SaveChanges();
        }
    }
}
