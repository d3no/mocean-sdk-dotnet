using System;
using System.Windows.Forms;
using Mocean;

namespace MoceanTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Send MT Message Button Controller
        private void SendMessage(object sender, EventArgs e)
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
                mocean_resp_format = "json"
            };

            string response = Mocean.Message.Client.Send(_message, creds);
            Console.WriteLine(response);
        }

        // Send Flash Message Button Controller
        private void SendFlashMessage(object sender, EventArgs e)
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
            Console.WriteLine(response);
        }

        // Search Message Button Controller
        private void SearchMessageStatus(object sender, EventArgs e)
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
            Console.WriteLine(response);
        }

        // Check Pricing Button Controller
        private void CheckPricing(object sender, EventArgs e)
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
            Console.WriteLine(response);
        }

        // Check Balance Button Controller
        private void CheckBalance(object sender, EventArgs e)
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
            Console.WriteLine(response);
        }

        private void Verification(object sender, EventArgs e)
        {
            Credentials creds = new Credentials
            {
                mocean_api_key = "MOCEAN_API_KEY",
                mocean_api_secret = "MOCEAN_API_SECRET"
            };

            Mocean.Verify.Verify _verification = new Mocean.Verify.Verify()
            {
                mocean_to = "60123456789",
                mocean_brand = "My App"

            };
            string response = Mocean.Verify.Client.Start(_verification, creds);
            Console.WriteLine(response);
        }

        private void CheckVerify(object sender, EventArgs e)
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
            Console.WriteLine(response);
        }
    }
}
