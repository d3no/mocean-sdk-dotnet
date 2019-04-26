using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Message
{
    public class MessageStatus : MoceanFactory
    {
        public MessageStatus(Client client) : base(client.Credentials)
        {
            this.requiredFields = new List<string>() { "mocean-api-key", "mocean-api-secret", "mocean-msgid" };
        }

        public MessageStatusResponse Inquiry(MessageStatusRequest messageStatusRequest)
        {
            this.ValidatedAndParseFields(messageStatusRequest);
            var apiRequest = new ApiRequest("/report/message", "get", this.parameters);
            return (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(apiRequest.Response)
                .SetRawResponse(apiRequest.Response);
        }
    }
}
