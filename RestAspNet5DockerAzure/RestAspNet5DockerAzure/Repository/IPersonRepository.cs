using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        //Create Custom Methods
        List<Person> FindByName(string firstName, string lastName);

        List<Person> FindWithPagedSearch(Dictionary<string, string> filter, List<string> sortfield, string sort, int size, int offset);

        int GetCount(Dictionary<string, string> filters);
    }
}
