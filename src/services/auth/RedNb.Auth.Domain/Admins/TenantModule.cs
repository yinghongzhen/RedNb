using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 租户模块实体类
    /// </summary>
    [Table("TenantModule")]
    public class TenantModule : EntityBase, IHasTenant
    {
        public long ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
