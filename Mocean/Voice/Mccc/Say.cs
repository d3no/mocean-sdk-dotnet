using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Say : AbstractMccc
    {
        public string Language { set => this.requestData["language"] = value; }
        public string Text { set => this.requestData["text"] = value; }
        public bool BargeIn { set => this.requestData["barge-in"] = value; }

        public bool ClearDigitCache { set => this.requestData["clear-digit-cache"] = value; }

        public Say() : this(new Dictionary<string, object>())
        {
        }

        public Say(Dictionary<string, object> parameter) : base(parameter)
        {
            // default value
            this.requestData["language"] = this.requestData.ContainsKey("language") ? this.requestData["language"] : "en-US";
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "text", "language"
            };
        }

        protected override string Action()
        {
            return "say";
        }
    }
}
