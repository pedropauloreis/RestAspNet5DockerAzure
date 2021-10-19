using RestAspNet5DockerAzure.Data.Converter.Contract;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Data.Converter.Implementations
{
    public class RoleConverter : IParser<RoleVO, Role>, IParser<Role, RoleVO>
    {
        public Role Parse(RoleVO origin)
        {
            if (origin == null) return null;
            return new Role
            {
                Id = origin.Id,
                Name = origin.Name,
            };
        }
        public RoleVO Parse(Role origin)
        {
            if (origin == null) return null;
            return new RoleVO
            {
                Id = origin.Id,
                Name = origin.Name
            };
        }

        public List<Role> Parse(List<RoleVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<RoleVO> Parse(List<Role> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


    }
}
