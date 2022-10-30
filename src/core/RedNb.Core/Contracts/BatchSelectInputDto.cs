using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Core.Contracts
{
    public class BatchSelectInputDto
    {
        /// <summary>
        /// 是否所有人员
        /// </summary>
        public bool IsAllTenant { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public List<BatchArgInputDto> Users { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 部门组
        /// </summary>
        public List<BatchArgInputDto> Departments { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 角色组
        /// </summary>
        public List<BatchArgInputDto> Roles { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 身份组
        /// </summary>
        public List<BatchArgInputDto> Identitys { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 楼宇组
        /// </summary>
        public List<BatchArgInputDto> Buildings { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 位置组
        /// </summary>
        public List<BatchArgInputDto> Positions { get; set; } = new List<BatchArgInputDto>();

        /// <summary>
        /// 年份组
        /// </summary>
        public List<string> Years { get; set; } = new List<string>();
    }

    public class BatchArgInputDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string DeviceNumber { get; set; }
    }
}
