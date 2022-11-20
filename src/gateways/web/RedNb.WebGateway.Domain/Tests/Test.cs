using RedNb.Core.Domain;

namespace RedNb.WebGateway.Domain.Tests;

[Table("Test")]
public class Test: EntityBase
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}