using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("HttpRequest")]
public class HttpRequest : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}