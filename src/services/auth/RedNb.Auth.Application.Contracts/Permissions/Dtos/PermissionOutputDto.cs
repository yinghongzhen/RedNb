using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Permissions.Dtos
{
    public class PermissionOutputDto : TreeOutputDto<PermissionOutputDto>
    {
        public EPermissionType Type { get; set; }

        public string TypeStr { get; set; }

        public string Key { get; set; }

        public string TreeKeys { get; set; }

        public string Path { get; set; }

        public string FullPath { get; set; }

        /// <summary>
        /// 路由组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 附带参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 允许关闭
        /// </summary>
        public bool Closeable { get; set; }

        public long PlatformId { get; set; }

        public long ModuleId { get; set; }
    }
}
