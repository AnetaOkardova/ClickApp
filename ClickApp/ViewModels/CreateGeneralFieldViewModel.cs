﻿using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CreateGeneralFieldViewModel
    {
        [Required]
        public string Name { get; set; }
        public GeneralFieldCode Code { get; set; }

    }
}
