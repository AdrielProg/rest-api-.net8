namespace Rest_api_aspnet8.Model.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception e) : base(message, e) { }
    }

}