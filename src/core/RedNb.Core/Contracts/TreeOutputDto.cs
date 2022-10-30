using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class TreeOutputDto<T> : AuditFullEntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string TreeName { get; set; }

        /// <summary>
        /// 全节点名
        /// </summary>
        public string TreeNames { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 所有父级编号
        /// </summary>
        public string ParentIds { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public decimal TreeSort { get; set; }

        /// <summary>
        /// 所有排序号
        /// </summary>
        public string TreeSorts { get; set; }

        /// <summary>
        /// 是否最末级
        /// </summary>
        public bool TreeLeaf { get; set; }

        /// <summary>
        /// 层次级别
        /// </summary>
        public int TreeLevel { get; set; }

        public List<T> Children { get; set; }
    }
}
