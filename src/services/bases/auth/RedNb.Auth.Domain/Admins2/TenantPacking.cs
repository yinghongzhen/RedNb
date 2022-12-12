using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 租户菜单包装实体类
    /// </summary>
    [Table("TenantPacking")]
    public class TenantPacking : EntityBase
    {
        [Required]
        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }

        [Required]
        public long PackingId { get; set; }

        public virtual Packing Packing { get; set; }

        public DateTime? VaildTime { get; set; }
    }
}
