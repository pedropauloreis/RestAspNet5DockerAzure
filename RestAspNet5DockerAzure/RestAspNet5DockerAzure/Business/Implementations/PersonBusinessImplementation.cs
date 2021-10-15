using RestAspNet5DockerAzure.Data.Converter.Implementations;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository.Generic;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            this._repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }


        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var PersonEntity = _converter.Parse(person);
            PersonEntity = _repository.Create(PersonEntity);
            return _converter.Parse(PersonEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var PersonEntity = _converter.Parse(person);
            PersonEntity = _repository.Update(PersonEntity);
            return _converter.Parse(PersonEntity);
        }

        public void Delete(long id)
        {

            _repository.Delete(id);

        }

    }
}
