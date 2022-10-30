using RedNb.Core.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace RedNb.Core.Domain;

public class EntityBase : Entity<long>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(Order = 0)]
    public override long Id { get; protected set; }

    public void CreateKey()
    {
        Id = IdentityManager.NewId();
    }
}
