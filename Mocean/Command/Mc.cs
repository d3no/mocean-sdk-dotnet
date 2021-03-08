using Mocean.Command.McObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Command
{
    public static class Mc
    {
        public static TgSendAudio tgSendAudio() { return new TgSendAudio();  }
        public static TgSendAnimation tgSendAnimation() { return new TgSendAnimation();  }
        public static TgSendDocument tgSendDocument() { return new TgSendDocument();  }
        public static TgSendPhoto tgSendPhoto() { return new TgSendPhoto();  }
        public static TgSendVideo tgSendVideo() { return new TgSendVideo();  }
        public static TgSendText tgSendText() { return new TgSendText(); }
        public static TgRequestContact tgRequestContact() { return new TgRequestContact();  }
        public static SendSMS sendSMS() { return new SendSMS(); }

    }
}
