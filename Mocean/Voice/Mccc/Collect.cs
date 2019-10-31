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
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "event-url", "min", "max", "timeout"
            };
        }

        protected override string Action()
        {
            return "collect";
        }
    }
}
