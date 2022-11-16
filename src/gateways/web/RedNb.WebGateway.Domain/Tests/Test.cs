namespace RedNb.WebGateway.Domain.Tests;

[Table("Test")]
public class Test : AggregateRoot<long>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    private Test()
    {
    }

    internal Test(long id, string name)
    {
        Id = id;
        Name = name;
    }
}