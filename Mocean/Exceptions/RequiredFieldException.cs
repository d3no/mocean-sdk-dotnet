using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Exceptions
{
    public class RequiredFieldException : MoceanErrorException
    {
        public RequiredFieldException(string errMsg) : base(errMsg)
        {

        }
    }
}
