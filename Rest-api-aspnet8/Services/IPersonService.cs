using Rest_api_aspnet8.Model;

namespace Rest_api_aspnet8.Services
{
    public interface IPersonService
    {
        public Person Create(Person person);
        public Person GetById(long? id);
        public Person Update(Person person);
        public void Delete(long? id);
        public List<Person> FindAll();
    }
}
