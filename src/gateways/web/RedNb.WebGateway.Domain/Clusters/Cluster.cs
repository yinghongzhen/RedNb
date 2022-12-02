using RedNb.Core.Domain;
using Volo.Abp;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("Cluster")]
public class Cluster : AggregateRootBase
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    //private Cluster()
    //{
        
    //}

    internal Cluster(
        long id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}