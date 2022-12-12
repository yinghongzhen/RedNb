using Microsoft.EntityFrameworkCore;
using RedNb.Core.Domain;

namespace RedNb.Gateway.Domain.Clusters;

[Table("Cluster")]
public class Cluster : AggregateRootBase
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Path { get; set; }

    internal Cluster(long id, string name, string path)
    {
        Id = id;
        Name = name;
        Path = path;
    }
}