using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Play : AbstractMccc
    {
        public string File { set => this.requestData["file"] = value; }

        public bool BargeIn { set => this.requestData["barge-in"] = value; }

        public bool ClearDigitCache { set => this.requestData["clear-digit-cache"] = value; }

        public Play() : this(new Dictionary<string, object>())
        {
        }

        public Play(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "file"
            };
        }

        protected override string Action()
        {
            return "play";
        }
    }
}
