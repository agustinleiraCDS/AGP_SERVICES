using Stripe;

namespace StripeAPI_Prototype.Services
{
    public class StripeService : IStripeService
    {
        string StripeApiKey { get; set; }
        public StripeService()
        {
                StripeApiKey = "sk_test_YskwifolV97dD2Iu0v8YgDt5";
        }

        public StripePlanService PlanService
        {
            get
            {
                return new StripePlanService(StripeApiKey);
            }
        }

        public StripeSubscriptionService SubscriptionService
        {
            get
            {
                return new StripeSubscriptionService(StripeApiKey);
            }
        }

        public StripeCustomerService CustomerService
        {
            get
            {
                return new StripeCustomerService(StripeApiKey);
            }
        }

        public StripeCardService CardService
        {
            get
            {
                return new StripeCardService(StripeApiKey);
            }
        }

        public StripeChargeService ChargeService
        {
            get
            {
                return new StripeChargeService(StripeApiKey);
            }
        }

        public StripeInvoiceService InvoiceService
        {
            get
            {
                return new StripeInvoiceService(StripeApiKey);
            }
        }

        public StripeInvoiceItemService InvoiceItemService
        {
            get
            {
                return new StripeInvoiceItemService(StripeApiKey);
            }
        }

        public StripeTokenService TokenService
        {
            get
            {
                return new StripeTokenService(StripeApiKey);
            }
        }

        public StripeRefundService RefundService
        {
            get
            {
                return new StripeRefundService(StripeApiKey);
            }
        }
    }
}