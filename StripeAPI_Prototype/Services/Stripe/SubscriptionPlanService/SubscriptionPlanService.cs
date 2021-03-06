﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe;

namespace StripeAPI_Prototype.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private IStripeService _stripe;
        public SubscriptionPlanService(IStripeService stripe)
        {
            _stripe = stripe;
        }

        public async Task<StripePlan> CreatePlan(string name, int amount, string colour, string description, string features, string currency = "gbp", string interval = "month", int intervalCount = 1, int trialPeriodDays = 30)
        {
            var myPlan = new StripePlanCreateOptions()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,                     // all amounts on Stripe are in cents, pence, etc
                Currency = currency,                 // "usd" only supported right now
                Interval = interval,                 // "month" or "year"
                IntervalCount = intervalCount,       // optional
                Nickname = name,
                TrialPeriodDays = trialPeriodDays,   // amount of time that will lapse before the customer is billed
                Product = new StripePlanProductCreateOptions()
                {
                    Name = name
                }
            };
            myPlan.Metadata.Add("Colour", colour);
            myPlan.Metadata.Add("Description", description);
            myPlan.Metadata.Add("Features", features);
            StripePlan response = await _stripe.PlanService.CreateAsync(myPlan);
            return response;
        }
        public void DeletePlan(string planId)
        {
            _stripe.PlanService.DeleteAsync(planId);
        }
        public async Task<StripePlan> FindByIdAsync(string planId)
        {
            StripePlan response = await _stripe.PlanService.GetAsync(planId);
            return response;
        }
        public async Task<IEnumerable<StripePlan>> GetAllAsync()
        {
            var stripeSubs = await _stripe.PlanService.ListAsync();
            return stripeSubs;
        }
    }
}
