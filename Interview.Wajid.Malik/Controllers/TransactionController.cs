using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Wajid.Malik.Controllers
{
    [Route("users/{userID}/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return new ObjectResult(null);
        }
    }
}