using RedNb.Auth.Application.Contracts.DictTypes.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.DictTypes
{
    public interface IDictTypeAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(DictTypeAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(DictTypeUpdateInputDto input);

        Task<PagedOutputDto<DictTypeOutputDto>> GetPageAsync(DictTypeGetPageInputDto input);
    }
}
