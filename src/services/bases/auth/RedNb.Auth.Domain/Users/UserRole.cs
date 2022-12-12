using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Roles;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Users
{
    /// <summary>
    /// 用户角色关联实体类
    /// </summary>
    [Table("UserRole")]
    public class UserRole : EntityBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
