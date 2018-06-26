using Microsoft.AspNetCore.Mvc;
using Mocean;

namespace MoceanASPCoreTest.Controllers
{
    public class VerifyController : Controller
    {
        [Route("api/Verify")]
        [HttpGet]
        public IActionResult Verify()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Verify.Verify _verification = new Mocean.Verify.Verify()
            {
                mocean_to = "60123456789",
                mocean_brand = "My App",

            };
            string response = Mocean.Verify.Client.Start(_verification, creds);
            return Ok(response);
        }

        [Route("api/CheckVerify")]
        [HttpGet]
        public IActionResult CheckVerify()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Verify.Verify _verification = new Mocean.Verify.Verify()
            {
                mocean_reqid = "req0123456789",
                mocean_code = "1234",

            };
            string response = Mocean.Verify.Client.Check(_verification, creds);
            return Ok(response);
        }
    }
}