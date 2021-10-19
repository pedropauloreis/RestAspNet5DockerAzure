using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Hypermedia.Utils;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Business
{
    public interface IPersonBusiness
    {
        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(Dictionary<string, string> filters, List<string> sortfields, string sortDirection, int pageSize, int page);

        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        PersonVO Update(PersonVO person);
        void Delete(long id);

        bool Exists(long id);

        List<PersonVO> FindByName(string firstName, string lastName);
    }
}


