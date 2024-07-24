﻿namespace Rest_api_aspnet8.Model.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException() { }

        public DataAccessException(string message) : base(message) { }

        public DataAccessException(string message, Exception ex) : base(message, ex) { }

    }
}
