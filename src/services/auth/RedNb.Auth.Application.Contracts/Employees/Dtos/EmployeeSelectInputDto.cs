using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Employees.Dtos
{
    public class EmployeeSelectInputDto
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 工号、学号
        /// </summary>
        public List<string> Numbers { get; set; }

        /// <summary>
        /// 模糊查询工号、学号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public EManagerType? Type { get; set; }

        /// <summary>
        /// 排除用户类型
        /// </summary>
        public EManagerType? NoType { get; set; }

        /// <summary>
        /// 模糊查询真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 精准查询真实姓名
        /// </summary>
        public List<string> RealNames { get; set; }

        /// <summary>
        /// 模糊查询身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 精准查询身份证
        /// </summary>
        public List<string> IdCards { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex? Sex { get; set; }

        /// <summary>
        /// 模糊查询硬件编码
        /// </summary>
        public string DeviceNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public List<long> UserIds { get; set; }

        /// <summary>
        /// 有人脸照片
        /// </summary>
        public bool? HasFace { get; set; }

        /// <summary>
        /// 需要展示岗位
        /// </summary>
        public bool? IsPost { get; set; }

        /// <summary>
        /// 需要展示部门
        /// </summary>
        public bool? IsDepartment { get; set; }

        /// <summary>
        /// 需要展示床位信息
        /// </summary>
        public bool? IsCw { get; set; }
    }
}
