using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Modules.Dtos
{
    public class ModuleUpdateInputDto : EntityDto<long>
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public EModuleType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Key { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Version { get; set; }

        /// <summary>
        /// 是否必选
        /// </summary>
        [Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 注册回调
        /// </summary>
        [MaxLength(500)]
        public string RegCallback { get; set; }

        /// <summary>
        /// 服务实例
        /// </summary>
        [Required]
        public List<InstanceUpdateInputDto> InstanceList { get; set; }
    }
}
