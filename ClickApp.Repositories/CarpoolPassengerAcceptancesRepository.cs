using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class CarpoolPassengerAcceptancesRepository : BaseRepository<CarpoolPassengerAcceptance>, ICarpoolPassengerAcceptancesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public CarpoolPassengerAcceptancesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
