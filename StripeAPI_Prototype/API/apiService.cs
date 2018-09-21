//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//namespace StripeAPI_Prototype.API
//{
//    [Produces("application/json")]
//    [Route("api/[controller]")]
//    public class apiService : ControllerBase
//    {

//    }

//}



using Microsoft.AspNetCore.Mvc;

namespace StripeAPI_Prototype.API
{
    #region snippet_ControllerSignature
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    public class apiService : ControllerBase
    {
        [HttpPost]
        public IActionResult PrimerPost() => Ok();
    }
    #endregion
}