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
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        /*[HttpPost]
        public void Post([FromBody] string value)
        {
        }*/

        // POST api/values
        [HttpPost("PrimerPost/")]
        public IActionResult PrimerPost([FromForm] User param)
        {
            TestController pepe = new TestController();
            int res = pepe.CreateSubscription("aleira@codigodelsur.com", "testPlan");
            return  Ok("{value:\"primer post "+ param.toString() + "\" }");
        }

        // POST api/values
        [HttpPost("CancelSubscription/")]
        public IActionResult CancelSubscription([FromForm] User param)
        {
            TestController pepe = new TestController();
            int res = pepe.CancelSubscription("aleira@codigodelsur.com", "testPlan");
            return Ok("{value:\"CancelSubscription " + param.toString() + "\" }");
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
