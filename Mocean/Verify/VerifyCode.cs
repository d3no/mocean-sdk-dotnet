using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Verify
{
    public class VerifyCode : MoceanFactory
    {
        public VerifyCode(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-reqid", "mocean-code" };
        }

        public VerifyCodeResponse Send(VerifyCodeRequest verifyCode)
        {
            this.ValidatedAndParseFields(verifyCode);
            var apiRequest = new ApiRequest("/verify/check", "post", this.parameters);
            return (VerifyCodeResponse)ResponseFactory.CreateObjectfromRawResponse<VerifyCodeResponse>(apiRequest.Response
                    .Replace("<verify_check>", "")
                    .Replace("</verify_check>", "")
                ).SetRawResponse(apiRequest.Response);
        }
    }
}
