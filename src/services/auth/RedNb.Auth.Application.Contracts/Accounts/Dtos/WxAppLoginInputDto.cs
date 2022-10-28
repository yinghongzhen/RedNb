using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class WxAppLoginInputDto
    {
        [StringLength(255)]
        public string WxUnionId { get; set; }

        [Required]
        [StringLength(255)]
        public string WxAppOpenId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        public long TenantId { get; set; }
    }
}
