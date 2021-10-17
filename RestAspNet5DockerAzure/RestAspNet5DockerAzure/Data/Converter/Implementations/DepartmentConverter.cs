using RestAspNet5DockerAzure.Data.Converter.Contract;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Data.Converter.Implementations
{
    public class DepartmentConverter : IParser<DepartmentVO, Department>, IParser<Department, DepartmentVO>
    {
        

        public Department Parse(DepartmentVO origin)
        {
            if (origin == null) return null;
            return new Department
            {
                Id = origin.Id,
                Name = origin.Name,
                
            };
        }
        public DepartmentVO Parse(Department origin)
        {
            if (origin == null) return null;
            return new DepartmentVO
            {
                Id = origin.Id,
                Name = origin.Name,
                
            };
        }

        public List<Department> Parse(List<DepartmentVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DepartmentVO> Parse(List<Department> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public ICollection<DepartmentVO> Parse(ICollection<Department> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public ICollection<Department> Parse(ICollection<DepartmentVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
