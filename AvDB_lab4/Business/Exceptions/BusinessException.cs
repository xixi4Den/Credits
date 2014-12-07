using System;
using System.Runtime.Serialization;

namespace AvDB_lab4.Business.Exceptions
{
    public class BusinessException : Exception
        {
            public BusinessException()
                : base() { }

            public BusinessException(string message)
                : base(message) { }

            public BusinessException(string format, params object[] args)
                : base(string.Format(format, args)) { }

            public BusinessException(string message, Exception innerException)
                : base(message, innerException) { }

            public BusinessException(string format, Exception innerException, params object[] args)
                : base(string.Format(format, args), innerException) { }

            protected BusinessException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }
        }
}