using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Dial : AbstractMccc
    {
        public string To { set => this.requestData["to"] = value; }

        public string From { set => this.requestData["from"] = value; }

        public bool DialSequentially { set => this.requestData["dial-sequentially"] = value; }

        public Dial() : this(new Dictionary<string, object>())
        {
        }

        public Dial(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "to"
            };
        }

        protected override string Action()
        {
            return "dial";
        }
    }
}
