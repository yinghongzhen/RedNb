namespace RedNb.Core.Domain;

public class TreeEntity : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 全节点名
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string TreeNames { get; set; }

    /// <summary>
    /// 父级编号
    /// </summary>
    [Required]
    public long ParentId { get; set; }

    /// <summary>
    /// 所有父级编号
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string ParentIds { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public decimal Sort { get; set; }

    /// <summary>
    /// 所有排序号
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string TreeSorts { get; set; }

    /// <summary>
    /// 是否最末级
    /// </summary>
    [Required]
    public bool TreeLeaf { get; set; }

    /// <summary>
    /// 层次级别
    /// </summary>
    [Required]
    public int TreeLevel { get; set; }
}
