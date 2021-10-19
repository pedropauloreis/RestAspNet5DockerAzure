using RestAspNet5DockerAzure.Data.Converter.Implementations;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business.Implementations
{
    public class UserBusinessImplementation : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly UserConverter _converter;
        private readonly RoleConverter _converterRole;

        public UserBusinessImplementation(IUserRepository repository)
        {
            _repository = repository;
            _converter = new UserConverter();
            _converterRole = new RoleConverter();
        }

        public List<UserVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public UserVO FindByID(long id)
        {
            UserVO user = _converter.Parse(_repository.FindByID(id));
            user.Roles = _converterRole.Parse(_repository.GetUserRoles(_converter.Parse(user)));
            return user;
        }

        public UserVO Create(UserVO user)
        {
            var UserEntity = _converter.Parse(user);
            UserEntity = _repository.Create(UserEntity);
            return _converter.Parse(UserEntity);
        }

        public UserVO Update(UserVO user)
        {
            var UserEntity = _converter.Parse(user);
            UserEntity = _repository.Update(UserEntity);
            return _converter.Parse(UserEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

        public UserVO ValidateCredentials(UserVO user)
        {
            var UserEntity = _converter.Parse(user);
            UserEntity = _repository.ValidateCredentials(UserEntity);
            return _converter.Parse(UserEntity);
        }

        public UserVO Enable(long id)
        {
            return _converter.Parse(_repository.Enable(id));
        }

        public UserVO Disable(long id)
        {
            return _converter.Parse(_repository.Disable(id));
        }
    }
}
