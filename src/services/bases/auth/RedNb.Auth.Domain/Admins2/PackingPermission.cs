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
    /// 菜单包装-权限关系实体类
    /// </summary>
    [Table("PackingPermission")]
    public class PackingPermission : EntityBase
    {
        [Required]
        public long PackingId { get; set; }

        public virtual Packing Packing { get; set; }

        [Required]
        public long PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
