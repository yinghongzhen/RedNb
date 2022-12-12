using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Roles
{
    /// <summary>
    /// 角色数据权限实体类
    /// </summary>
    [Table("RoleDataScope")]
    public class RoleDataScope : EntityBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
