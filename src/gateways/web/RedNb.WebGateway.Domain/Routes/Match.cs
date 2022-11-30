using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Routes;

[Table("Match")]
public class Match : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}