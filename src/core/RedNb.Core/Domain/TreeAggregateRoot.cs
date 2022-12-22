using System.Diagnostics;

namespace RedNb.Core.Domain;

public class TreeAggregateRoot : BaseAggregateRoot
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
    public string Names { get; set; }

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
    public int Sort { get; set; }

    /// <summary>
    /// 所有排序号
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Sorts { get; set; }

    /// <summary>
    /// 层次级别
    /// </summary>
    [Required]
    public int Level { get; set; }

    /// <summary>
    /// 是否最末级
    /// </summary>
    [Required]
    public bool IsLast { get; set; }

    private string FormatSort(decimal sort)
    {
        return sort.ToString("0000000000");
    }

    public void UpdateTreeValue(TreeAggregateRoot parent, List<TreeAggregateRoot> children)
    {
        if (parent != null)
        {
            Names = $"{parent.Names}/{Name}";
            ParentIds = $"{parent.ParentIds}{ParentId},";
            Sorts = $"{parent.Sorts}{FormatSort(Sort)},";
            Level = parent.Level + 1;

            parent.IsLast = false;
        }
        else
        {
            Names = Name;
            ParentIds = $"{ParentId},";
            Sorts = $"{FormatSort(Sort)},";
            Level = 0;
        }

        if (children != null && children.Any())
        {
            foreach (var item in children)
            {
                var childParent = children.SingleOrDefault(m => m.Id == item.ParentId) ?? this;

                item.UpdateTreeValue(childParent, null);
            }

            IsLast = false;
        }
        else
        {
            IsLast = true;
        }
    }

    public void DeleteNode(TreeAggregateRoot parent)
    {
        if (parent != null)
        {
            Names = $"{parent.Names}/{Name}";
            ParentIds = $"{parent.ParentIds}{ParentId},";
            Sorts = $"{parent.Sorts}{Sort},";
            Level = parent.Level + 1;
            IsLast = true;
        }
        else
        {
            Names = Name;
            ParentIds = $"{ParentId},";
            Sorts = $"{Sort},";
            Level = 0;
            IsLast = true;
        }
    }
}
