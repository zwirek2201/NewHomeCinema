using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema
{
    public class NotFoundException : Exception { }

    public class CouldNotPerformOperationException : Exception
    {
        public Exception InnerException { get; set; }
    }

    public class UserAlreadyRegisteredException : Exception { }

    public class BadLoginDataException : Exception { }

    public class NotAuthenticatedException : Exception { }

    public class TokenExpiredException : Exception { }

}
