using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace RedNb.Auth.Domain.Offices;

/// <summary>
/// 员工岗位关联实体类
/// </summary>
[Table("EmployeePost")]
public class EmployeePost : Entity
{
    /// <summary>
    /// 岗位编号
    /// </summary>
    public long PostId { get; set; }

    public virtual Post Post { get; set; }

    /// <summary>
    /// 员工编号
    /// </summary>
    public long EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { PostId, EmployeeId };
    }
}
