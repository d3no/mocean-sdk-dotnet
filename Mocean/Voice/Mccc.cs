using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public static class Mccc
    {
        public static Say say()
        {
            return new Say();
        }

        public static Say say(string text)
        {
            return new Say { Text = text };
        }

        public static Play play()
        {
            return new Play();
        }

        public static Play play(string file)
        {
            return new Play { File = file };
        }

        public static Bridge bridge()
        {
            return new Bridge();
        }

        public static Bridge bridge(string to)
        {
            return new Bridge { To = to };
        }

        public static Collect collect()
        {
            return new Collect();
        }

        public static Collect collect(string eventUrl)
        {
            return new Collect { EventUrl = eventUrl };
        }

        public static Sleep sleep()
        {
            return new Sleep();
        }

        public static Sleep sleep(int duration)
        {
            return new Sleep { Duration = duration };
        }
    }
}
