using RedNb.Core.Domain.Audit;
using RedNb.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace RedNb.Core.Domain
{
    public class TreeEntity : AuditFullEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TreeName { get; set; }

        /// <summary>
        /// 全节点名
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string TreeNames { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        [Required]
        public long ParentId { get; set; }

        /// <summary>
        /// 所有父级编号
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string ParentIds { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public decimal TreeSort { get; set; }

        /// <summary>
        /// 所有排序号
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string TreeSorts { get; set; }

        /// <summary>
        /// 是否最末级
        /// </summary>
        [Required]
        public bool TreeLeaf { get; set; }

        /// <summary>
        /// 层次级别
        /// </summary>
        [Required]
        public int TreeLevel { get; set; }

        public void UpdateTreeValue(TreeEntity parent, List<TreeEntity> children, Action callback = null)
        {
            if (parent != null)
            {
                TreeNames = $"{parent.TreeNames}/{TreeName}";
                ParentIds = $"{parent.ParentIds}{ParentId},";
                TreeSorts = $"{parent.TreeSorts}{TreeSort},";
                TreeLeaf = false;
                TreeLevel = parent.TreeLevel + 1;
            }
            else
            {
                TreeNames = TreeName;
                ParentIds = $"{ParentId},";
                TreeSorts = $"{TreeSort},";
                TreeLeaf = false;
                TreeLevel = 0;
            }

            if(callback != null)
            {
                callback();
            }

            if (children != null)
            {
                foreach (var item in children)
                {
                    var childParent = children.SingleOrDefault(m => m.Id == item.ParentId) ?? this;

                    item.UpdateTreeValue(childParent, null, null);
                }
            }
        }
    }
}
