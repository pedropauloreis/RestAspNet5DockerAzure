using RestAspNet5DockerAzure.Data.Converter.Contract;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestAspNet5DockerAzure.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person,PersonVO>
    {
        private DepartmentConverter departmentConverter;

        public PersonConverter()
        {
            departmentConverter = new DepartmentConverter();
        }

        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                DepartmentId = origin.DepartmentId,
                Department = departmentConverter.Parse(origin.Department),
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                DepartmentId = origin.DepartmentId,
                Department = departmentConverter.Parse(origin.Department),
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public ICollection<PersonVO> Parse(ICollection<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public ICollection<Person> Parse(ICollection<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

    }
}
