using RedNb.Auth.Application.Contracts.Apis.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Apis
{
    public interface IApiAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(ApiAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(ApiUpdateInputDto input);

        Task<PagedOutputDto<ApiOutputDto>> GetPageAsync(ApiGetPageInputDto input);

        Task SyncAsync(BatchInputDto input);
    }
}
