using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.DictDatas.Dtos
{
    public class DictDataAddInputDto
    {
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
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
