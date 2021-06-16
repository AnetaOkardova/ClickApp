﻿using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class GeneralFieldViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public GeneralFieldCode Code { get; set; }
    }
}
