using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Companys
{
    public interface ICompanyAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(CompanyAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(CompanyUpdateInputDto input);

        Task<List<AntdTreeOutputDto>> GetTreeAsync(CompanyGetTreeInputDto input);

        Task<PagedOutputDto<CompanyOutputDto>> GetPageAsync(CompanyGetPageInputDto input);
    }
}
