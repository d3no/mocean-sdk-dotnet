using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command.McObj

{
    public class SendSMS : AbstractMc
    {

        public SendSMS() : this(new Dictionary<string, object>())
        {
        }

        public SendSMS(Dictionary<string, object> parameter) : base(parameter)
        {
        }


        public SendSMS from(string id, string type = "phone_num")
        {
            this.requestData["from"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }


        public SendSMS to(string id, string type = "phone_num")
        {
            this.requestData["to"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }

        public SendSMS content(string text)
        {
            this.requestData["content"] = new Dictionary<string, string>
            {
                {"type", "text"},
                {"text", text}
            };
            return this;
        }


        protected override string Action()
        {
            return "send-sms";
        }

        protected override List<string> RequiredKey()
        {

            return new List<string> {

                "from",
                "to",
                "content",
            };
        }
    }
}
