using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stripe;
using StripeAPI_Prototype.Classes;

namespace StripeAPI_Prototype.Controllers
{
    public class StripeController
    {

        public StripeController()
        {
        }

        
        public int makePOST(JObject param,string sURL)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
            

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                               
                string json = param.ToString();

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return 0;
        }




        public int CreateSubscription(StripeServiceParams info)
        {
            int cres;
            if (info.isValidToCreateSubscription())
            {
                if (info.paymentMethod.ToLower() == "cc")
                {
                    //StripeConfiguration.SetApiKey(Environment.GetEnvironmentVariable("stripePrivTok"));
                    //StripeConfiguration.SetApiKey("sk_test_sjbGjYh6HKH6xSGnu1F3XWqG");
                    StripeConfiguration.SetApiKey(Utils.getInstance().stripePrivTok);
                    var customers = new StripeCustomerService();
                    var charges = new StripeChargeService();
                   
                        var tokenOptions = new StripeTokenCreateOptions()
                        {
                            Card = new StripeCreditCardOptions()
                            {
                                Number = info.cNumber,
                                ExpirationYear = Int32.Parse(info.expirationYear),
                                ExpirationMonth = Int32.Parse(info.expirationMonth),
                                Cvc = info.cvc,
                                Name = info.nameOnCard,
                                AddressLine1 = info.addressLine1,
                                AddressLine2 = info.addressLine2,
                                AddressCountry = info.country,
                                AddressCity = info.city,
                                AddressZip = info.zip,
                                AddressState = info.state,
                            }
                        };
                        var tokenService = new StripeTokenService();
                        StripeToken stripeToken = tokenService.Create(tokenOptions);
                        var customerService = new StripeCustomerService();
                        StripeList<StripeCustomer> customerItems = customerService.List(
                          new StripeCustomerListOptions()
                          {
                              Limit = 300,
                              Email = info.userEmail
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
                                SourceToken = stripeToken.Id,

                            });
                        }
                        var items = new List<StripeSubscriptionItemOption> {
                          new StripeSubscriptionItemOption {PlanId = info.planId}
                        };
                        var subscriptionOptions = new StripeSubscriptionCreateOptions
                        {
                            Items = items,
                            CustomerId = customer.Id,

                        };
                        var subscriptionService = new StripeSubscriptionService();
                        StripeSubscription subscription = subscriptionService.Create(subscriptionOptions);
                }


                JObject param = new JObject
                {
                    { "mycurrentFunction", "submitPayment" }
                };

                JObject data = new JObject();
                data.Add("plan_id", info.planId);
                data.Add("plan_size", info.planSize);
                data.Add("firstName", info.userFirstName);
                data.Add("lastName", info.userLastName);
                data.Add("Email", info.userEmail);
                data.Add("Phone", info.userPhone);
                data.Add("Address", info.addressLine1 + " " + info.addressLine2);
                data.Add("City", info.city);
                data.Add("State", info.state);
                data.Add("Zip", info.state);
                data.Add("payment_method", info.paymentMethod);
                data.Add("invoice_text", info.invoiceText);
                data.Add("hospitalName", info.hospitalName);
                data.Add("description", (info.paymentMethod.ToLower() == "cc") ? "cc" : "invoice");
                data.Add("currentAmount", 10); 
                

                param.Add("data", data);

                string sURL = "";
                if (info.paymentMethod.ToLower() == "cc")
                {
                    //sURL = Environment.GetEnvironmentVariable("mycurrentDomain") + Environment.GetEnvironmentVariable("purchaseMethodURL");
                    sURL = "https://www.thesullivangroup.com/RSQSolutions/ASP/services_purchase_subscription.asp";
                    //sURL = Utils.getInstance().mycurrentDomain + "/RSQSolutions/ASP/services_purchase_subscription.asp";
                }
                else if (info.paymentMethod.ToLower() == "invoice")
                {
                    //sURL = Environment.GetEnvironmentVariable("invoiceServiceURL");
                    //sURL = "https://hooks.zapier.com/hooks/catch/3912961/llfs9y/";
                    sURL = Utils.getInstance().invoiceServiceURL;
                    cres = makePOST(param, sURL);
                    sURL = "https://www.thesullivangroup.com/RSQSolutions/ASP/services_purchase_subscription.asp";
                    //sURL = Utils.getInstance().mycurrentDomain + "/RSQSolutions/ASP/services_purchase_subscription.asp";
                }
               cres = makePOST(param, sURL);

            }
            else
            {
                return -1;
            }
            return 0;
        }


        public int CancelSubscription(StripeServiceParams info)
        {
            //StripeConfiguration.SetApiKey(Environment.GetEnvironmentVariable("stripePrivTok"));
            //StripeConfiguration.SetApiKey("sk_test_sjbGjYh6HKH6xSGnu1F3XWqG");
            StripeConfiguration.SetApiKey(Utils.getInstance().stripePrivTok);
            if (info.isValidToCancelSubscription())
            {
                var customerService = new StripeCustomerService();
                StripeList<StripeCustomer> customerItems = customerService.List(
                  new StripeCustomerListOptions()
                  {
                      Limit = 300,
                      Email = info.userEmail
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
                        Limit = 3333,
                        PlanId = info.planId
                    });
                    found = false;
                    var subscript = new StripeSubscription();
                    foreach (StripeSubscription subs in response)
                    {
                        if (subs.CustomerId == customer.Id && subs.StripePlan.Id == info.planId)
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


        public JObject GetCustomerInformation(string email)
        {
            StripeConfiguration.SetApiKey(Utils.getInstance().stripePrivTok);
            var customers = new StripeCustomerService();

            var customerService = new StripeCustomerService();
            StripeList<StripeCustomer> customerItems = customerService.List(
              new StripeCustomerListOptions()
              {
                  Limit = 300,
                  Email = email
              }
            );
            bool found = false;
            var customer = new StripeCustomer();
            foreach (StripeCustomer cus in customerItems)
            {
                if (cus.Email == email)
                {
                    found = true;
                    customer = cus;
                    break;
                }
            }


            var service = new StripeCardService();
            StripeCard card = service.Get(customer.Id, customer.DefaultSourceId);

            JObject res = new JObject();
            JObject cardInfo = new JObject();

            cardInfo.Add("Country", card.Country);
            cardInfo.Add("Brand", card.Brand);
            cardInfo.Add("AddressZip", card.AddressZip);
            cardInfo.Add("AddressState", card.AddressState);
            cardInfo.Add("AddressLine2", card.AddressLine2);
            cardInfo.Add("AddressLine1", card.AddressLine1);
            cardInfo.Add("AddressCountry", card.AddressCountry);
            cardInfo.Add("AddressCity", card.AddressCity);
            cardInfo.Add("ExpirationMonth", card.ExpirationMonth);
            cardInfo.Add("ExpirationYear", card.ExpirationYear);
            cardInfo.Add("Last4", card.Last4);

            res.Add("card", cardInfo);

            return res;
        }


        public int UpdateStripeCustomerInformation(StripeServiceParams updatedInfo)
        {
            StripeConfiguration.SetApiKey(Utils.getInstance().stripePrivTok);
            var customers = new StripeCustomerService();
          
            var customerService = new StripeCustomerService();
            StripeList<StripeCustomer> customerItems = customerService.List(
              new StripeCustomerListOptions()
              {
                  Limit = 300,
                  Email = updatedInfo.userEmail
              }
            );
            bool found = false;
            var customer = new StripeCustomer();
            foreach (StripeCustomer cus in customerItems)
            {
                if (cus.Email == updatedInfo.userEmail)
                {
                    found = true;
                    customer = cus;
                    break;
                }
            }
            if (found)
            {

                var tokenOptions = new StripeTokenCreateOptions();

                var myCustomer = new StripeCustomerUpdateOptions();

                var options = new StripeTokenCreateOptions();
                options.Card = new StripeCreditCardOptions();

                // setting up the card
         
                if (updatedInfo.cNumber != "")
                {
                    options.Card.Number = updatedInfo.cNumber;
                }
                if (updatedInfo.expirationYear != "")
                {
                    options.Card.ExpirationYear = Int32.Parse(updatedInfo.expirationYear);
                }
                if (updatedInfo.expirationMonth != "")
                {
                    options.Card.ExpirationMonth = Int32.Parse(updatedInfo.expirationMonth);
                }
                if (updatedInfo.country != "")
                {
                    options.Card.AddressCountry = updatedInfo.country;
                }
                if (updatedInfo.addressLine1 != "")
                {
                    options.Card.AddressLine1 = updatedInfo.addressLine1;
                }
                if (updatedInfo.addressLine2 != "")
                {
                    options.Card.AddressLine2 = updatedInfo.addressLine2;
                }
                if (updatedInfo.city != "")
                {
                    options.Card.AddressCity = updatedInfo.city;
                }
                if (updatedInfo.state != "")
                {
                    options.Card.AddressState = updatedInfo.state;
                }
                if (updatedInfo.zip != "")
                {
                    options.Card.AddressZip = updatedInfo.zip;
                }
                if (updatedInfo.userFirstName != "" && updatedInfo.userLastName != "")
                {
                    options.Card.Name = updatedInfo.userFirstName + " " + updatedInfo.userLastName;
                }
                customerService = new StripeCustomerService();

                var service = new StripeTokenService();
                StripeToken stripeToken = service.Create(options);

                myCustomer.SourceToken = stripeToken.Id;

                StripeCustomer stripeCustomer = customerService.Update(customer.Id, myCustomer);
            }
            return 0;
        }
    }
}
