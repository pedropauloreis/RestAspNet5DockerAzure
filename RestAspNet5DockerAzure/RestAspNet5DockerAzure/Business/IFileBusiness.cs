using Microsoft.AspNetCore.Http;
using RestAspNet5DockerAzure.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
    }
}
