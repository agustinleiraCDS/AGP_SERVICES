using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeAPI_Prototype.Classes
{
    public class Utils
    {
        private static Utils instance = null;

        public string stripePrivTok { get; set; }
        public string mycurrentDomain { get; set; }
        public string purchaseMethodURL { get; set; }
        public string newsLetterWebhookURL { get; set; }
        public string invoiceServiceURL { get; set; }

        private Utils()
        {
            stripePrivTok = "sk_test_sjbGjYh6HKH6xSGnu1F3XWqG";
            mycurrentDomain = "https://www.thesullivangroup.com";
            purchaseMethodURL = "/RSQSolutions/ASP/services_purchase_subscription.asp";
            //newsLetterWebhookURL = "https://hooks.zapier.com/hooks/catch/3912961/llfuft/";
            newsLetterWebhookURL = "https://hooks.zapier.com/hooks/catch/3976200/eh8617/";
            //invoiceServiceURL = "https://hooks.zapier.com/hooks/catch/3912961/llfs9y/";
            invoiceServiceURL = "https://hooks.zapier.com/hooks/catch/3976200/ehsxjk/";
        }


        public static Utils getInstance()
        {
            if(instance == null)
            {
                instance = new Utils();
            }
            return instance;

        }




    }
}
