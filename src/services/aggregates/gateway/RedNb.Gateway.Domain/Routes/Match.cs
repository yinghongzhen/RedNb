using RedNb.Core.Domain;

namespace RedNb.Gateway.Domain.Routes;

[Table("Match")]
public class Match : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public virtual Route Route { get; set; }
}