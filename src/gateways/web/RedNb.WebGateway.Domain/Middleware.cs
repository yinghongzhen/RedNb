using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Tests;

[Table("Middleware")]
public class Middleware : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}