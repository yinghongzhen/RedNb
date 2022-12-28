using RedNb.Auth.Application.Contracts;
using RedNb.Auth.Application.Contracts.Permissions;
using RedNb.Auth.Application.Contracts.Permissions.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Shared.Enums;
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
using RedNb.Core.Util;
using RedNb.Core.Domain;
using RedNb.Core.Application;
using RedNb.Auth.Domain.Products;

namespace RedNb.Auth.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IRepository<Platform, long> _platformRepository;
        private readonly IRepository<Permission, long> _permissionRepository;
        private readonly IRepository<RolePermission, long> _rolePermissionRepository;
        private readonly IRepository<TenantPermission, long> _tenantPermissionRepository;
        private readonly IRepository<TenantModule, long> _tenantModuleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _objectMapper;

        public LoginUser LoginUser { get; set; }

        public PermissionAppService(IRepository<Platform, long> platformRepository,
            IRepository<Permission, long> permissionRepository,
            IRepository<RolePermission, long> rolePermissionRepository,
            IRepository<TenantPermission, long> tenantPermissionRepository,
            IRepository<TenantModule, long> tenantModuleRepository,
            IRepository<User, long> userRepository,
            IObjectMapper objectMapper)
        {
            _platformRepository = platformRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _tenantPermissionRepository = tenantPermissionRepository;
            _tenantModuleRepository = tenantModuleRepository;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(PermissionAddInputDto input)
        {
            var model = _objectMapper.Map<PermissionAddInputDto, Permission>(input);

            model.CreateKey();

            if (input.ParentId != 0)
            {
                var parent = await _permissionRepository.GetAsync(input.ParentId);

                model.UpdateTreeValue(parent, null, () =>
                {
                    model.TreeKeys = $"{parent.TreeKeys}_{model.Key}";
                });
            }
            else
            {
                model.UpdateTreeValue(null, null, () =>
                {
                    model.TreeKeys = model.Key;
                });
            }

            if (await _permissionRepository.AnyAsync(m => m.Key == input.Key &&
                        m.TreeLevel == model.TreeLevel && m.ParentId == model.ParentId))
            {
                throw new UserFriendlyException("编码已存在，添加失败");
            }

            await _permissionRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var model = await _permissionRepository.GetAsync(item);

                if (await _permissionRepository.AnyAsync(m => m.ParentId == model.Id))
                {
                    throw new UserFriendlyException("节点下有子节点，不能删除");
                }

                await _permissionRepository.DeleteAsync(model);
            }
        }

        public async Task UpdateAsync(PermissionUpdateInputDto input)
        {
            var model = await _permissionRepository.GetAsync(input.Id);

            var queryable = await _permissionRepository.GetQueryableAsync();

            _objectMapper.Map(input, model);

            var parentId = $"{model.Id},";

            var children = await queryable
                .Where(m => m.ParentIds.Contains(parentId))
                .OrderBy(m => m.TreeLevel)
                .ThenBy(m => m.Id)
                .Select(m => (TreeEntity)m)
                .ToListAsync();

            if (model.ParentId != 0)
            {
                var parent = await _permissionRepository.GetAsync(model.ParentId);

                model.UpdateTreeValue(parent, children, () =>
                {
                    model.TreeKeys = $"{parent.TreeKeys}_{model.Key}";
                });
            }
            else
            {
                model.UpdateTreeValue(null, children, () =>
                {
                    model.TreeKeys = model.Key;
                });
            }

            if (await _permissionRepository.AnyAsync(m => m.Key == input.Key &&
                m.Id != model.Id && m.TreeLevel == model.TreeLevel && m.ParentId == model.ParentId))
            {
                throw new UserFriendlyException("编码已存在，添加失败");
            }
        }

        public async Task<List<AntdTreeOutputDto>> GetTreeAsync(PermissionGetTreeInputDto input)
        {
            var queryable = await _permissionRepository.GetQueryableAsync();

            if (input.ExcludeIds != null)
            {
                queryable = queryable.Where(m => !input.ExcludeIds.Contains(m.Id));
            }

            var list = await queryable
                .OrderByDescending(m => m.TreeSort)
                .ThenBy(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(list);

            var topData = data.Where(m => m.TreeLevel == 0).ToList();
            var leafData = data.Where(m => m.TreeLevel != 0).ToList();

            foreach (var item in topData)
            {
                CommonHelper.ReGetTree(item, leafData);
            }

            return _objectMapper.Map<List<PermissionOutputDto>, List<AntdTreeOutputDto>>(topData);
        }

        public async Task<PagedOutputDto<PermissionOutputDto>> GetPageAsync(PermissionGetPageInputDto input)
        {
            var queryable = await _permissionRepository.GetQueryableAsync();

            var topQuery = queryable.Where(m => m.TreeLevel == 0);

            if (!String.IsNullOrWhiteSpace(input.TreeName))
            {
                topQuery = topQuery.Where(m => m.TreeName.Contains(input.TreeName));
            }

            var count = await topQuery.CountAsync();

            var topList = await topQuery
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .PageBy(input)
                .ToListAsync();

            var leafList = await queryable
                .Where(m => m.TreeLevel > 0)
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .ToListAsync();

            var topData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(topList);
            var leafData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(leafList);

            foreach (var item in topData)
            {
                CommonHelper.ReGetTree(item, leafData);
            }

            return new PagedOutputDto<PermissionOutputDto>(count, topData);
        }

        public async Task<PermissionGroupOutputDto> GetTreeByRoleAsync(PermissionGetTreeByRoleInputDto input)
        {
            var user = await _userRepository.GetAsync(LoginUser.UserId);

            var data = new PermissionGroupOutputDto();

            var queryable = await _rolePermissionRepository.GetQueryableAsync();

            var selectedPermissionIds = await queryable
                .Where(m => m.RoleId == input.RoleId)
                .Select(m => m.PermissionId)
                .Distinct()
                .ToListAsync();

            data.SelectedPermissionIds = selectedPermissionIds;

            if (user.ManagerType == EManagerType.SuperAdmin)
            {
                var tenantPermissionQueryable = await _tenantPermissionRepository.GetQueryableAsync();

                var list = await tenantPermissionQueryable
                   .Include(m => m.Permission)
                   .OrderBy(m => m.Permission.TreeLevel)
                   .ThenByDescending(m => m.Permission.TreeSort)
                   .ThenBy(m => m.Permission.Id)
                   .Select(m => m.Permission)
                   .Distinct()
                   .ToListAsync();

                var topList = list
                .Where(m => m.TreeLevel == 0)
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .ThenBy(m => m.Id)
                .ToList();

                var leafList = list
                    .Where(m => m.TreeLevel != 0)
                    .OrderBy(m => m.TreeLevel)
                    .ThenByDescending(m => m.TreeSort)
                    .ThenBy(m => m.Id)
                    .ToList();

                var menuData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(topList);
                var leafData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(leafList);

                foreach (var item in menuData)
                {
                    CommonHelper.ReGetTree(item, leafData);
                }

                data.PermissionList = new PagedOutputDto<PermissionOutputDto>(menuData.Count, menuData);
            }

            if (user.ManagerType == EManagerType.TenantAdmin ||
                user.ManagerType == EManagerType.TenantUser)
            {
                var tenantPermissionQueryable = await _tenantPermissionRepository.GetQueryableAsync();

                var list = await tenantPermissionQueryable
                    .Include(m => m.Permission)
                    .OrderByDescending(m => m.Permission.TreeSort)
                    .ThenBy(m => m.Permission.Id)
                    .Select(m => m.Permission)
                    .ToListAsync();

                var topList = list
                    .Where(m => m.TreeLevel == 0)
                    .OrderByDescending(m => m.TreeSort)
                    .ThenBy(m => m.Id)
                    .ToList();

                var leafList = list
                    .Where(m => m.TreeLevel != 0)
                    .OrderByDescending(m => m.TreeSort)
                    .ThenBy(m => m.Id)
                    .ToList();

                var menuData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(topList);
                var leafData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(leafList);

                foreach (var item in menuData)
                {
                    CommonHelper.ReGetTree(item, leafData);
                }

                data.PermissionList = new PagedOutputDto<PermissionOutputDto>(menuData.Count, menuData);
            }

            return data;
        }

        public async Task<PermissionGroupOutputDto> GetTreeByTenantAsync(PermissionGetTreeByTenantInputDto input)
        {
            var data = new PermissionGroupOutputDto();

            var queryable = await _tenantModuleRepository.GetQueryableAsync();

            var tenantModules = await queryable
                        .IgnoreQueryFilters()
                        .Where(m => m.TenantId == input.TenantId)
                        .ToListAsync();

            var moduleIds = tenantModules.Select(m => m.ModuleId).ToList();

            var tenantPermissionQueryable = await _tenantPermissionRepository.GetQueryableAsync();

            var selectedPermissionIds = await tenantPermissionQueryable
                .IgnoreQueryFilters()
                .Where(m => m.TenantId == input.TenantId)
                .Select(m => m.PermissionId)
                .ToListAsync();

            data.SelectedPermissionIds = selectedPermissionIds;

            var permissionQueryable = await _permissionRepository.GetQueryableAsync();

            permissionQueryable = permissionQueryable
                .IgnoreQueryFilters()
                .Where(m => moduleIds.Contains(m.ModuleId));

            var topList = await permissionQueryable
                .Where(m => m.TreeLevel == 0)
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .ThenBy(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var leafList = await permissionQueryable
                .Where(m => m.TreeLevel > 0)
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .ThenBy(m => m.Id)
                .ToListAsync();

            var permissionData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(topList);
            var leafData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(leafList);

            foreach (var item in permissionData)
            {
                CommonHelper.ReGetTree(item, leafData);
            }

            data.PermissionList = new PagedOutputDto<PermissionOutputDto>(permissionData.Count, permissionData);

            return data;
        }

        public async Task AllotToRoleAsync(PermissionAllotToRoleInputDto input)
        {
            var queryable = await _rolePermissionRepository.GetQueryableAsync();

            var rolePermissions = await queryable
                .Where(m => m.RoleId == input.RoleId)
                .ToListAsync();

            foreach (var item in rolePermissions)
            {
                await _rolePermissionRepository.DeleteAsync(item);
            }

            foreach (var item in input.PermissionIds)
            {
                var model = new RolePermission()
                {
                    PermissionId = item,
                    RoleId = input.RoleId
                };

                model.CreateKey();

                await _rolePermissionRepository.InsertAsync(model);
            }
        }

        public async Task AllotToTenantAsync(PermissionAllotToTenantInputDto input)
        {
            var queryable = await _tenantPermissionRepository.GetQueryableAsync();

            var tenantPermissions = await queryable
                .IgnoreQueryFilters()
                .Where(m => m.TenantId == input.TenantId)
                .ToListAsync();

            foreach (var item in tenantPermissions)
            {
                await _tenantPermissionRepository.DeleteAsync(item);
            }

            foreach (var item in input.PermissionIds)
            {
                var model = new TenantPermission()
                {
                    PermissionId = item,
                    TenantId = input.TenantId
                };

                model.CreateKey();

                await _tenantPermissionRepository.InsertAsync(model);
            }
        }
    }
}
