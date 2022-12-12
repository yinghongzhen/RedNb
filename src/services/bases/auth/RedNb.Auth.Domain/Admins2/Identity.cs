using RedNb.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 身份实体类
    /// </summary>
    [Table("Identity")]
    public class Identity : EntityBase, IHasTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(100)]
        public string Desc { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        [Required]
        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
