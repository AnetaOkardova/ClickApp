using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private ApplicationDbContext _context { get; set; }
        public OffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Offer> GetAllPublicWithFilter(string title, bool isProffesional)
        {
            var query = _context.Offers.Include(x => x.User).Where(x => x.IsPublic == true).AsQueryable();
            if (title != null)
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            if (isProffesional != null)
            {
                query = query.Where(x => x.IsProfessional == isProffesional);
            }

            var offers = query.ToList();
            return offers;
        }

        public void Create(Offer offer)
        {
            _context.Offers.Add(offer);
            _context.SaveChanges();
        }
    }
}
