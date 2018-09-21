using System;
using Stripe;

namespace StripeAPI_Prototype.Controllers
{
    public class StripeController
    {
        private StripeController instance = null;

        private StripeController()
        {
        }

        public StripeController getInstance(){
            if(instance==null){
                instance = new StripeController();
            }
            return instance;
        }





    }
}
