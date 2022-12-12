using RedNb.Auth.Application.Contracts.Modules;
using RedNb.Auth.Application.Contracts.Modules.Dtos;
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

namespace RedNb.Auth.Application.Modules
{
    public class ModuleAppService : IModuleAppService
    {
        private readonly IRepository<Module, long> _moduleRepository;
        private readonly IRepository<Instance, long> _instanceRepository;
        private readonly IRepository<TenantModule, long> _tenantModuleRepository;
        private readonly IObjectMapper _objectMapper;

        public ModuleAppService(IRepository<Module, long> moduleRepository,
            IRepository<Instance, long> instanceRepository,
            IRepository<TenantModule, long> tenantModuleRepository,
            IObjectMapper objectMapper)
        {
            _moduleRepository = moduleRepository;
            _instanceRepository = instanceRepository;
            _tenantModuleRepository = tenantModuleRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(ModuleAddInputDto input)
        {
            if (await _moduleRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = _objectMapper.Map<ModuleAddInputDto, Module>(input);
            model.CreateKey();

            if (input.InstanceList != null)
            {
                foreach (var item in input.InstanceList)
                {
                    var instance = _objectMapper.Map<InstanceAddInputDto, Instance>(item);

                    instance.CreateKey();
                    instance.ModuleId = model.Id;

                    await _instanceRepository.InsertAsync(instance);
                }
            }

            await _moduleRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var module = await _moduleRepository.GetAsync(item);

                if (await _tenantModuleRepository
                    .AnyAsync(m => m.ModuleId == module.Id))
                {
                    throw new UserFriendlyException("模块已绑定到租户，禁止删除");
                }

                await _moduleRepository.DeleteAsync(module);
            }
        }

        public async Task UpdateAsync(ModuleUpdateInputDto input)
        {
            if (await _moduleRepository.AnyAsync(m =>
                m.Key == input.Key &&
                m.Id != input.Id))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = await _moduleRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);

            if (input.InstanceList.Any())
            {
                var ids = input.InstanceList.Where(m => m.Id != 0).Select(m => m.Id).ToList();

                var queryable = await _instanceRepository.GetQueryableAsync();

                var deleteList = await queryable
                    .Where(m => m.ModuleId == model.Id &&
                    !ids.Contains(m.Id))
                    .ToListAsync();

                foreach (var item in deleteList)
                {
                    await _instanceRepository.DeleteAsync(item);
                }

                foreach (var item in input.InstanceList)
                {
                    if (item.Id == 0)
                    {
                        var instance = _objectMapper.Map<InstanceUpdateInputDto, Instance>(item);

                        instance.CreateKey();
                        instance.ModuleId = model.Id;

                        await _instanceRepository.InsertAsync(instance);
                    }
                    else
                    {
                        var instance = await _instanceRepository
                            .SingleOrDefaultAsync(m => m.Id == item.Id);

                        _objectMapper.Map(item, instance);
                    }
                }
            }
        }

        public async Task<List<ModuleOutputDto>> GetAllAsync(ModuleGetAllInputDto input)
        {
            var queryable = await _moduleRepository.GetQueryableAsync();

            var list = await queryable
                .OrderBy(m => m.Type)
                .ThenByDescending(m => m.IsRequired)
                .ThenByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Module>, List<ModuleOutputDto>>(list);

            return data;
        }

        public async Task<PagedOutputDto<ModuleOutputDto>> GetPageAsync(ModuleGetPageInputDto input)
        {
            var queryable = await _moduleRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderBy(m => m.Type)
                .ThenByDescending(m => m.IsRequired)
                .ThenByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Module>, List<ModuleOutputDto>>(list);

            var moduleIds = data.Select(m => m.Id).ToList();

            var instanceQueryable = await _instanceRepository.GetQueryableAsync();

            var instanceList = await instanceQueryable
                .Where(m => moduleIds.Contains(m.ModuleId))
                .OrderBy(m => m.Id)
                .ToListAsync();

            foreach (var item in data)
            {
                var tInstances = instanceList.Where(m => m.ModuleId == item.Id).ToList();

                item.InstanceList = _objectMapper.Map<List<Instance>, List<InstanceOutputDto>>(tInstances);
            }

            return new PagedOutputDto<ModuleOutputDto>(count, data);
        }
    }
}