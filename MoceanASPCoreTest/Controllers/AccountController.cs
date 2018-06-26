using Microsoft.AspNetCore.Mvc;
using Mocean;

namespace MoceanASPCoreTest.Controllers
{
    public class AccountController : Controller
    {
        [Route("api/CheckPricing")]
        [HttpGet]
        public IActionResult CheckPricing()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Account.Pricing _pricing = new Mocean.Account.Pricing()
            {
                mocean_resp_format = "json",
                mocean_mcc = "502",
                mocean_mnc = "01",
                mocean_delimiter = ";",

            };

            string response = Mocean.Account.Client.GetPricing(_pricing, creds);
            return Ok(response);
        }

        [Route("api/CheckBalance")]
        [HttpGet]
        public IActionResult CheckBalance()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Account.Balance _balance = new Mocean.Account.Balance()
            {
                mocean_resp_format = "json"
            };

            string response = Mocean.Account.Client.GetBalance(_balance, creds);
            return Ok(response);
        }
    }
}