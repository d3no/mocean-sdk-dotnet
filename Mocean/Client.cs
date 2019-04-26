using Mocean.Account;
using Mocean.Auth;
using Mocean.Exceptions;
using Mocean.Message;
using Mocean.Verify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean
{
    public class Client
    {
        public IAuth Credentials { get; }

        public Client(IAuth credentials)
        {
            this.Credentials = credentials;

            if (credentials.GetAuthMethod().Equals("basic", StringComparison.InvariantCultureIgnoreCase))
            {
                if (String.IsNullOrEmpty(credentials.GetParams()["mocean-api-key"]) || String.IsNullOrEmpty(credentials.GetParams()["mocean-api-secret"]))
                {
                    throw new RequiredFieldException("Api key and api secret for client object can't be empty.");
                }
            }
            else
            {
                throw new MoceanErrorException("Unsupported Auth Method");
            }
        }

        public Balance Balance { get => new Balance(this); }
        public Pricing Pricing { get => new Pricing(this); }
        public MessageStatus MessageStatus { get => new MessageStatus(this); }
        public Sms Sms { get => new Sms(this); }
        public SendCode SendCode { get => new SendCode(this); }
        public VerifyCode VerifyCode { get => new VerifyCode(this); }
        public NumberLookup.NumberLookup NumberLookup { get => new NumberLookup.NumberLookup(this); }
    }
}
