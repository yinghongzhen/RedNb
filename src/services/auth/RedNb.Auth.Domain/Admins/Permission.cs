using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using Volo.Abp;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    [Table("Permission")]
    public class Permission : TreeEntity, ISoftDelete
    {
        /// <summary>
        /// 权限类型
        /// </summary>
        [Required]
        public EPermissionType Type { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string TreeKeys { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; }

        /// <summary>
        /// 路由组件
        /// </summary>
        [MaxLength(200)]
        public string Component { get; set; }

        /// <summary>
        /// 附带参数
        /// </summary>
        [MaxLength(1000)]
        public string Params { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(100)]
        public string Icon { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [MaxLength(50)]
        public string Color { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        public bool IsShow { get; set; }

        /// <summary>
        /// 允许关闭
        /// </summary>
        [Required]
        public bool Closeable { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(200)]
        public string Desc { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public long ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public long PlatformId { get; set; }

        public virtual Platform Platform { get; set; }
    }
}
