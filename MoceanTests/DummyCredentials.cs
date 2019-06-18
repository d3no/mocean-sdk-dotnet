using Mocean.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
