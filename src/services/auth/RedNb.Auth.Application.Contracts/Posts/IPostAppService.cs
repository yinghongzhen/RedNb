using RedNb.Auth.Application.Contracts.Posts.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Auth.Application.Contracts.Posts
{
    public interface IPostAppService : IApplicationService, ITransientDependency
    {
        Task AddAsync(PostAddInputDto input);

        Task DeleteBatchAsync(DeleteBatchInputDto input);

        Task UpdateAsync(PostUpdateInputDto input);

        Task<List<PostOutputDto>> GetAllAsync(PostGetAllInputDto input);

        Task<PagedOutputDto<PostOutputDto>> GetPageAsync(PostGetPageInputDto input);
    }
}
