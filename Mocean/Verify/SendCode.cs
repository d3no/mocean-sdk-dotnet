using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Verify
{
    public class SendCode : AbstractClient
    {
        public Channel Channel { get; set; } = Channel.Auto;
        public bool IsResend { get; set; } = false;

        public SendCode(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-to", "mocean-brand" };
        }

        public SendCode SendAs(Channel channel)
        {
            this.Channel = channel;
            return this;
        }

        public SendCodeResponse Send(SendCodeRequest sendCode)
        {
            this.ValidatedAndParseFields(sendCode);

            StringBuilder verifyRequestUrl = new StringBuilder("/verify");
            if (this.IsResend)
            {
                verifyRequestUrl.Append("/resend");
            }
            else
            {
                verifyRequestUrl.Append("/req");
            }

            if (this.Channel == Channel.Sms)
            {
                verifyRequestUrl.Append("/sms");
            }

            string responseStr = this.ApiRequest.Post(verifyRequestUrl.ToString(), this.parameters);
            var sendCodeResponse = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(responseStr)
                .SetRawResponse(responseStr);
            sendCodeResponse.Client = this;

            return sendCodeResponse;
        }

        public SendCodeResponse Resend(SendCodeRequest sendCode)
        {
            this.SendAs(Channel.Sms);
            this.IsResend = true;
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-reqid" };

            return this.Send(sendCode);
        }
    }
}
