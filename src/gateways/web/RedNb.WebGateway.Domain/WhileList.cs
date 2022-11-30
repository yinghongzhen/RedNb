using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Tests;

[Table("WhileList")]
public class WhileList : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}