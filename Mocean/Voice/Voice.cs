using Mocean.Voice.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Voice : AbstractClient
    {
        public Voice(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-to" };
        }

        public VoiceResponse Call(VoiceRequest voice)
        {
            this.ValidatedAndParseFields(voice);

            string responseStr = this.ApiRequest.Get("/voice/dial", this.parameters);
            return (VoiceResponse)ResponseFactory.CreateObjectfromRawResponse<VoiceResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
