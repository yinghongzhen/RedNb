using RedNb.Auth.Application.Contracts.Configs.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Configs
{
    public interface IConfigAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(ConfigAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(ConfigUpdateInputDto input);

        Task<PagedOutputDto<ConfigOutputDto>> GetPageAsync(ConfigGetPageInputDto input);
    }
}
