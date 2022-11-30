using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Tests;

[Table("Node")]
public class Node : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}