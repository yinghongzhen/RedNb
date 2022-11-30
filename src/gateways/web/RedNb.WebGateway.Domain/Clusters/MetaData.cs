using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("MetaData")]
public class MetaData : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}