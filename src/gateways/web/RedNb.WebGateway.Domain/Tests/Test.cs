namespace RedNb.WebGateway.Domain.Tests;

public class BaseRoot : AggregateRoot<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(Order = 0)]
    public override long Id { get; protected set; }
}

[Table("Test")]
public class Test : BaseRoot
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