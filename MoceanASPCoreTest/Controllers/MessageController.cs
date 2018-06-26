using Microsoft.AspNetCore.Mvc;
using Mocean;
using System.Diagnostics;

namespace MoceanASPCoreTest.Controllers
{
    public class MessageController : Controller
    {
        [Route("api/SendMessage")]
        [HttpGet]
        public IActionResult SendMessage()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Message.Message _message = new Mocean.Message.Message()
            {
                mocean_to = "60123456789",
                mocean_from = " MOCEAN",
                mocean_text = "Hello World",
                mocean_resp_format = "json",

            };

            string response = Mocean.Message.Client.Send(_message, creds);
            return Ok(response);
        }

        [Route("api/SendFlashMessage")]
        [HttpGet]
        public IActionResult SendFlashMessage()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Message.Message _message = new Mocean.Message.Message()
            {
                mocean_to = "60123456789",
                mocean_from = "MOCEAN",
                mocean_text = "Hello World",
                mocean_resp_format = "json",
                mocean_mclass = "1"
            };

            string response = Mocean.Message.Client.Send(_message, creds);
            return Ok(response);
        }

        [Route("api/SearchMessageStatus")]
        [HttpGet]
        public IActionResult SearchMessageStatus()
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Message.Message _message = new Mocean.Message.Message()
            {
                mocean_resp_format = "json",
                mocean_msgid = "cust0123456789"

            };

            string response = Mocean.Message.Client.Search(_message, creds);
            return Ok(response);
        }

        [Route("api/ReceiveDlr")]
        public void ReceiveDLR()
        {
            Debug.WriteLine("-------------------------------------------------------------------------");
            Debug.WriteLine("DELIVERY RECEIPT");
            Debug.WriteLine("mocean-msgid: " + Request.Form["mocean-msgid"]);
            Debug.WriteLine("mocean-from: " + Request.Form["mocean-from"]);
            Debug.WriteLine("mocean-to: " + Request.Form["mocean-to"]);
            Debug.WriteLine("mocean-dlr-status: " + Mocean.Message.Client.DLRStatus(Request.Form["mocean-dlr-status"]));
            Debug.WriteLine("mocean-error-code: " + Request.Form["mocean-error-code"]);
            Debug.WriteLine("-------------------------------------------------------------------------");
        }
    }
}