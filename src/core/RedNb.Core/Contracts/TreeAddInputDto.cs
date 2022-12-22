namespace RedNb.Core.Contracts
{
    public class TreeAddInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        [Required]
        public long ParentId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public decimal Sort { get; set; }
    }
}
