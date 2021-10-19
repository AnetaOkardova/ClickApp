using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionsService(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public StatusModel Create(string subscriberEmail)
        {
            var response = new StatusModel();
            var IfExists = _subscriptionsRepository.CheckIfExists(subscriberEmail);
            if (!IfExists)
            {
                var subscription = new Subscription();
                subscription.Email = subscriberEmail;
                subscription.DateSubscribed = DateTime.Now;
                subscription.IsSubscribed = true;

                _subscriptionsRepository.Create(subscription);
                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"There is already a subscription with the email: {subscriberEmail}";
                return response;
            }
        }
    }
}
