using RedNb.Core.Domain;

namespace RedNb.Gateway.Domain.Clusters;

[Table("MetaData")]
public class MetaData : EntityBase
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public virtual Cluster Cluster { get; set; }
}