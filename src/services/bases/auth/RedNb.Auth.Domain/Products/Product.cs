using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp;

namespace RedNb.Auth.Domain.Products;

/// <summary>
/// 产品实体类
/// </summary>
[Table("Product")]
public class Product : BaseAggregateRoot
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public virtual List<Platform> Platforms { get; protected set; } = new List<Platform>();

    public void AddPlatform(Platform input)
    {
        Platforms.Add(input);
    }

    public void DeletePlatform(long id)
    {
        var platform = Platforms.SingleOrDefault(m => m.Id == id);

        if(platform != null)
        {
            Platforms.Remove(platform);
        }
    }
}