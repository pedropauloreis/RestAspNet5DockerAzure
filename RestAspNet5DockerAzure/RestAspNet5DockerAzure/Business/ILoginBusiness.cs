using RestAspNet5DockerAzure.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userCredentials);

        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string userName);
    }
}
