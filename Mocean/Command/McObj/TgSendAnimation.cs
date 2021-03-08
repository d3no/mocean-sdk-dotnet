using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command.McObj

{
    public class TgSendAnimation : AbstractMc
    {

        public TgSendAnimation() : this(new Dictionary<string, object>())
        {
        }

        public TgSendAnimation(Dictionary<string, object> parameter) : base(parameter)
        {
        }


        public TgSendAnimation from(string id, string type = "bot_username")
        {
            this.requestData["from"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }


        public TgSendAnimation to(string id, string type = "chat_id")
        {
            this.requestData["to"] = new Dictionary<string, string>
            {
                {"type", type},
                {"id", id}
            };
            return this;
        }

        public TgSendAnimation content(string url, string text = "")
        {
            this.requestData["content"] = new Dictionary<string, string>
            {
                {"type", "animation"},
                {"rich_media_url", url},
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
