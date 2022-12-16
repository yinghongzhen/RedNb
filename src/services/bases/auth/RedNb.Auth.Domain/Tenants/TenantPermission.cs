using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RedNb.Auth.Domain.Menus;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Tenants
{
    /// <summary>
    /// 租户权限实体类，定义最大权限
    /// </summary>
    [Table("TenantPermission")]
    public class TenantPermission : EntityBase, IHasTenant
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public long PermissionId { get; set; }

        public virtual Permission Permission { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
