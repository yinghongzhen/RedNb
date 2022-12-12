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
    /// 菜单包装-模块关系实体类
    /// </summary>
    [Table("PackingModule")]
    public class PackingModule : EntityBase
    {
        [Required]
        public long PackingId { get; set; }

        public virtual Packing Packing { get; set; }

        [Required]
        public long ModuleId { get; set; }

        public virtual Module Module { get; set; } 
    }
}
