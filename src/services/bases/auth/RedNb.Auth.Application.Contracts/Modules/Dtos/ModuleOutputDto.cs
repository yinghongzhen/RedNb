using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Modules.Dtos
{
    public class ModuleOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public EModuleType Type { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string TypeStr { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 是否必选
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 注册回调
        /// </summary>
        public string RegCallback { get; set; }

        /// <summary>
        /// 服务实例
        /// </summary>
        public List<InstanceOutputDto> InstanceList { get; set; }
    }
}
