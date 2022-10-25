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
    /// 权限接口关系实体类
    /// </summary>
    [Table("PermissionApi")]
    public class PermissionApi : EntityBase
    {
        public long ApiId { get; set; }

        public virtual Api Api { get; set; }

        public long PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
