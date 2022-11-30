using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Clusters;

[Table("HttpClient")]
public class HttpClient : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}