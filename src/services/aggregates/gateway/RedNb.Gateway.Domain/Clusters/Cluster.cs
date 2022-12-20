using Microsoft.EntityFrameworkCore;
using RedNb.Core.Domain;
using System.ComponentModel;
using Volo.Abp.Auditing;

namespace RedNb.Gateway.Domain.Clusters;

[Table("Cluster")]
public class Cluster : BaseTreeAggregateRoot
{
    [Required]
    [MaxLength(100)]
    [Column(Order = 1)]
    public string Path { get; set; }

    internal Cluster(long id, string name, string path)
    {
        Id = id;
        Name = name;
        Path = path;
    }
}