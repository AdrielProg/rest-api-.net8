using Microsoft.EntityFrameworkCore;
using Rest_api_aspnet8.Model;
using Rest_api_aspnet8.Model.Context;
using Rest_api_aspnet8.Model.Exceptions;
using System;
using System.Data;

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
            catch (Exception e)
            {
                throw new DataAccessException(e);
            }
        }

        public Person GetById(long? id)
        {
            if (id == null)
            {
                throw new BusinessException("ID cannot be null.");
            }

            try
            {
                var person = _context.Persons.SingleOrDefault(e => e.Id == id);
                if (person == null)
                {
                    throw new BusinessException("Person not found.");
                }
                return person;
            }
            catch (Exception e)
            {
                throw new BusinessException("An error occurred while retrieving the person.", e);
            }
        }


        public Person Create(Person person)
        {
            if (person == null)
            {
                throw new BusinessException();
            }
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                return person;
            }
            catch (Exception e)
            {
                throw new DataAccessException(e);
            }
        }

        public void Delete(long? id)
        {
            var result = _context.Persons.SingleOrDefault(e => e.Id == id);
            if (result == null)
            {
                throw new BusinessException();
            }

            try
            {
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DataAccessException(e);
            }
        }

        public Person Update(Person person)
        {
            if (person == null)
            {
                throw new BusinessException("Person cannot be null.");
            }

            var existingPerson = _context.Persons.SingleOrDefault(e => e.Id == person.Id);

            if (existingPerson == null)
            {
                throw new BusinessException($"Person with ID {person.Id} not found.");
            }

            try
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.SaveChanges();
                return existingPerson;
            }
            catch (DbUpdateException e)
            {
                throw new DataAccessException("An error occurred while updating the person.", e);
            }
        }
    }
}
