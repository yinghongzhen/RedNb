using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Users
{
    public interface IUserAppService : IApplicationService, ITransientDependency
    {
        Task<UserOutputDto> AddAsync(UserAddInputDto input);

        Task UpdateAsync(UserUpdateInputDto input);

        Task UpdateByAsync(UserUpdateInputDto input);

        Task UpdatePasswordAsync(UserUpdatePasswordInputDto input);

        Task UpdateRealNameAsync(UserUpdateRealNameInputDto input);

        Task UpdateMobileAsync(UserUpdateMobileInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task DeleteBatchByAsync(DeleteBatchInputDto input);

        Task<UserOutputDto> GetDetailAsync(GetDetailInputDto input);

        Task<UserOutputDto> GetDetailByAsync(GetDetailInputDto input);

        Task<PagedOutputDto<UserOutputDto>> GetPageAsync(UserGetPageInputDto input);

        Task<List<UserOutputDto>> GetListByAsync(UserGetListInputDto input);

        Task<List<UserOutputDto>> GetListByReferenceAsync(BatchInputDto input);

        Task<int> GetUserCount(GetDetailInputDto input);

        Task<List<UserOutputDto>> GetAllByRoleKeyAsync(UserGetAllByRoleKeyInputDto input);

        Task<PagedOutputDto<UserRoleOutputDto>> GetPageByRoleAsync(UserGetPageByRoleInputDto input);

        Task<PagedOutputDto<UserOutputDto>> GetPageNotByRoleAsync(UserGetPageByRoleInputDto input);

        Task AddRoleBatchAsync(UserAddRoleBatchInputDto input);

        Task DeleteRoleBatchAsync(DeleteBatchInputDto input);
    }
}
