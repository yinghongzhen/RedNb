using RedNb.Auth.Application.Contracts.Caches.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Caches
{
    public interface ICacheAppService : IApplicationService, ITransientDependency
    {
        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task<List<CacheOutputDto>> GetAllKeyAsync();

        Task<PagedOutputDto<CacheOutputDto>> GetPageAsync(CacheGetPageInputDto input);
    }
}
