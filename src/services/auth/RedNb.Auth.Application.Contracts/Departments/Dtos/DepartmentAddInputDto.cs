using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Departments.Dtos
{
    public class DepartmentAddInputDto : TreeAddInputDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(100)]
        public string Leader { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [MaxLength(100)]
        public string Phone { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [MaxLength(100)]
        public string ZipCode { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(300)]
        public string Email { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(500)]
        public string Desc { get; set; }
    }
}
