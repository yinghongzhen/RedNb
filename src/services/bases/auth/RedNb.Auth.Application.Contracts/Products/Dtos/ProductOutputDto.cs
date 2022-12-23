using RedNb.Core.Contracts;

namespace RedNb.Auth.Application.Contracts.Products.Dtos;

public class ProductOutputDto : BaseEntityDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

}
