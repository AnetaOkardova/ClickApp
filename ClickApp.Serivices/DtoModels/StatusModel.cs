using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Serivices.DtoModels
{
    public class StatusModel
    {
        public StatusModel()
        {
            IsSuccessful = true;
        }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
