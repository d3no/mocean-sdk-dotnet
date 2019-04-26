using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Message
{
    public class Sms : MoceanFactory
    {
        public Sms(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-from", "mocean-to", "mocean-text" };
        }

        public SmsResponse Send(SmsRequest sms)
        {
            this.ValidatedAndParseFields(sms);
            var apiRequest = new ApiRequest("/sms", "post", this.parameters);
            return (SmsResponse)ResponseFactory.CreateObjectfromRawResponse<SmsResponse>(apiRequest.Response)
                .SetRawResponse(apiRequest.Response);
        }
    }
}
