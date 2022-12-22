namespace RedNb.Gateway.Domain.Clusters;

[Table("Cluster")]
public class Cluster : TreeAggregateRoot
{
    [Required]
    [MaxLength(100)]
    public string Path { get; set; }
}