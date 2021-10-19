using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateSubscribed { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
