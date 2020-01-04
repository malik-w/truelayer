using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Wajid.Malik.Models;
using Interview.Wajid.Malik.Services;
using Interview.Wajid.Malik.Services.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Interview.Wajid.Malik.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IAuthHttpClient authHttpClient;

        public HomeController(IAuthService authService, IAuthHttpClient authHttpClient)
        {
            this.authService = authService;
            this.authHttpClient = authHttpClient;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (authService.IsAuthenticated)
            {
                return RedirectToAction("Authenticated");
            }
            else
            {
                return Redirect(authService.AuthenticationUrl);
            }
        }

        [HttpGet("callback")]
        public async Task<ActionResult> Callback(string code, string state, string scope, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                var credentials = await authHttpClient.GetTokenAsync(code);
                return RedirectToAction("Authenticated");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet("authenticated")]
        public ActionResult Authenticated()
        {
            return Content("You have successfully authenticated.");
        }

        [HttpGet("error")]
        public ActionResult Error()
        {
            return Content("Authentication error.");
        }
    }
}
