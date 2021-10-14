using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CreateMessageViewModel
    {
        [Required]
        public string UserFromId { get; set; }
        [Required]
        public string UserToId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
