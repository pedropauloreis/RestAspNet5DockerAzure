namespace RestAspNet5DockerAzure.Configurations
{
    public class UploadConfiguration
    {
        
        public long maxSizeBytes { get; set; }
        public string directory { get; set; }
        public string[] acceptedFileExtensions { get; set; }

        public string filePrefix { get; set; }
        public string fileSufix{ get; set; }

    }
}
