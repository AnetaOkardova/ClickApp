using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface ISubscriptionsService
    {
        StatusModel Create(string subscriberEmail);
    }
}
