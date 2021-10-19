using RestAspNet5DockerAzure.Data.Converter.Implementations;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Hypermedia.Utils;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository;
using RestAspNet5DockerAzure.Repository.Generic;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            this._repository = repository;
            _converter = new PersonConverter();
        }


        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(Dictionary<string,string> filters, List<string> sortfields, string sortDirection, int pageSize, int page)
        {
            
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var persons = _repository.FindWithPagedSearch(filters, sortfields, sort, size, offset);
            int totalResult = _repository.GetCount(filters);

            return new PagedSearchVO<PersonVO> {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResult,
                Filters = filters,
                SortFields = sortfields

            };
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

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName,lastName));
        }

        
    }
}
