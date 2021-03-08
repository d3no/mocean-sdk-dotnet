using Mocean.Command.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command
{
    public class Command : AbstractClient
    {
        public Command(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-command"};
        }

        public CommandResponse Execute(CommandRequest command)
        {          
            this.ValidatedAndParseFields(command);
            string responseStr = this.ApiRequest.Post("/send-message", this.parameters);
            return (CommandResponse)ResponseFactory.CreateObjectfromRawResponse<CommandResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
