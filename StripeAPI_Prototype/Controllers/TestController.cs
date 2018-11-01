using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Stripe;
using StripeAPI_Prototype.Classes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StripeAPI_Prototype.Controllers
{
    public class TestController
    {

        public int CreateSubscription(StripeServiceParams info)
        {

            StripeConfiguration.SetApiKey("sk_test_YskwifolV97dD2Iu0v8YgDt5");
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();
            if (info.isValidToCreateSubscription())
            {
                var tokenOptions = new StripeTokenCreateOptions()
                {
                    Card = new StripeCreditCardOptions()
                    {
                        Number = info.number,
                        ExpirationYear = Int32.Parse(info.expirationYear),
                        ExpirationMonth = Int32.Parse(info.expirationMonth),
                        Cvc = info.cvc
                    }
                };
                var tokenService = new StripeTokenService();
                StripeToken stripeToken = tokenService.Create(tokenOptions);
                var customerService = new StripeCustomerService();
                StripeList<StripeCustomer> customerItems = customerService.List(
                  new StripeCustomerListOptions()
                  {
                      Limit = 300
                  }
                );
                bool found = false;
                var customer = new StripeCustomer();
                foreach (StripeCustomer cus in customerItems)
                {
                    if (cus.Email == info.userEmail)
                    {
                        found = true;
                        customer = cus;
                        break;
                    }
                }
                if (!found)
                {
                    customer = customers.Create(new StripeCustomerCreateOptions
                    {
                        Email = info.userEmail,
                        SourceToken = stripeToken.Id
                    });
                }
                var items = new List<StripeSubscriptionItemOption> {
                  new StripeSubscriptionItemOption {PlanId = info.planName}
                };
                var subscriptionOptions = new StripeSubscriptionCreateOptions
                {
                    Items = items,
                    CustomerId = customer.Id
                };
                var subscriptionService = new StripeSubscriptionService();
                StripeSubscription subscription = subscriptionService.Create(subscriptionOptions);
            }
            return 0;
        }


        public int CancelSubscription(StripeServiceParams info)
        {
            StripeConfiguration.SetApiKey("sk_test_YskwifolV97dD2Iu0v8YgDt5");
            if (info.isValidToCancelSubscription())
            {
                var customerService = new StripeCustomerService();
                StripeList<StripeCustomer> customerItems = customerService.List(
                  new StripeCustomerListOptions()
                  {
                      Limit = 300
                  }
                );
                bool found = false;
                var customer = new StripeCustomer();
                foreach (StripeCustomer cus in customerItems)
                {
                    if (cus.Email == info.userEmail)
                    {
                        found = true;
                        customer = cus;
                        break;
                    }
                }
                if (found)
                {
                    var subscriptionService = new StripeSubscriptionService();
                    StripeList<StripeSubscription> response = subscriptionService.List(new StripeSubscriptionListOptions
                    {
                        Limit = 3333
                    });
                    found = false;
                    var subscript = new StripeSubscription();
                    foreach (StripeSubscription subs in response)
                    {
                        if (subs.CustomerId == customer.Id && subs.StripePlan.Id == info.planName)
                        {
                            found = true;
                            subscript = subs;
                            break;
                        }
                    }
                    if (found)
                    {
                        StripeSubscription subscription = subscriptionService.Cancel(subscript.Id, new StripeSubscriptionCancelOptions());
                    }
                }
            }
            return 0;
        }

    }


}


