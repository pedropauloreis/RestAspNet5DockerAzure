using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
