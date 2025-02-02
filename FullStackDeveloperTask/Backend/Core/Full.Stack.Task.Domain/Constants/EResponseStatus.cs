using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Domain.Constants
{
    public enum EResponseStatus
    {
        Success = 1,
        Error = -1,
        Exception = -2,
        Security = -3,
        ConcurrentSession = -4,
        Forbidden = -403,
        BadGateway = -502,
        Unauthorized = -401,
    }
}
