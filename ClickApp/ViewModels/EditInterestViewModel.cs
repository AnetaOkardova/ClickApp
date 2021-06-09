using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class EditInterestViewModel
    {
        public InterestViewModel Interest { get; set; }
        public List<GeneralFieldViewModel> GeneralFields { get; set; }
    }
}
