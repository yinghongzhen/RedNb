using RedNb.Auth.Application.Contracts.Posts;
using RedNb.Auth.Application.Contracts.Posts.Dtos;
using RedNb.Core.Contracts;
using RedNb.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RedNb.Auth.Domain.Offices;

namespace RedNb.Auth.Application.Posts
{
    public class PostAppService : IPostAppService
    {
        private readonly IRepository<Post, long> _postRepository;
        private readonly IObjectMapper _objectMapper;

        public PostAppService(IRepository<Post, long> postRepository,
            IObjectMapper objectMapper)
        {
            _postRepository = postRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(PostAddInputDto input)
        {
            if (await _postRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编号已存在，添加失败");
            }

            var model = _objectMapper.Map<PostAddInputDto, Post>(input);

            model.CreateKey();

            await _postRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var system = await _postRepository.GetAsync(item);

                await _postRepository.DeleteAsync(system);
            }
        }

        public async Task UpdateAsync(PostUpdateInputDto input)
        {
            var model = await _postRepository.GetAsync(input.Id);

            if (await _postRepository.AnyAsync(m =>
                m.Key == input.Key &&
                m.Id != model.Id))
            {
                throw new UserFriendlyException("编号已存在，添加失败");
            }

            _objectMapper.Map(input, model);
        }

        public async Task<List<PostOutputDto>> GetAllAsync(PostGetAllInputDto input)
        {
            var queryable = await _postRepository.GetQueryableAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Post>, List<PostOutputDto>>(list);

            return data;
        }

        public async Task<PagedOutputDto<PostOutputDto>> GetPageAsync(PostGetPageInputDto input)
        {
            var queryable = await _postRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Post>, List<PostOutputDto>>(list);

            return new PagedOutputDto<PostOutputDto>(count, data);
        }
    }
}
