using RedNb.Auth.Application.Contracts.Modules.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Modules
{
    public interface IModuleAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(ModuleAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(ModuleUpdateInputDto input);

        Task<List<ModuleOutputDto>> GetAllAsync(ModuleGetAllInputDto input);

        Task<PagedOutputDto<ModuleOutputDto>> GetPageAsync(ModuleGetPageInputDto input);
    }
}
