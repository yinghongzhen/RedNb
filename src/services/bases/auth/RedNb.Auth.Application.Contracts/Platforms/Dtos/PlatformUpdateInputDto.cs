using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Platforms.Dtos;

public class PlatformUpdateInputDto : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}
