using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Model.Base;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> FindAll();
        T Create(T item);
        T FindByID(long id);
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);

        List<T> ExecuteFromRaw(string query);

        int ExecuteScalar(string query);
    }
}
