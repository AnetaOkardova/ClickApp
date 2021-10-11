using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class CarpoolPassengerRequestsRepository : BaseRepository<CarpoolPassengerRequest>, ICarpoolPassengerRequestsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public CarpoolPassengerRequestsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
