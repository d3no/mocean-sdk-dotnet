namespace Mocean.Verify
{
    public class Client
    {
        public static string Start(Verify _verification, Credentials creds)
        {
            string subdomain = "/verify/req";
            var response = ApiRequest.DoPostRequest(subdomain, _verification, creds);
            return response.HttpResponse;
        }

        public static string Check(Verify _verification, Credentials creds)
        {
            string subdomain = "/verify/check";
            var response = ApiRequest.DoPostRequest(subdomain, _verification, creds);
            return response.HttpResponse;
        }
    }
}
