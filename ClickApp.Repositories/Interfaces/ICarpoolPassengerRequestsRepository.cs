using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface ICarpoolPassengerRequestsRepository : IBaseRepository<CarpoolPassengerRequest>
    {
        List<CarpoolPassengerRequest> GetAllValid(int id);
    }
}
