using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class CarpoolPassengerAcceptancesService : ICarpoolPassengerAcceptancesService
    {
        private readonly ICarpoolPassengerAcceptancesRepository _carpoolPassengerAcceptancesRepository;

        public CarpoolPassengerAcceptancesService(ICarpoolPassengerAcceptancesRepository carpoolPassengerAcceptancesRepository)
        {
            _carpoolPassengerAcceptancesRepository = carpoolPassengerAcceptancesRepository;
        }
    }
}
