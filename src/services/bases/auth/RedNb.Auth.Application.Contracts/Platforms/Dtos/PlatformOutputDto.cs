using RedNb.Core.Contracts;

namespace RedNb.Auth.Application.Contracts.Platforms.Dtos;

public class PlatformOutputDto : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    public long ProductId { get; set; }
}
