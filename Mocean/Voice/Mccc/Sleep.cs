using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Sleep : AbstractMccc
    {
        public int Duration { set => this.requestData["duration"] = value; }
        public bool BargeIn { set => this.requestData["barge-in"] = value; }

        public Sleep() : this(new Dictionary<string, object>())
        {
        }

        public Sleep(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "duration"
            };
        }

        protected override string Action()
        {
            return "sleep";
        }
    }
}
