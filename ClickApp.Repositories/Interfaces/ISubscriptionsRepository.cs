using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface ISubscriptionsRepository : IBaseRepository<Subscription>
    {
        bool CheckIfExists(string subscriberEmail);
    }
}
