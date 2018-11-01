using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StripeAPI_Prototype.Classes;

namespace StripeAPI_Prototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value333" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("CreateSubscription/")]
        public IActionResult CreateSubscription([FromForm] StripeServiceParams param)
        {

            try
            {
                int res = StripeController.getInstance().CreateSubscription(param);

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }

        // POST api/values
        [HttpPost("CancelSubscription/")]
        public IActionResult CancelSubscription([FromForm] StripeServiceParams param)
        {
            try
            {
                int res = StripeController.getInstance().CancelSubscription(param);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now, description = e.Message }); ;
            }

            return new ObjectResult(new { message = "200 Ok", currentDate = DateTime.Now });
        }


        [HttpPost("SegundoPost/")]
        public ActionResult SegundoPost()
        {
            return Ok("{Este es el segundo test}");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
