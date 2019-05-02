using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Verify
{
    public class SendCode : MoceanFactory
    {
        public ChargeType VerifyChargeType { get; set; } = ChargeType.ChargePerConversion;

        public SendCode(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-to", "mocean-brand" };
        }

        public SendCode SendAs(ChargeType chargeType)
        {
            this.VerifyChargeType = chargeType;
            return this;
        }

        public SendCodeResponse Send(SendCodeRequest sendCode)
        {
            this.ValidatedAndParseFields(sendCode);

            string verifyRequestUrl = "/verify/req";
            if(this.VerifyChargeType == ChargeType.ChargePerAttempt)
            {
                verifyRequestUrl += "/sms";
            }

            var apiRequest = new ApiRequest(verifyRequestUrl, "post", this.parameters);
            return (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(apiRequest.Response
                    .Replace("<verify_request>", "")
                    .Replace("</verify_request>", "")
                ).SetRawResponse(apiRequest.Response);
        }
    }
}
