using RedNb.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 用户身份实体类
    /// </summary>
    [Table("UserIdentity")]
    public class UserIdentity : EntityBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        public virtual User User { get; set; }

        /// <summary>
        /// 身份Id
        /// </summary>
        [Required]
        public long IdentityId { get; set; }

        public virtual Identity Identity { get; set; }
    }
}
