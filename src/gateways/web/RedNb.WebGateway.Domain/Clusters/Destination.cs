using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("Destination")]
public class Destination : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}