using RedNb.Auth.Application.Contracts.Tenants;
using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Extensions;
using RedNb.Core.Util;
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
using RedNb.Core.Domain;
using RedNb.Auth.Domain.Shared;
using RedNb.Auth.Application.Contracts.Users;
using RedNb.Auth.Application.Contracts.Users.Dtos;

namespace RedNb.Auth.Application.Tenants
{
    public class TenantAppService : ITenantAppService
    {
        private readonly IRepository<Tenant, long> _tenantRepository;
        private readonly IRepository<TenantModule, long> _tenantModuleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _userAppService;

        public TenantAppService(IRepository<Tenant, long> tenantRepository,
            IRepository<TenantModule, long> tenantModuleRepository,
            IRepository<User, long> userRepository,
            IObjectMapper objectMapper,
            IUserAppService userAppService)
        {
            _tenantRepository = tenantRepository;
            _tenantModuleRepository = tenantModuleRepository;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
            _userAppService = userAppService;
        }

        public async Task AddAsync(TenantAddInputDto input)
        {
            if (await _tenantRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = _objectMapper.Map<TenantAddInputDto, Tenant>(input);

            model.CreateKey();
            model.IsSystem = false;
            model.IsActive = true;

            if (input.ModuleIds != null)
            {
                foreach (var item in input.ModuleIds)
                {
                    var tenantModule = new TenantModule()
                    {
                        ModuleId = item,
                        TenantId = model.Id
                    };

                    tenantModule.CreateKey();

                    await _tenantModuleRepository.InsertAsync(tenantModule);
                }
            }

            await _userAppService.AddAsync(new UserAddInputDto()
            {
                ManagerType = EManagerType.TenantAdmin,
                Username = input.Username,
                Password = input.Password,
                Nickname = input.Nickname,
                TenantId = model.Id
            });

            await _tenantRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var tenant = await _tenantRepository.GetAsync(item);

                if (tenant.IsSystem)
                {
                    throw new UserFriendlyException("禁止删除系统数据");
                }

                var tenantModules = await _tenantModuleRepository
                    .GetListAsync(m => m.TenantId == tenant.Id);

                var users = await _userRepository
                    .GetListAsync(m => m.TenantId == tenant.Id);

                foreach (var tenantModule in tenantModules)
                {
                    await _tenantModuleRepository.DeleteAsync(tenantModule);
                }

                foreach (var user in users)
                {
                    await _userRepository.DeleteAsync(user);
                }

                await _tenantRepository.DeleteAsync(tenant);
            }
        }

        public async Task UpdateAsync(TenantUpdateInputDto input)
        {
            var model = await _tenantRepository.GetAsync(input.Id);

            if (await _tenantRepository.AnyAsync(m =>
                m.Key == input.Key &&
                m.Id != model.Id))
            {
                throw new UserFriendlyException("编码已存在");
            }

            if (model.IsSystem)
            {
                throw new UserFriendlyException("禁止修改系统数据");
            }

            if (input.ModuleIds != null)
            {
                var tenantModules = await _tenantModuleRepository
                    .GetListAsync(m => m.TenantId == model.Id);

                foreach (var item in tenantModules)
                {
                    await _tenantModuleRepository.DeleteAsync(item);
                }

                foreach (var item in input.ModuleIds)
                {
                    var tenantModule = new TenantModule()
                    {
                        ModuleId = item,
                        TenantId = model.Id
                    };

                    tenantModule.CreateKey();

                    await _tenantModuleRepository.InsertAsync(tenantModule);
                }
            }

            await _userAppService.UpdateAsync(new UserUpdateInputDto()
            {
                Id = input.UserId,
                Username = input.Username,
                Nickname = input.Nickname
            });

            _objectMapper.Map(input, model);
        }

        public async Task<TenantOutputDto> GetDetailAsync(GetDetailInputDto input)
        {
            var model = await _tenantRepository.GetAsync(input.Id);

            return _objectMapper.Map<Tenant, TenantOutputDto>(model);
        }

        public async Task<List<TenantOutputDto>> GetAllAsync(TenantGetAllInputDto input)
        {
            var queryable = await _tenantRepository.GetQueryableAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Tenant>, List<TenantOutputDto>>(list);

            return data;
        }

        public async Task<PagedOutputDto<TenantOutputDto>> GetPageAsync(TenantGetPageInputDto input)
        {
            var queryable = await _tenantRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Tenant>, List<TenantOutputDto>>(list);

            var tenantIds = data.Select(m => m.Id).ToList();

            var tenantModules = await _tenantModuleRepository
                .GetListAsync(m => tenantIds.Contains(m.TenantId));

            var userQueryable = await _userRepository.GetQueryableAsync();

            var users = await userQueryable
                .IgnoreQueryFilters()
                .Where(m =>
                (m.ManagerType == EManagerType.TenantAdmin ||
                m.ManagerType == EManagerType.SuperAdmin) &&
                tenantIds.Contains(m.TenantId))
                .ToListAsync();

            foreach (var item in data)
            {
                var moduleIds = tenantModules
                                .Where(m => m.TenantId == item.Id)
                                .Select(m => m.ModuleId)
                                .ToList();

                item.ModuleIds = moduleIds;

                var user = users.SingleOrDefault(m => m.TenantId == item.Id);

                if (user != null)
                {
                    item.UserId = user.Id;
                    item.Username = user.Username;
                    item.Nickname = user.Nickname;
                }
            }

            return new PagedOutputDto<TenantOutputDto>(count, data);
        }

        public async Task<TenantOutputDto> GetDetailCacheAsync(GetDetailInputDto input)
        {
            var tenant = RedisHelper.StringGet<TenantOutputDto>(RedisKeyManger.GetTenantKey(input.Id.ToString()));

            return tenant;
        }
    }
}
