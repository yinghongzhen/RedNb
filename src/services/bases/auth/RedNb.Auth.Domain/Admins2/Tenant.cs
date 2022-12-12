using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 租户实体类
    /// </summary>
    [Table("Tenant")]
    public class Tenant : AuditFullEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Key { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 是否系统
        /// </summary>
        [Required]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        [Required]
        public EUsageStatus Status { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [MaxLength(80)]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(80)]
        public string City { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [MaxLength(80)]
        public string District { get; set; }

        /// <summary>
        /// 父级租户Id
        /// </summary>
        public long ParentId { get; set; }
    }
}
