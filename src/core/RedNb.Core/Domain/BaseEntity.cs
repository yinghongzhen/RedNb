namespace RedNb.Core.Domain;

public class BaseEntity : Entity<long>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(Order = 0)]
    [Comment("主键")]
    public override long Id { get; protected set; }
}