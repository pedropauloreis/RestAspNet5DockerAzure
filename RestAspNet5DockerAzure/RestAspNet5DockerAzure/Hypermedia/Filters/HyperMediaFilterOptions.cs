using RestAspNet5DockerAzure.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
