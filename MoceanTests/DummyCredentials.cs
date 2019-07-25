using Mocean.Auth;
using System.Collections.Generic;

namespace MoceanTests
{
    public class DummyCredentials : IAuth
    {
        public string GetAuthMethod()
        {
            return "";
        }

        public IDictionary<string, string> GetParams()
        {
            return new Dictionary<string, string>();
        }
    }
}
