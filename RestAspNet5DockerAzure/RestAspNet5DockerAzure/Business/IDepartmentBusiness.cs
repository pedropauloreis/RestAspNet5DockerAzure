using RestAspNet5DockerAzure.Data.VO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Business
{
    public interface IDepartmentBusiness
    {
        List<DepartmentVO> FindAll();
        DepartmentVO Create(DepartmentVO person);
        DepartmentVO FindByID(long id);
        DepartmentVO Update(DepartmentVO person);
        void Delete(long id);

        bool Exists(long id);

    }
}
