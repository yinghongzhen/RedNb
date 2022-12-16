using RedNb.Auth.Application.Contracts.Configs;
using RedNb.Auth.Application.Contracts.Configs.Dtos;
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
using RedNb.Auth.Domain.Configs;

namespace RedNb.Auth.Application.Configs
{
    public class ConfigAppService : IConfigAppService
    {
        private readonly IRepository<Config, long> _configRepository;
        private readonly IObjectMapper _objectMapper;

        public ConfigAppService(IRepository<Config, long> configRepository,
            IObjectMapper objectMapper)
        {
            _configRepository = configRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(ConfigAddInputDto input)
        {
            if (await _configRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = _objectMapper.Map<ConfigAddInputDto, Config>(input);

            model.CreateKey();

            await _configRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var config = await _configRepository.GetAsync(item);

                await _configRepository.DeleteAsync(config);
            }
        }

        public async Task UpdateAsync(ConfigUpdateInputDto input)
        {
            if (await _configRepository.AnyAsync(m =>
                m.Key == input.Key &&
                m.Id != input.Id))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = await _configRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);
        }

        public async Task<PagedOutputDto<ConfigOutputDto>> GetPageAsync(ConfigGetPageInputDto input)
        {
            var queryable = await _configRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Config>, List<ConfigOutputDto>>(list);

            return new PagedOutputDto<ConfigOutputDto>(count, data);
        }
    }
}