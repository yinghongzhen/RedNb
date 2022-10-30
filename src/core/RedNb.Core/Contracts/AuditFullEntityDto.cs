using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class AuditFullEntityDto : EntityBaseDto
    {
        /// <summary>
        /// 创建者编号
        /// </summary>
        public long CreateId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者编号
        /// </summary>
        public long UpdateId { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
