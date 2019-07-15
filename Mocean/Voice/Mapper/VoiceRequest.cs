using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.Mapper
{
    public class VoiceRequest
    {
        [JsonProperty("mocean-to")]
        public string mocean_to { get; set; }

        [JsonProperty("mocean-call-event-url")]
        public string mocean_call_event_url { get; set; }

        [JsonProperty("mocean-call-control-commands")]
        public object mocean_call_control_commands
        {
            get
            {
                var builderCallControlCommands = _callControlCommands as McccBuilder;
                if(builderCallControlCommands != null)
                {
                    return JsonConvert.SerializeObject(((McccBuilder)_callControlCommands).build());
                }

                var objCallControlCommands = _callControlCommands as AbstractMccc;
                if(objCallControlCommands != null)
                {
                    return JsonConvert.SerializeObject((new McccBuilder()).add((AbstractMccc)_callControlCommands).build());
                }

                var dictCallControlCommands = _callControlCommands as Dictionary<string, object>;
                if(dictCallControlCommands != null)
                {
                    return JsonConvert.SerializeObject(new List<Dictionary<string, object>>
                    {
                        (Dictionary<string, object>)_callControlCommands
                    });
                }

                return _callControlCommands;
            }
            set
            {
                _callControlCommands = value;
            }
        }
        private object _callControlCommands;

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
