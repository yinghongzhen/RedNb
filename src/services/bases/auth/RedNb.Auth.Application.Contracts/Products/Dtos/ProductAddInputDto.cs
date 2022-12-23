using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Products.Dtos;

public class ProductAddInputDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

}
