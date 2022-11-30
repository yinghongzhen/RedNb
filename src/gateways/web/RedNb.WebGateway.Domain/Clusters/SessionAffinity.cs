using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("SessionAffinity")]
public class SessionAffinity : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}