using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeAPI_Prototype.Classes
{
    public class StripeServiceParams
    {
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string planId { get; set; }
        public string planSize { get; set; }
        public string cNumber { get; set; }
        public string expirationYear { get; set; }
        public string expirationMonth { get; set; }
        public string cvc { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string paymentMethod { get; set; }
        public string invoiceText { get; set; }
        public string hospitalName { get; set; }
        public bool isDummy { get; set; }
        public string nameOnCard { get; set; }

        public StripeServiceParams()
        {
            userFirstName = "";
            userLastName = "";
            userEmail = "";
            userPhone = "";
            planId = "";
            planSize = "";
            cNumber = "";
            expirationYear = "";
            expirationMonth = "";
            cvc = "";
            addressLine1 = "";
            addressLine2 = "";
            city = "";
            state = "";
            country = "";
            zip = "";
            paymentMethod = "";
            invoiceText = "";
            hospitalName = "";
            nameOnCard = "";
            isDummy = false;
        }


        public bool isValidToCreateSubscription()
        {
            return (paymentMethod.ToLower() == "cc") ? userFirstName != "" && userLastName != "" && userEmail != "" && planId != "" && planSize != "" && cNumber != "" && expirationYear != "" && expirationMonth != "" && cvc != "" && addressLine1 != "" && city != "" && state != "" && country != "" && zip != "" && paymentMethod != "" && hospitalName != "" : userFirstName != "" && userLastName != "" && userEmail != "" && planId != "" && planSize != "" && addressLine1 != "" && city != "" && state != "" && country != "" && zip != "" && paymentMethod != "" && hospitalName != "";
        }

        public bool isValidToCancelSubscription()
        {
            return userEmail != "" && planId != "";
        }

    }
}
