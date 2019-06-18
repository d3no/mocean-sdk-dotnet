using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Exceptions
{
    public class MoceanErrorException : Exception
    {
        public ErrorResponse ErrorResponse { get; private set; }

        public MoceanErrorException(string errMsg) : base(errMsg)
        {
        }

        public MoceanErrorException(ErrorResponse errorResponse) : base(errorResponse.RawResponse)
        {
            this.ErrorResponse = errorResponse;
        }
    }
}
