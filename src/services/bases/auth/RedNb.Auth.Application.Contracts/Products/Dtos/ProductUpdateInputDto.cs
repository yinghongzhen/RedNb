using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Products.Dtos;

public class ProductUpdateInputDto : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}
