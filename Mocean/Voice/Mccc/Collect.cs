using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Collect : AbstractMccc
    {
        public string EventUrl { set => this.requestData["event-url"] = value; }
        public int Min { set => this.requestData["min"] = value; }
        public int Max { set => this.requestData["max"] = value; }
        public string Terminators { set => this.requestData["terminators"] = value; }
        public int Timeout { set => this.requestData["timeout"] = value; }

        public Collect() : this(new Dictionary<string, object>())
        {
        }

        public Collect(Dictionary<string, object> parameter) : base(parameter)
        {
            // default value
            this.requestData["min"] = this.requestData.ContainsKey("min") ? this.requestData["min"] : 1;
            this.requestData["max"] = this.requestData.ContainsKey("max") ? this.requestData["max"] : 1;
            this.requestData["terminators"] = this.requestData.ContainsKey("terminators") ? this.requestData["terminators"] : "#";
            this.requestData["timeout"] = this.requestData.ContainsKey("timeout") ? this.requestData["timeout"] : 5000;
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "event-url", "min", "max", "terminators", "timeout"
            };
        }

        protected override string Action()
        {
            return "collect";
        }
    }
}
