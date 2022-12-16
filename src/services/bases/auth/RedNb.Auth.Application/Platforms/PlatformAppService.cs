using RedNb.Auth.Application.Contracts.Platforms;
using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RestSharp;
using RedNb.Auth.Domain.Menus;

namespace RedNb.Auth.Application.Platforms
{
    public class PlatformAppService : IPlatformAppService
    {
        private readonly IRepository<Platform, long> _platformRepository;
        private readonly IRepository<Permission, long> _permissionRepository;
        private readonly IObjectMapper _objectMapper;

        public PlatformAppService(IRepository<Platform, long> platformRepository,
            IRepository<Permission, long> permissionRepository,
            IObjectMapper objectMapper)
        {
            _platformRepository = platformRepository;
            _permissionRepository = permissionRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(PlatformAddInputDto input)
        {
            if (await _platformRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = _objectMapper.Map<PlatformAddInputDto, Platform>(input);
            model.CreateKey();

            await _platformRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var platform = await _platformRepository.GetAsync(item);

                if (await _permissionRepository
                    .AnyAsync(m => m.PlatformId == platform.Id))
                {
                    throw new UserFriendlyException("平台已配置权限，禁止删除");
                }

                await _platformRepository.DeleteAsync(platform);
            }
        }

        public async Task UpdateAsync(PlatformUpdateInputDto input)
        {
            if (await _platformRepository.AnyAsync(m =>
                m.Key == input.Key &&
                m.Id != input.Id))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = await _platformRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);
        }

        public async Task<List<PlatformOutputDto>> GetAllAsync(PlatformGetAllInputDto input)
        {
            var queryable = await _platformRepository.GetQueryableAsync();

            var list = await queryable
                .OrderByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Platform>, List<PlatformOutputDto>>(list);

            return data;
        }

        public async Task<PagedOutputDto<PlatformOutputDto>> GetPageAsync(PlatformGetPageInputDto input)
        {
            var queryable = await _platformRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Platform>, List<PlatformOutputDto>>(list);

            return new PagedOutputDto<PlatformOutputDto>(count, data);
        }
    }
}