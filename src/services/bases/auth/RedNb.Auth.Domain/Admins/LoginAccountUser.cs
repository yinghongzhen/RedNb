using RedNb.Core.Domain;
using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using RedNb.Auth.Domain.Users;

namespace RedNb.Auth.Domain.Admins;

/// <summary>
/// 登录账户身份关联实体类
/// </summary>
[Table("LoginAccountUser")]
public class LoginAccountUser : EntityBase
{
    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long LoginAccountId { get; set; }

    public virtual LoginAccount LoginAccount { get; set; }
}
