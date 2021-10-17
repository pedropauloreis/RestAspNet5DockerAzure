using RestAspNet5DockerAzure.Data.Converter.Implementations;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository.Generic;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business.Implementations
{
    public class DepartmentBusinessImplementation : IDepartmentBusiness
    {
        private readonly IRepository<Department> _repository;
        private readonly DepartmentConverter _converter;

        public DepartmentBusinessImplementation(IRepository<Department> repository)
        {
            this._repository = repository;
            _converter = new DepartmentConverter();
        }

        public List<DepartmentVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }


        public DepartmentVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public DepartmentVO Create(DepartmentVO department)
        {
            var DepartmentEntity = _converter.Parse(department);
            DepartmentEntity = _repository.Create(DepartmentEntity);
            return _converter.Parse(DepartmentEntity);
        }

        public DepartmentVO Update(DepartmentVO department)
        {
            var DepartmentEntity = _converter.Parse(department);
            DepartmentEntity = _repository.Update(DepartmentEntity);
            return _converter.Parse(DepartmentEntity);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);

        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

      
    }
}
