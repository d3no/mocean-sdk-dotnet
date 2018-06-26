using System.Collections.Generic;

namespace Mocean.Message
{
    public class Client
    {
        public static string Send(Message _message, Credentials creds)
        {
            string subdomain = "/sms";
            var response = ApiRequest.DoPostRequest(subdomain, _message, creds);
            return response.HttpResponse;
        }

        public static string Search(Message _message, Credentials creds)
        {
            string subdomain = "/report/message";
            var response = ApiRequest.DoGetRequest(subdomain, _message, creds);
            return response.HttpResponse;
        }

        public static string DLRStatus(string status)
        {
            IDictionary<string, string> dlr_status = new Dictionary<string, string>();
            dlr_status.Add("1", "Success");
            dlr_status.Add("2", "Two");
            dlr_status.Add("3", "Three");

            return dlr_status[status];
        }
    }
}
