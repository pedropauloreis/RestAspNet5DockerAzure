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
        List<Person> FindByName(string name);
    }
}
