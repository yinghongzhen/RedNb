namespace RedNb.WebGateway.Domain.Test2s;

[Table("Test2")]
public class Test2 : AggregateRoot<Guid>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    private Test2()
    {
    }

    internal Test2(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}