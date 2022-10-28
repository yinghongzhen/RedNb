using RedNb.Auth.Application.Contracts.Permissions.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Permissions
{
    public interface IPermissionAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(PermissionAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(PermissionUpdateInputDto input);

        Task<List<AntdTreeOutputDto>> GetTreeAsync(PermissionGetTreeInputDto input);

        Task<PagedOutputDto<PermissionOutputDto>> GetPageAsync(PermissionGetPageInputDto input);

        Task<PermissionGroupOutputDto> GetTreeByRoleAsync(PermissionGetTreeByRoleInputDto input);

        Task<PermissionGroupOutputDto> GetTreeByTenantAsync(PermissionGetTreeByTenantInputDto input);

        Task AllotToRoleAsync(PermissionAllotToRoleInputDto input);

        Task AllotToTenantAsync(PermissionAllotToTenantInputDto input);
    }
}
