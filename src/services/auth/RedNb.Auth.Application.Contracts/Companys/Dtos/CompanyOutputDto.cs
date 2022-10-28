using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Companys.Dtos
{
    public class CompanyOutputDto : TreeOutputDto<CompanyOutputDto>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 公司全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }

        public bool IsDeleted { get; set; }
    }
}
