using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Account
{
    public class Pricing : MoceanFactory
    {
        public Pricing(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret" };
        }

        public PricingResponse Inquiry(PricingRequest pricing = default(PricingRequest))
        {
            this.ValidatedAndParseFields(pricing);
            var apiRequest = new ApiRequest("/account/pricing", "get", this.parameters);
            return (PricingResponse)ResponseFactory.CreateObjectfromRawResponse<PricingResponse>(apiRequest.Response)
                .SetRawResponse(apiRequest.Response);
        }
    }
}
