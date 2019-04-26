using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Auth
{
    public interface IAuth
    {
        string GetAuthMethod();

        IDictionary<string, string> GetParams();
    }
}
