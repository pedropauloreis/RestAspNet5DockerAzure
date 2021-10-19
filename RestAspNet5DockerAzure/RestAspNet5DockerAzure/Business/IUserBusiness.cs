using RestAspNet5DockerAzure.Data.VO;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business
{
    public interface IUserBusiness
    {
        List<UserVO> FindAll();
        UserVO Create(UserVO user);
        UserVO FindByID(long id);
        UserVO Update(UserVO user);
        void Delete(long id);

        UserVO ValidateCredentials(UserVO user);

        bool Exists(long id);

        UserVO Enable(long id);

        UserVO Disable(long id);
    }
}
