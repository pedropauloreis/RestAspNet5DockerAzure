using RestAspNet5DockerAzure.Model;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Services
{
    public interface IPersonService
    {
        List<Person> FindAll();
        Person Create(Person person);
        Person FindByID(long id);
        Person Update(Person person);
        void Delete(long id);
    }
}
