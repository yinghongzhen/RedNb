using System;
using System.Collections.Generic;
using System.Text;

namespace RedNb.Core.Contracts
{
    /// <summary>
    /// 发布任务传输类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MqDto<T>
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string Key { get; set; }

        public long TenantId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        public long? CreateId { get; set; }

        public string CreateName { get; set; }
    }

    public class MqObjDto
    {
        public long TenantId { get; set; }

        public bool IsDelete { get; set; }

        public long KeyId { get; set; }

        /// <summary>
        /// 硬件id 明确硬件数据变化
        /// </summary>
        public long? DeviceId { get; set; }

        public List<long> Ids { get; set; }
    }
}
