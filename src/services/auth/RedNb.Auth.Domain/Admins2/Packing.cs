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
    /// 菜单包装实体类
    /// </summary>
    [Table("Packing")]
    public class Packing : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
