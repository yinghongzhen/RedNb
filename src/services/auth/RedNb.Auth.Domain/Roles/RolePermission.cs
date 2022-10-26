using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Roles
{
    /// <summary>
    /// 角色权限关联实体类
    /// </summary>
    [Table("RolePermission")]
    public class RolePermission : EntityBase
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public long PermissionId { get; set; }

        public virtual Permission Permission { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
