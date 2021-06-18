using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class InterestsRepository : BaseRepository<Interest>, IInterestsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public InterestsRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
}
