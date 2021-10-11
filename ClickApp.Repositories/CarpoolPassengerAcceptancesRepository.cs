using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class CarpoolPassengerAcceptancesRepository : BaseRepository<CarpoolPassengerAcceptance>, ICarpoolPassengerAcceptancesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public CarpoolPassengerAcceptancesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<CarpoolPassengerAcceptance> GetAllValid(int id)
        {
            return _context.CarpoolPassengerAcceptances.Where(x => x.CarpoolOfferId == id && x.Valid == true).ToList();
        }
    }
}
