using RedNb.Auth.Application.Contracts.Roles;
using RedNb.Auth.Application.Contracts.Roles.Dtos;
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
using RedNb.Core.Domain;

namespace RedNb.Auth.Application.Roles
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRepository<Role, long> _roleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IObjectMapper _objectMapper;

        public RoleAppService(IRepository<Role, long> roleRepository,
            IRepository<User, long> userRepository,
            IRepository<UserRole, long> userRoleRepository,
            IObjectMapper objectMapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(RoleAddInputDto input)
        {
            if (await _roleRepository.AnyAsync(m =>
                m.Key == input.Key))
            {
                throw new UserFriendlyException("编码已存在，添加失败");
            }

            var model = _objectMapper.Map<RoleAddInputDto, Role>(input);

            model.CreateKey();

            model.IsActive = true;

            await _roleRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                var queryable = await _roleRepository.GetQueryableAsync();

                var list = await queryable
                    .Where(m => input.Ids.Contains(m.Id))
                    .ToListAsync();

                foreach (var item in list)
                {
                    await _roleRepository.DeleteAsync(item);
                }
            }
        }

        public async Task UpdateAsync(RoleUpdateInputDto input)
        {
            var model = await _roleRepository.GetAsync(input.Id);

            if (await _roleRepository.AnyAsync(m => m.Key == input.Key && m.Id != model.Id))
            {
                throw new UserFriendlyException("编码已存在，添加失败");
            }

            _objectMapper.Map(input, model);
        }

        public async Task<List<RoleOutputDto>> GetAllAsync(RoleGetAllInputDto input)
        {
            var queryable = await _roleRepository.GetQueryableAsync();

            var list = await queryable
                .OrderByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Role>, List<RoleOutputDto>>(list);

            return data;
        }

        public async Task<List<RoleOutputDto>> GetAllByAsync(RoleGetAllByInputDto input)
        {
            var queryable = await _userRoleRepository.WithDetailsAsync(m => m.Role);

            queryable = queryable.Where(m => m.UserId == input.UserId);

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .Select(m => m.Role)
                .ToListAsync();

            var data = _objectMapper.Map<List<Role>, List<RoleOutputDto>>(list);

            return data;
        }

        public async Task<PagedOutputDto<RoleOutputDto>> GetPageAsync(RoleGetPageInputDto input)
        {
            var queryable = await _roleRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Sort)
                .ThenByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Role>, List<RoleOutputDto>>(list);

            return new PagedOutputDto<RoleOutputDto>(count, data);
        }

        public async Task AddUserBatchAsync(RoleAddUserBatchInputDto input)
        {
            if (input.UserIds != null)
            {
                foreach (var item in input.UserIds)
                {
                    var model = new UserRole()
                    {
                        RoleId = input.RoleId,
                        UserId = item
                    };

                    model.CreateKey();

                    await _userRoleRepository.InsertAsync(model);
                }
            }
        }
    }
}
