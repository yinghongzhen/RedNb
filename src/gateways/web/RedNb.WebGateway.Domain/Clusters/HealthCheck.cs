using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("HealthCheck")]
public class HealthCheck : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}