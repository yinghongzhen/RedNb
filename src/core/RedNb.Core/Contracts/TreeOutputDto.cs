namespace RedNb.Core.Contracts;

public class TreeOutputDto<T> : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 全节点名
    /// </summary>
    public string Names { get; set; }

    /// <summary>
    /// 父级编号
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 层次级别
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 是否最末级
    /// </summary>
    public bool IsLast { get; set; }

    public List<T> Children { get; set; }
}
