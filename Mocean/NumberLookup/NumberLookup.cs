using Mocean.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.NumberLookup
{
    public class NumberLookup : MoceanFactory
    {
        public NumberLookup(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-to" };
        }

        public NumberLookupResponse Inquiry(NumberLookupRequest numberLookup)
        {
            this.ValidatedAndParseFields(numberLookup);
            var apiRequest = new ApiRequest("/nl", "get", this.parameters);
            return (NumberLookupResponse)ResponseFactory.CreateObjectfromRawResponse<NumberLookupResponse>(apiRequest.Response)
                .SetRawResponse(apiRequest.Response);
        }
    }
}
