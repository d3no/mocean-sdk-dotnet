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
        public static string SDK_VERSION = "2.0.0";
        public IAuth Credentials { get; }
        public ApiRequest ApiRequest { get; set; }

        public Client(IAuth credentials) : this(credentials, new ApiRequest(ApiRequestConfig.make())) { }

        public Client(IAuth credentials, ApiRequest apiRequest)
        {
            this.Credentials = credentials;
            this.ApiRequest = apiRequest;

            if (credentials.GetAuthMethod().Equals("basic", StringComparison.CurrentCultureIgnoreCase))
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

        public Balance Balance { get => new Balance(this, this.ApiRequest); }
        public Pricing Pricing { get => new Pricing(this, this.ApiRequest); }
        public MessageStatus MessageStatus { get => new MessageStatus(this, this.ApiRequest); }
        public Sms Sms { get => new Sms(this, this.ApiRequest); }
        public SendCode SendCode { get => new SendCode(this, this.ApiRequest); }
        public VerifyCode VerifyCode { get => new VerifyCode(this, this.ApiRequest); }
        public NumberLookup.NumberLookup NumberLookup { get => new NumberLookup.NumberLookup(this, this.ApiRequest); }
        public Voice.Voice Voice { get => new Voice.Voice(this, this.ApiRequest); }
    }
}
