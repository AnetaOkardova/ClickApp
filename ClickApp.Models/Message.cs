using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UserFromId { get; set; }
        public string UserToId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool SeenStatus { get; set; }
    }
}
