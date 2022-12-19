namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 平台实体类
    /// </summary>
    [Table("Platform")]
    public class Platform : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Key { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public decimal Sort { get; set; }
    }
}
