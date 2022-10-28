using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Tenants
{
    public interface ITenantAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(TenantAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(TenantUpdateInputDto input);

        Task<TenantOutputDto> GetDetailAsync(GetDetailInputDto input);

        Task<List<TenantOutputDto>> GetAllAsync(TenantGetAllInputDto input);

        Task<PagedOutputDto<TenantOutputDto>> GetPageAsync(TenantGetPageInputDto input);

        Task<TenantOutputDto> GetDetailCacheAsync(GetDetailInputDto input);
    }
}
