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
    /// 用户数据权限实体类
    /// </summary>
    [Table("UserDataScope")]
    public class UserDataScope : EntityBase, IHasTenant
    {
        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
