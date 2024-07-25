using Rest_api_aspnet8.Model.Exceptions.Interfaces;

namespace Rest_api_aspnet8.Model.Exceptions.Person
{
    public class PersonNotFoundException : BusinessException, INotFoundException
    {
        public PersonNotFoundException() : base("Person not found.") { }

        public PersonNotFoundException(string message) : base(message) { }
    }

    public class PersonAlreadyExistsException : BusinessException, IConflictException
    {
        public PersonAlreadyExistsException() : base("Person already exists.") { }

        public PersonAlreadyExistsException(string message) : base(message) { }
    }

    public class PersonNullException : BusinessException, IValidationException
    {
        public PersonNullException() : base("Person cannot be null.") { }
    }
}

