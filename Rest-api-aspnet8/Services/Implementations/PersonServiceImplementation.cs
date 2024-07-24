using Microsoft.EntityFrameworkCore;
using Rest_api_aspnet8.Model;
using Rest_api_aspnet8.Model.Context;
using Rest_api_aspnet8.Model.Exceptions;
using System;

namespace Rest_api_aspnet8.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly MySQLContext _context;
        public PersonServiceImplementation(MySQLContext mySQLContext) { 
            
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
                throw new DataAccessException("An error occurred while retrieving all persons.", ex);
            }
        }
        public Person GetById(long id)
        {
            try
            {
                var person = _context.Persons.SingleOrDefault(e => e.Id == id);
                if (person == null)
                {
                    throw new BusinessException($"Person with id {id} not found.");
                }
                return person;
            }
            catch (Exception e)
            {
                throw new DataAccessException("An error occurred while retrieving the person by id.", ex);
            }
        }
        public Person Create(Person person)
        {
            if (person == null)
            {
                throw new BusinessException("Person object cannot be null.");
            }
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                return person;
            }
            catch (DbUpdateException e)
            {
                throw new DataAccessException("An error occurred while saving the person to the database.", e);
            }
            catch (Exception e)
            {
                throw new DataAccessException("An unexpected error occurred while creating the person.", e);
            }
        }
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(e => e.Id.Equals(id));
            if (result != null)
            {

                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new BusinessException("An error occurs when deleting data", e);
                }
                catch (Exception e)
                {
                    throw new DataAccessException("An unexpected error occurred while deleting the person.", e);
                }
            }
        }
        public Person Update(Person person)
        {
            if (!Exists(person.Id))
            {
                throw new BusinessException("Person object cannot be null.");
            }
            var result = _context.Persons.SingleOrDefault(e => e.Id.Equals(person.Id));
            if (result != null) {

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DataAccessException("An error occurred while updating a record on the database", e);
                }
                catch (Exception e)
                {
                    throw new DataAccessException("An unexpected error occurred while creating the person.", e);
                }
            }
                return person;
        }
        private bool Exists(long id)
        {
            return _context.Persons.Any(e => e.Id.Equals(id));
        }
    }
}
