using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Bridge : AbstractMccc
    {
        public string To { set => this.requestData["to"] = value; }

        public Bridge() : this(new Dictionary<string, object>())
        {
        }

        public Bridge(Dictionary<string, object> parameter) : base(parameter)
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
