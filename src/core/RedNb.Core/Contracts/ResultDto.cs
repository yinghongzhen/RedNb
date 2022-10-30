using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Core.Contracts
{
    public class ResultDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// sessionId
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public byte[] TenantName { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public byte[] Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
