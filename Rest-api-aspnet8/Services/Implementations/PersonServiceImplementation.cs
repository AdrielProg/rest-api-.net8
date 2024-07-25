using Microsoft.EntityFrameworkCore;
using Rest_api_aspnet8.Model;
using Rest_api_aspnet8.Model.Context;
using Rest_api_aspnet8.Model.Exceptions.Person;
using Rest_api_aspnet8.Model.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Rest_api_aspnet8.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly MySQLContext _context;
        public PersonServiceImplementation(MySQLContext mySQLContext)
        {

            _context = mySQLContext;
        }
        public List<Person> FindAll()
        {
            try
            {
                return _context.Persons.ToList();
            }
            catch (Exception)
            {
                throw new DataAccessException("An error occurred while retrieving all persons.");
            }
        }

        public Person GetById(long? id)
        {
            if (id == null)
            {
                throw new PersonNullException();
            }

            var person = _context.Persons.SingleOrDefault(e => e.Id == id);

            if (person == null)
            {
                throw new PersonNotFoundException($"Person with ID {id} not found.");
            }
            return person;
        }

        public Person Create(Person person)
        {
            if (person == null)
            {
                throw new PersonNullException();
            }
            var result = _context.Persons.SingleOrDefault(e => e.Id == person.Id);
            if (result != null) 
            {
                throw new PersonAlreadyExistsException();
            }
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long? id)
        {
            var result = _context.Persons.SingleOrDefault(e => e.Id == id);
            if (result == null)
            {
                throw new PersonNotFoundException($"Person with ID {id} not found.");
            }

            try
            {
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("An error occurred while deleting the person.");
            }
        }

        public Person Update(Person person)
        {
            if (person == null)
            {
                throw new PersonNullException();
            }

            var existingPerson = _context.Persons.SingleOrDefault(e => e.Id == person.Id);

            if (existingPerson == null)
            {
                throw new PersonNotFoundException($"Person with ID {person.Id} not found.");
            }

            try
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.SaveChanges();
                return existingPerson;
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("An error occurred while updating the person.");
            }
        }
    }
}
