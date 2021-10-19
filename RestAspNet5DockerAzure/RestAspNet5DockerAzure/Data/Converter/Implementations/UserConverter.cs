using RestAspNet5DockerAzure.Data.Converter.Contract;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Data.Converter.Implementations
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if (origin == null) return null;
            return new User
            {
                Id = origin.Id,
                UserName = origin.UserName,
                FullName = origin.FullName,
                Password = origin.Password,
                RefreshToken = origin.RefreshToken,
                RefreshTokenExpiryTime = origin.RefreshTokenExpiryTime,
                Enabled = origin.Enabled,
            };
        }
        public UserVO Parse(User origin)
        {
            if (origin == null) return null;
            return new UserVO
            {
                Id = origin.Id,
                UserName = origin.UserName,
                FullName = origin.FullName,
                Password = origin.Password,
                RefreshToken = origin.RefreshToken,
                RefreshTokenExpiryTime = origin.RefreshTokenExpiryTime,
                Enabled = origin.Enabled,
            };
        }

        public List<User> Parse(List<UserVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UserVO> Parse(List<User> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public ICollection<UserVO> Parse(ICollection<User> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public ICollection<User> Parse(ICollection<UserVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
