namespace RedNb.Core.Contracts;

public class TreeUpdateInputDto : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 父级编号
    /// </summary>
    [Required]
    public long ParentId { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public int Sort { get; set; }
}
