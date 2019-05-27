using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Message
{
    public class Sms : AbstractClient
    {
        public Sms(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-from", "mocean-to", "mocean-text" };
        }

        public SmsResponse Send(SmsRequest sms)
        {
            this.ValidatedAndParseFields(sms);

            string responseStr = this.ApiRequest.Post("/sms", this.parameters);
            return (SmsResponse)ResponseFactory.CreateObjectfromRawResponse<SmsResponse>(responseStr)
                .SetRawResponse(responseStr);
        }
    }
}
