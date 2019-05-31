using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Account
{
    public class Pricing : AbstractClient
    {
        public Pricing(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret" };
        }

        public PricingResponse Inquiry(PricingRequest pricing = default(PricingRequest))
        {
            this.ValidatedAndParseFields(pricing);

            string responseStr = this.ApiRequest.Get("/account/pricing", this.parameters);
            return (PricingResponse)ResponseFactory.CreateObjectfromRawResponse<PricingResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
