using Microsoft.AspNetCore.Http;
using RestAspNet5DockerAzure.Configurations;
using RestAspNet5DockerAzure.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly UploadConfiguration _uploadConfiguration;
        private readonly IHttpContextAccessor _context;
        
        public FileBusinessImplementation(UploadConfiguration uploadConfiguration, IHttpContextAccessor context)
        {
            _context = context;
            _uploadConfiguration = uploadConfiguration;
            _basePath = Directory.GetCurrentDirectory() + $"\\{_uploadConfiguration.directory}\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            

            if (file == null || file.Length == 0)
                throw new Exception($"No or Null file provided.");

            var fileType = Path.GetExtension(file.FileName).ToLower();
            var baseUrl = _context.HttpContext.Request.Host;

            
            if (file.Length > _uploadConfiguration.maxSizeBytes)
                throw new Exception($"Max filesize exceeded, configured maxSizeBytes: {_uploadConfiguration.maxSizeBytes.ToString()}");

            if (!_uploadConfiguration.acceptedFileExtensions.Any(s => s.IndexOf(fileType.Replace(".",""), StringComparison.CurrentCultureIgnoreCase)>-1))
                throw new Exception($"File extension not accepted.");

            var docName = _uploadConfiguration.filePrefix + Guid.NewGuid() + _uploadConfiguration.fileSufix + fileType;

            FileDetailVO fileDetail = new FileDetailVO();
            try
            {
                var destination = Path.Combine(_basePath, "", docName);
                fileDetail.DocumentName = docName;
                fileDetail.DocType = fileType;
                fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/uploadedfiles/" + fileDetail.DocumentName);

                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception)
            {

                throw new Exception("Error saving file.");
            }
            

            
            return fileDetail;
        }


    }
}
