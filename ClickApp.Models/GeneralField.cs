using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class GeneralField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<Interest>? Interests { get; set; }

    }
}
