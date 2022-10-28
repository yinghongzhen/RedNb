using RedNb.Auth.Application.Contracts.Employees.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Employees
{
    public interface IEmployeeAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(EmployeeAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(EmployeeUpdateInputDto input);

        Task<EmployeeOutputDto> GetDetailAsync(GetDetailInputDto input);

        Task<EmployeeOutputDto> GetDetailByUserAsync(GetDetailInputDto input);

        Task<List<EmployeeOutputDto>> GetAllAsync();

        Task<PagedOutputDto<EmployeeOutputDto>> GetPageAsync(EmployeeGetPageInputDto input);
    }
}
