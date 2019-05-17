using System;

namespace ee.Framework.Schema
{
    public class EeException : Exception
    {
        public int ErrorCode { get; protected set; }

        public EeException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
