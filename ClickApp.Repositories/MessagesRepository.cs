using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class MessagesRepository : BaseRepository<Message>, IMessagesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public MessagesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
