using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Routes;

[Table("MetaData")]
public class MetaData : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}