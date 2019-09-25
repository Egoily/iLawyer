namespace ee.Framework
{
    public class ErrorCodes
    {
        private const int BaseErrorCode = 1000;
        public const int UnknownError = -1;
        public const int Ok = 0;
        public const int NullParameter = BaseErrorCode + 1;
        public const int InvalidParameter = BaseErrorCode + 2;
        public const int OperationFailed = BaseErrorCode + 3;
        public const int NotFound = BaseErrorCode + 4;
        public const int Existed = BaseErrorCode + 5;
        public const int Unauthorized = BaseErrorCode + 6;
        public const int DevelopError = BaseErrorCode + 7;
        public const int ExistedUser = BaseErrorCode + 8;
        public const int NotExistedUser = BaseErrorCode + 9;
        public const int IllegalCertificate = BaseErrorCode + 10;
        public const int IllegalStatus = BaseErrorCode + 110;
    }
}
