using RedNb.Core.Domain;

namespace RedNb.Gateway.Domain.Routes;

[Table("Route")]
public class Route : BaseAggregateRoot
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}