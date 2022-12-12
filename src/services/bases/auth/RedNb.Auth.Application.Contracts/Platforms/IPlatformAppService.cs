using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Platforms
{
    public interface IPlatformAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(PlatformAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(PlatformUpdateInputDto input);

        Task<List<PlatformOutputDto>> GetAllAsync(PlatformGetAllInputDto input);

        Task<PagedOutputDto<PlatformOutputDto>> GetPageAsync(PlatformGetPageInputDto input);
    }
}
