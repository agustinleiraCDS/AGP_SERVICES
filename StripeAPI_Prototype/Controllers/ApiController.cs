using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StripeAPI_Prototype.Classes;

namespace StripeAPI_Prototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class V1Controller : ControllerBase
    {

        // Get api/V1/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now, description = "Service running properly" });
        }


        // POST api/V1/CreateSubscription
        [HttpPost("CreateSubscription/")]
        public IActionResult CreateSubscription([FromBody] StripeServiceParams param)
        {
            try
            {
                int res = (new StripeController()).CreateSubscription(param);
                if (res == -1)
                {
                    return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = "Invalid arguments" });
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message });
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }

        // POST api/V1/CancelSubscription
        [HttpPost("CancelSubscription/")]
        public IActionResult CancelSubscription([FromBody] StripeServiceParams param)
        {
            try
            {
                int res = (new StripeController()).CancelSubscription(param);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }


        // POST api/V1/NotifySubscription
        [HttpPost("NotifySubscription/")]
        public IActionResult NotifySubscription([FromBody] NotificationParams email)
        {
            try
            {
                JObject param = new JObject();
                param.Add("email", email.email);
                int res = (new NewsLetterSubscription()).UseWebHook(param);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }

        // POST api/V1/UpdateStripeCustomer
        [HttpPost("UpdateStripeCustomer/")]
        public IActionResult UpdateStripeCustomer([FromBody] StripeServiceParams updatedInfo)
        {
            try
            {

                int res = (new StripeController()).UpdateStripeCustomerInformation(updatedInfo);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }

        // Post api/V1/GetCustomerInformation
        [HttpPost("GetCustomerInformation/")]
        public IActionResult GetCustomerInformation([FromBody] NotificationParams param)
        {
            JObject res = new JObject();
            try
            {

                res = (new StripeController()).GetCustomerInformation(param.email);

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now, data = res });
        }
        

    }
}