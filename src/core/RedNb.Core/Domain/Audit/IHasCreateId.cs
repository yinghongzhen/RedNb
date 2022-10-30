using RedNb.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace RedNb.Core.Domain.Audit
{
    public interface IHasCreateId
    {
        /// <summary>
        /// 创建者编号
        /// </summary>
        public long CreateId { get; set; }
    }
}
