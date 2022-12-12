using RedNb.Auth.Application.Contracts.Departments.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Departments
{
    public interface IDepartmentAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(DepartmentAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(DepartmentUpdateInputDto input);

        Task<List<AntdTreeOutputDto>> GetTreeAsync(DepartmentGetTreeInputDto input);

        Task<PagedOutputDto<DepartmentOutputDto>> GetPageAsync(DepartmentGetPageInputDto input);
    }
}
