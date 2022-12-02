using RedNb.Core.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace RedNb.Core.Domain;

public class AggregateRootBase : AggregateRoot<long>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(Order = 0)]
    public override long Id { get; protected set; }
}
