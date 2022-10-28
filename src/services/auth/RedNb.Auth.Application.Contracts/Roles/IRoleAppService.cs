using RedNb.Auth.Application.Contracts.Roles.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Roles
{
    public interface IRoleAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(RoleAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(RoleUpdateInputDto input);

        Task<List<RoleOutputDto>> GetAllAsync(RoleGetAllInputDto input);

        Task<List<RoleOutputDto>> GetAllByAsync(RoleGetAllByInputDto input);

        Task<PagedOutputDto<RoleOutputDto>> GetPageAsync(RoleGetPageInputDto input);

        Task AddUserBatchAsync(RoleAddUserBatchInputDto input);
    }
}
