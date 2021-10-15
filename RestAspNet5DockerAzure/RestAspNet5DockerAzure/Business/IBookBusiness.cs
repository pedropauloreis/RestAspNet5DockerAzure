using RestAspNet5DockerAzure.Data.VO;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Business
{
    public interface IBookBusiness
    {
        List<BookVO> FindAll();
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
