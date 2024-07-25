namespace Rest_api_aspnet8.Model.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException() { }
        public DataAccessException(Exception e) { }
        public DataAccessException(string message) : base(message) { }

        public DataAccessException(string message, Exception e) : base(message, e) { }

    }
}
