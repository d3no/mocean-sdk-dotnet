using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command.McObj

{
    public class TgRequestContact : AbstractMc
    {

        public TgRequestContact() : this(new Dictionary<string, object>())
        {
            this.content("");
            this.buttonText("Share contact");
        }

        public TgRequestContact(Dictionary<string, object> parameter) : base(parameter)
        {
        }


        public TgRequestContact from(string id, string type = "bot_username")
        {
            this.requestData["from"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }


        public TgRequestContact to(string id, string type = "chat_id")
        {
            this.requestData["to"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }

        public TgRequestContact content(string text = "")
        {
            this.requestData["content"] = new Dictionary<string, string>
            {
                {"type", "text"},
                {"text", text},
            };
            return this;
        }

        public TgRequestContact buttonText(string buttonText) 
        {
            this.requestData["tg_keyboard"] = new Dictionary<string, string>
            {
                {"button_text", buttonText},
                {"button_request", "contact"},
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
