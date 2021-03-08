using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command.McObj

{
    public class TgSendText : AbstractMc
    {

        public TgSendText() : this(new Dictionary<string, object>())
        { 
        }

        public TgSendText(Dictionary<string,object> parameter) : base(parameter)
        {
        }


        public TgSendText from(string id, string type="bot_username") 
        {
            this.requestData["from"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }


        public TgSendText to(string id, string type = "chat_id")
        {
            this.requestData["to"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }

        public TgSendText content(string text)
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
            return "send-telegram";
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
