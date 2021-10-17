using RestAspNet5DockerAzure.Data.VO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Business
{
    public interface IPersonBusiness
    {
        List<PersonVO> FindAll();
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
