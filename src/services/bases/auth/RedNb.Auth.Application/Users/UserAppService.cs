using RedNb.Auth.Application.Contracts.Users;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using RedNb.Core.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Extensions;
using RedNb.Core.Application;
using RedNb.Auth.Domain.Shared;
using Microsoft.Extensions.Configuration;

namespace RedNb.Auth.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<UserClaim, long> _userClaimRepository;
        private readonly IRepository<EmployeeDepartment, long> _employeeDepartmentRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IConfiguration _appConfiguration;

        public LoginUser LoginUser { get; set; }

        public UserAppService(
            IRepository<User, long> userRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<UserClaim, long> userClaimRepository,
            IRepository<EmployeeDepartment, long> employeeDepartmentRepository,
            IObjectMapper objectMapper,
            IConfiguration appConfiguration)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userClaimRepository = userClaimRepository;
            _employeeDepartmentRepository = employeeDepartmentRepository;
            _objectMapper = objectMapper;
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// 获取指定租户user数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetUserCount(GetDetailInputDto input)
        {
            return await _userRepository.CountAsync(m => m.TenantId == input.Id);
        }

        public async Task<List<UserOutputDto>> GetAllByRoleKeyAsync(UserGetAllByRoleKeyInputDto input)
        {
            var queryable = await _userRoleRepository
                .WithDetailsAsync(m => m.User, m => m.Role);

            queryable = queryable
                .Where(m => m.Role.Key == input.Key);

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .Select(m => m.User)
                .ToListAsync();

            var data = _objectMapper.Map<List<User>, List<UserOutputDto>>(list);

            return data;
        }

        /// <summary>
        /// 根据角色用户用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutputDto<UserRoleOutputDto>> GetPageByRoleAsync(UserGetPageByRoleInputDto input)
        {
            var queryable = await _userRoleRepository
                .WithDetailsAsync(m => m.User, m => m.Role);

            queryable = queryable.Where(m => m.RoleId == input.RoleId);

            var count = await _userRoleRepository.AsyncExecuter
                .CountAsync(queryable);

            queryable = queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input);

            var list = await _userRoleRepository.AsyncExecuter
                .ToListAsync(queryable);

            var data = _objectMapper.Map<List<UserRole>, List<UserRoleOutputDto>>(list);

            return new PagedOutputDto<UserRoleOutputDto>(count, data);
        }

        /// <summary>
        /// 获取没有角色的用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutputDto<UserOutputDto>> GetPageNotByRoleAsync(UserGetPageByRoleInputDto input)
        {
            var user = await _userRepository.GetAsync(LoginUser.UserId);

            var userRoleQueryable = await _userRoleRepository.GetQueryableAsync();

            var userRoleQueryableUserId = userRoleQueryable
                 .Where(m => m.RoleId == input.RoleId)
                 .Select(m => m.UserId);

            var userIds = await _userRoleRepository.AsyncExecuter
                .ToListAsync(userRoleQueryableUserId);

            var queryable = await _userRepository.GetQueryableAsync();

            if (user.ManagerType == EManagerType.SuperAdmin)
            {
                queryable = queryable.Where(m => m.ManagerType == EManagerType.SuperAdmin);
            }
            else
            {
                queryable = queryable.Where(m => m.ManagerType == EManagerType.TenantUser);
            }

            queryable = queryable.Where(m => !userIds.Contains(m.Id));

            //if (user.ManagerType == EManagerType.SuperAdmin)
            //{
            //    queryable = queryable.Where(m =>
            //    (m.ManagerType == EManagerType.SuperAdmin ||
            //    m.ManagerType == EManagerType.PlatformUser));
            //}

            if (user.ManagerType == EManagerType.TenantAdmin)
            {
                queryable = queryable.Where(m =>
                (m.ManagerType == EManagerType.TenantAdmin ||
                m.ManagerType == EManagerType.TenantUser));
            }

            if (user.ManagerType == EManagerType.TenantUser)
            {
                queryable = queryable.Where(m =>
                m.ManagerType == EManagerType.TenantUser);
            }

            var count = await _userRepository.AsyncExecuter
                .CountAsync(queryable);

            queryable = queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input);

            var list = await _userRepository.AsyncExecuter
                .ToListAsync(queryable);

            var data = _objectMapper.Map<List<User>, List<UserOutputDto>>(list);

            return new PagedOutputDto<UserOutputDto>(count, data);
        }

        /// <summary>
        /// 用户添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddRoleBatchAsync(UserAddRoleBatchInputDto input)
        {
            if (input.Roles != null)
            {
                foreach (var item in input.Roles)
                {
                    var model = _objectMapper.Map<UserRoleAddInputDto, UserRole>(item);

                    model.CreateKey();

                    await _userRoleRepository.InsertAsync(model);
                }
            }
        }

        /// <summary>
        /// 批量删除用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteRoleBatchAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                var list = await _userRoleRepository
                    .GetListAsync(m => input.Ids.Contains(m.Id));

                await _userRoleRepository.DeleteManyAsync(list);
            }
        }

        public async Task<UserOutputDto> AddAsync(UserAddInputDto input)
        {
            if (input.ManagerType == EManagerType.SuperAdmin)
            {
                throw new UserFriendlyException("禁止添加超级管理员");
            }

            var model = _objectMapper.Map<UserAddInputDto, User>(input);

            model.CreateKey();

            if (!String.IsNullOrWhiteSpace(model.Username))
            {
                if (await _userRepository.AnyAsync(m => m.Username == model.Username))
                {
                    throw new UserFriendlyException("用户名已存在");
                }
            }
            else
            {
                var str = model.Id.ToString("X2").ToLower();
                var arr = str.ToCharArray().Reverse();
                model.Username = $"zc_{string.Join("", arr)}";
            }

            if (String.IsNullOrWhiteSpace(model.Avatar))
            {
                model.Avatar = AuthConst.DefaultAvatar;
            }

            var password = String.IsNullOrWhiteSpace(model.Password) ? "111111" : model.Password;

            model.Password = CommonHelper.Encrypt(password);

            model.IsActive = true;

            await _userRepository.InsertAsync(model);

            return _objectMapper.Map<User, UserOutputDto>(model);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task UpdateAsync(UserUpdateInputDto input)
        {
            if (await _userRepository.AnyAsync(m =>
                 m.Username == input.Username &&
                 m.Id != input.Id))
            {
                throw new UserFriendlyException("用户名已存在");
            }

            var model = await _userRepository
                    .SingleOrDefaultAsync(m => m.Id == input.Id);

            _objectMapper.Map(input, model);
        }

        public async Task UpdateByAsync(UserUpdateInputDto input)
        {
            if (await _userRepository.AnyAsync(m =>
                    m.Username == input.Username &&
                    m.ReferenceId != input.Id))
            {
                throw new UserFriendlyException("用户名已存在");
            }

            var model = await _userRepository
                    .SingleOrDefaultAsync(m => m.ReferenceId == input.Id);

            model.Username = input.Username;
            model.Mobile = input.Username;
            model.Nickname = input.Nickname;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdatePasswordAsync(UserUpdatePasswordInputDto input)
        {
            if (input.Id > 0)
            {
                var user = await _userRepository
                    .SingleOrDefaultAsync(m => m.ReferenceId == input.Id);

                user.Password = CommonHelper.Encrypt(input.Password);
            }
            else
            {
                var user = await _userRepository.GetAsync(input.UserId);

                user.Password = CommonHelper.Encrypt(input.Password);
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateRealNameAsync(UserUpdateRealNameInputDto input)
        {
            var user = await _userRepository.GetAsync(input.UserId);

            user.Password = CommonHelper.Encrypt(input.Password);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateMobileAsync(UserUpdateMobileInputDto input)
        {
            var user = await _userRepository.GetAsync(input.UserId);

            user.Mobile = input.Mobile;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                var list = await _userRepository
                    .GetListAsync(m => input.Ids.Contains(m.Id));

                if (list.Any(m => m.ManagerType == EManagerType.SuperAdmin))
                {
                    throw new UserFriendlyException("平台管理员禁止删除");
                }

                await _userRepository.DeleteManyAsync(list);
            }
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task DeleteBatchByAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                var list = await _userRepository
                    .GetListAsync(m => m.ReferenceId.HasValue &&
                    input.Ids.Contains((long)m.ReferenceId));

                if (list.Any(m => m.ManagerType == EManagerType.SuperAdmin))
                {
                    throw new UserFriendlyException("平台管理员禁止删除");
                }

                await _userRepository.DeleteManyAsync(list);
            }
        }

        public async Task<UserOutputDto> GetDetailAsync(GetDetailInputDto input)
        {
            var model = await _userRepository.GetAsync(input.Id);

            var data = _objectMapper.Map<User, UserOutputDto>(model);

            return data;
        }

        public async Task<UserOutputDto> GetDetailByAsync(GetDetailInputDto input)
        {
            var queryable = await _userRepository.GetQueryableAsync();

            queryable = queryable.Where(m => m.ReferenceId == input.Id);

            var model = await _userRepository.AsyncExecuter
                .SingleOrDefaultAsync(queryable);

            var data = _objectMapper.Map<User, UserOutputDto>(model);

            return data;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<PagedOutputDto<UserOutputDto>> GetPageAsync(UserGetPageInputDto input)
        {
            var queryable = await _userRepository.GetQueryableAsync();

            queryable = queryable.Where(m => m.ManagerType != EManagerType.SuperAdmin && m.ManagerType == input.ManagerType && m.TenantId == input.TenantId);

            if (!String.IsNullOrWhiteSpace(input.Username))
            {
                queryable = queryable.Where(m => m.Username.Contains(input.Username));
            }

            if (!String.IsNullOrWhiteSpace(input.Nickname))
            {
                queryable = queryable.Where(m => m.Nickname.Contains(input.Nickname));
            }

            if (!String.IsNullOrWhiteSpace(input.Type))
            {
                queryable = queryable.Where(m => m.Type.Contains(input.Type));
            }

            if (!String.IsNullOrWhiteSpace(input.ReferenceName))
            {
                queryable = queryable.Where(m => m.ReferenceName.Contains(input.ReferenceName));
            }

            if (input.DepartmentId != null)
            {
                var queryEmployeeDeaprtment = await _employeeDepartmentRepository.GetQueryableAsync();

                var queryEmployeeDeaprtmentUserId = queryEmployeeDeaprtment
                      .Where(m => m.DepartmentId == input.DepartmentId)
                      .Select(m => m.Employee.UserId);

                var tUserIds = await _employeeDepartmentRepository.AsyncExecuter
                    .ToListAsync(queryEmployeeDeaprtmentUserId);

                queryable = queryable.Where(m => tUserIds.Contains(m.Id));
            }

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<User>, List<UserOutputDto>>(list);

            return new PagedOutputDto<UserOutputDto>(count, data);
        }

        public async Task<List<UserOutputDto>> GetListByAsync(UserGetListInputDto input)
        {
            var queryable = await _userRepository.GetQueryableAsync();

            queryable = queryable.Where(m => m.ManagerType != EManagerType.SuperAdmin);

            if (input.UserIds != null)
            {
                queryable = queryable.Where(m => input.UserIds.Contains(m.Id));
            }

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<User>, List<UserOutputDto>>(list);

            return data;
        }

        public async Task<List<UserOutputDto>> GetListByReferenceAsync(BatchInputDto input)
        {
            var queryable = await _userRepository.GetQueryableAsync();

            queryable = queryable.Where(m => m.ManagerType != EManagerType.SuperAdmin);

            if (input.Ids != null)
            {
                queryable = queryable.Where(m => m.ReferenceId.HasValue && input.Ids.Contains((long)m.ReferenceId));
            }

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<User>, List<UserOutputDto>>(list);

            return data;
        }
    }
}
