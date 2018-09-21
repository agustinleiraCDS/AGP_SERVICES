using Stripe;

namespace StripeAPI_Prototype.Services
{
    /// <summary>
    /// Interface to provide direct communication with the Stripe Api
    /// </summary>
    public interface IStripeService
    {
        StripePlanService PlanService { get; }
        StripeSubscriptionService SubscriptionService { get; }
        StripeCustomerService CustomerService { get; }
        StripeCardService CardService { get; }
        StripeChargeService ChargeService { get; }
        StripeInvoiceService InvoiceService { get; }
        StripeInvoiceItemService InvoiceItemService { get; }
        StripeTokenService TokenService { get; }
        StripeRefundService RefundService { get; }
    }
}
