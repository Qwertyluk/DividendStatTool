using xAPI.Errors;

namespace xAPIServices.Exceptions
{
    public class XTBLoginException : Exception
    {
        public XTBLoginException() { }

        public XTBLoginException(string message) : base(message) { }

        public XTBLoginException(string message, ERR_CODE errCode) : base(message + $" Error code: {errCode.StringValue}") { }

        public XTBLoginException(string message, Exception inner) : base(message, inner) { }
    }
}
