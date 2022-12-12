using RedNb.Auth.Application.Contracts.DictDatas.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.DictTypes.Dtos
{
    public class DictTypeUpdateInputDto : EntityDto<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        /// <summary>
        /// 是否系统字典
        /// </summary>
        [Required]
        public bool IsSystem { get; set; }

        [Required]
        public List<DictDataUpdateInputDto> DictDataList { get; set; }
    }
}
