using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Routes;

[Table("Transform")]
public class Transform : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}