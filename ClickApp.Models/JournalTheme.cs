﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class JournalTheme
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
    }
}
