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
    /// 限流实体类
    /// </summary>
    [Table("RateLimit")]
    public class RateLimit : EntityBase
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [Required]
        public string Version { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        [Required]
        public string Path { get; set; }
    }
}
