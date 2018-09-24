using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Stripe;
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

        public void Test1()
        {
            StripeConfiguration.SetApiKey("sk_test_YskwifolV97dD2Iu0v8YgDt5");
            //var req = this.HttpContext.Request.Form;
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            try
            {


                var tokenOptions = new StripeTokenCreateOptions()
                {
                    Card = new StripeCreditCardOptions()
                    {
                        Number = "4242424242424242",
                        ExpirationYear = 2019,
                        ExpirationMonth = 9,
                        Cvc = "123"
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
                    if (cus.Email == "aleira@codigodelsur.com")
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
                        Email = "aleiraa@codigodelsur.com",
                        SourceToken = stripeToken.Id
                    });

                }

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = 500,//charge in cents
                    Description = "Sample Charge",
                    Currency = "usd",
                    CustomerId = customer.Id

                });




                //  --------------------------------------------------------------------



                StripeSubscriptionService subscriptionSvc = new StripeSubscriptionService();
                //subscriptionSvc.Create(customer.Id, "EBSystems");

                var subscriptionOptions = new StripeSubscriptionUpdateOptions()
                {
                    PlanId = "testPlan",
                    Prorate = false,
                    TrialEnd = DateTime.Now.AddMinutes(2)
                };

                var subscriptionService = new StripeSubscriptionService();
                StripeSubscription subscription = subscriptionService.Update("sub_DfH8fv8g0MNQau", subscriptionOptions);


                //  --------------------------------------------------------------------



            }
            catch (Exception e)
            {
                string error = e.Message;
                //throw new Exception(error);
            }
        }

        public int CreateSubscription(string userMail, string planName)
        {

            StripeConfiguration.SetApiKey("sk_test_YskwifolV97dD2Iu0v8YgDt5");
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();


            var tokenOptions = new StripeTokenCreateOptions()
            {
                Card = new StripeCreditCardOptions()
                {
                    Number = "4242424242424242",
                    ExpirationYear = 2050,
                    ExpirationMonth = 9,
                    Cvc = "123"
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
                if (cus.Email == userMail)
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
                    Email = userMail,
                    SourceToken = stripeToken.Id
                });

            }



            var items = new List<StripeSubscriptionItemOption> {
              new StripeSubscriptionItemOption {PlanId = planName}
            };
            var subscriptionOptions = new StripeSubscriptionCreateOptions
            {
                Items = items,
                CustomerId = customer.Id
            };

            var subscriptionService = new StripeSubscriptionService();

            StripeSubscription subscription = subscriptionService.Create(subscriptionOptions);

            return 0;
        }


        public int CancelSubscription(string userMail,string plansId)
        {

            StripeConfiguration.SetApiKey("sk_test_YskwifolV97dD2Iu0v8YgDt5");


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
                if (cus.Email == userMail)
                {
                    found = true;
                    customer = cus;
                    break;
                }

            }

            if(found){


                var subscriptionService = new StripeSubscriptionService();
                StripeList<StripeSubscription> response = subscriptionService.List(new StripeSubscriptionListOptions
                {
                    Limit = 3333
                });


                found = false;

                var subscript = new StripeSubscription();

                foreach (StripeSubscription subs in response)
                {
                    if (subs.CustomerId == customer.Id && subs.StripePlan.Id == plansId)
                    {
                        found = true;
                        subscript = subs;
                        break;
                    }

                }

                if(found){
                    StripeSubscription subscription = subscriptionService.Cancel(subscript.Id, new StripeSubscriptionCancelOptions());
                }
                    


            }




            return 0;

        }

    }


}


