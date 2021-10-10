using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class CarpoolOffersRepository : BaseRepository<CarpoolOffer>, ICarpoolOffersRepository
    {
        private ApplicationDbContext _context { get; set; }
        public CarpoolOffersRepository(ApplicationDbContext context): base(context)
        {
        }
    }
}
