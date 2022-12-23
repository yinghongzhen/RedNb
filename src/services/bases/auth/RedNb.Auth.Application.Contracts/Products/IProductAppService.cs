using RedNb.Auth.Application.Contracts.Products.Dtos;
using RedNb.Core.Contracts;

namespace RedNb.Auth.Application.Contracts.Products;

public interface IProductAppService : IApplicationService, ITransientDependency
{
    Task AddAsync(ProductAddInputDto input);

    Task DeleteAsync(DeleteInputDto input);

    Task UpdateAsync(ProductUpdateInputDto input);

    Task<List<ProductOutputDto>> GetAllAsync(ProductGetAllInputDto input);
}