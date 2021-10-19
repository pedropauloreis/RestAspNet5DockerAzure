using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Repository.Generic;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User ValidateCredentials(User user);

        User ValidateCredentials(string userName);

        bool RevokeToken(string userName);

        User RefreshUserInfo(User user);

        List<Role> GetUserRoles(User user);

        User Enable (long id);

        User Disable(long id);
    }
}
