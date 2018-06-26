using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Mocean.Account
{
    public class Client
    {
        public static string GetPricing(Pricing _pricing, Credentials creds)
        {
            string subdomain = "/account/pricing";
            var response = ApiRequest.DoGetRequest(subdomain, _pricing, creds);
            return response.HttpResponse;
        }

        public static string GetBalance(Balance _balance, Credentials creds)
        {
            string subdomain = "/account/balance";
            var response = ApiRequest.DoGetRequest(subdomain, _balance, creds);
            return response.HttpResponse;
        }
    }
}
