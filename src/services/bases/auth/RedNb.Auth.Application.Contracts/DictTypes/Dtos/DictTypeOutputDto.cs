using RedNb.Auth.Application.Contracts.DictDatas.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.DictTypes.Dtos
{
    public class DictTypeOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否系统字典
        /// </summary>
        public bool IsSystem { get; set; }

        public List<DictDataOutputDto> DictDataList { get; set; }
    }
}
