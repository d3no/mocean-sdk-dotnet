using System;
using Mocean;
using Mocean.Auth;
using Mocean.Command;
using Mocean.Command.Mapper;

namespace MyProgram
{
    class Program
    {
        private static Client client;                                                                                                                                                                                                                                                                         

        static void Main(string[] args)
        {
            Console.WriteLine("send-message response:");
            Console.WriteLine(sendMessage());
            Console.WriteLine("");
            Console.WriteLine("send code response:");
            Console.WriteLine(sendCode());
            Console.WriteLine("");
            Console.WriteLine("voice call response:");
            Console.WriteLine(voiceCall());
            Console.WriteLine(reqCode());

            Console.ReadLine();
        }

        private static String sendSMS() 
        {
            var client = moceanClient();
            try
            {

                var res = client.Sms.Send(new Mocean.Message.SmsRequest
                {
                    mocean_to = "60165465738",
                    mocean_from = "MOCEAN",
                    mocean_text = "Hello World"
                });
                return res.ToString();
            }
            catch (NullReferenceException e) {
                return e.ToString();
            }
         
        }

        private static String reqCode() 
        {
            var client = moceanClient();
            try
            {

                var res = client.SendCode
                    .SendAs(Mocean.Verify.Channel.Telegram)
                    .Send(new Mocean.Verify.SendCodeRequest
                    {
                        mocean_from = "moceantestbot",
                        mocean_brand = "MOCEAN",
                        mocean_to = "60165465738",
                        mocean_resp_format = "json",
                    });
                return res.ToString();
            }
            catch (NullReferenceException e)
            {
                return e.ToString();
            }
        }

        private static String sendMessage() 
        {
            var client = moceanClient();
            try
            {

                var mc1 = Mc.tgSendText().from("moceantestbot").to("813260944").content("Hello world");
                var mc2 = Mc.tgSendAudio().from("moceantestbot").to("813260944").content("https://tzhongyan.com/tt.mp3", "Hello world");
                var mc3 = Mc.tgSendAnimation().from("moceantestbot").to("813260944").content("https://i.pinimg.com/originals/0d/0a/37/0d0a3751364e58a85f68b9bf36043fdf.gif", "Hello world");
                var mc4 = Mc.tgSendDocument().from("moceantestbot").to("813260944").content("https://www.segi.edu.my/images/uploads/pdf/mbbs-2020-pdf.pdf", "Hello world");
                var mc5 = Mc.tgSendPhoto().from("moceantestbot").to("813260944").content("https://www.drupal.org/files/project-images/mocean-logo_4.jpg", "Hello world");
                var mc6 = Mc.tgSendVideo().from("moceantestbot").to("813260944").content("https://tzhongyan.com/yee.mp4", "Hello world");
                var mc7 = Mc.tgRequestContact().from("moceantestbot").to("813260944").content("Share you contact number").buttonText("Share contact");
                var mc8 = Mc.sendSMS().from("63001").to("60165465738").content("Hello world");

                var builder = (new McBuilder()).add(mc1).add(mc2).add(mc3).add(mc4).add(mc5).add(mc6).add(mc7).add(mc8);


                var res = client.Command.Execute(new CommandRequest 
                { 
                    mocean_command = builder,
                    mocean_resp_format = "JSON",
                    
                    
                });
                return res.ToString();
            }
            catch (NullReferenceException e)
            {
                return e.ToString();
            }
        }

        private static String sendCode() {

            var client = moceanClient();

            try
            {
                var res = client.SendCode.SendAs(Mocean.Verify.Channel.Telegram).Send(new Mocean.Verify.SendCodeRequest
                {
                    mocean_brand = "mocean",
                    mocean_to = "813260944",
                    mocean_from = "moceantestbot",
                    mocean_resp_format = "JSON"
                });
                return res.ToString();
            }
            catch (Exception e) {
                return e.ToString();
            }
           
        }

        private static String voiceCall() {

            var client = moceanClient();

            try
            {
                var builder = new Mocean.Voice.McBuilder();
                builder.add(Mocean.Voice.Mc.say("Hello world"));

                var res = client.Voice.Call(new Mocean.Voice.Mapper.VoiceRequest
                {

                    mocean_to = "60165465738",
                    mocean_command = builder
                });

                return res.ToString();
            }
            catch (Exception e) {

                return e.ToString();
            }

            
        }

        private static Client moceanClient() 
        {

            if (client == null) {
                //var credentials = new Basic("SydD2E4X1H", "Abcd123456789");
                var credentials = new Basic("zhongyan_api", "zhongyan_api");
                client = new Client(credentials);
            }

            return client;
        }
    }
}
