using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Account
{
    public class Balance : MoceanFactory
    {
        public Balance(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret" };
        }

        public BalanceResponse Inquiry(BalanceRequest balance = default(BalanceRequest))
        {
            this.ValidatedAndParseFields(balance);
            var apiRequest = new ApiRequest("/account/balance", "get", this.parameters);
            return (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(apiRequest.Response)
                .SetRawResponse(apiRequest.Response);
        }
    }
}
