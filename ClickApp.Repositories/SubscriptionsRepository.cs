using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class SubscriptionsRepository : BaseRepository<Subscription>, ISubscriptionsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public SubscriptionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool CheckIfExists(string subscriberEmail)
        {
            var allSubscriptions = GetAll();
            if (allSubscriptions.Count != 0)
            {
            return allSubscriptions.Any(x=>x.Email.Trim().ToLower() == subscriberEmail.Trim().ToLower());
            }
            return false;
        }
    }
}
