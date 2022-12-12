using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using RedNb.Core.Extensions;
using RedNb.Core.Util;
using Microsoft.AspNetCore.Http;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RedNb.Auth.Domain.Offices;
using RedNb.Auth.Application.Contracts.Employees;
using RedNb.Auth.Application.Contracts.Employees.Dtos;
using RedNb.Auth.Application.Contracts.Users;
using RedNb.Auth.Application.Contracts.Posts.Dtos;
using RedNb.Auth.Application.Contracts.Departments.Dtos;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RedNb.Core.Application;

namespace RedNb.Auth.Application.Employees
{
    public class EmployeeAppService : IEmployeeAppService
    {
        private readonly IUserAppService _userAppService;
        private readonly IRepository<Employee, long> _employeeRepository;
        private readonly IRepository<EmployeeDepartment, long> _employeeDepartmentRepository;
        private readonly IRepository<EmployeePost, long> _employeePostRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Department, long> _departmentRepository;

        public EmployeeAppService(
            IUserAppService userAppService,
            IRepository<Employee, long> employeeRepository,
            IRepository<EmployeeDepartment, long> employeeDepartmentRepository,
            IRepository<EmployeePost, long> employeePostRepository,
            IObjectMapper objectMapper,
            IRepository<Department, long> departmentRepository)
        {
            _userAppService = userAppService;
            _employeeRepository = employeeRepository;
            _employeeDepartmentRepository = employeeDepartmentRepository;
            _employeePostRepository = employeePostRepository;
            _objectMapper = objectMapper;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task AddAsync(EmployeeAddInputDto input)
        {
            var model = _objectMapper.Map<EmployeeAddInputDto, Employee>(input);

            model.CreateKey();

            var user = await _userAppService.AddAsync(new UserAddInputDto
            {
                ManagerType = EManagerType.TenantUser,
                Username = input.Username,
                Password = input.Password,
                Nickname = input.Nickname,
                Avatar = "default.jpg",
                Type = "employee",
                ReferenceId = model.Id,
                ReferenceName = input.Name
            });

            model.UserId = user.Id;

            await _employeeRepository.InsertAsync(model);

            if (input.PostIds != null)
            {
                foreach (var item in input.PostIds)
                {
                    var employeePost = new EmployeePost()
                    {
                        EmployeeId = model.Id,
                        PostId = item
                    };

                    employeePost.CreateKey();

                    await _employeePostRepository.InsertAsync(employeePost);
                }
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                var list = await _employeeRepository
                    .GetListAsync(m => input.Ids.Contains(m.Id));

                var ids = list.Select(m => m.Id).ToList();
                var userIds = list.Select(m => m.UserId).ToList();

                await _userAppService.DeleteBatchAsync(new DeleteBatchInputDto
                {
                    Ids = userIds
                });

                var employeeDeparments = await _employeeDepartmentRepository
                    .GetListAsync(m => ids.Contains(m.EmployeeId));

                await _employeeDepartmentRepository.DeleteManyAsync(employeeDeparments);

                var employeePosts = await _employeePostRepository
                    .GetListAsync(m => ids.Contains(m.EmployeeId));

                await _employeePostRepository.DeleteManyAsync(employeePosts);

                await _employeeRepository.DeleteManyAsync(list);
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(EmployeeUpdateInputDto input)
        {
            var model = await _employeeRepository.GetAsync(input.Id);

            await _userAppService.UpdateAsync(new UserUpdateInputDto
            {
                Id = model.UserId,
                Username = input.Username,
                Nickname = input.Nickname
            });

            _objectMapper.Map(input, model);

            var employeePosts = await _employeePostRepository
                .GetListAsync(m => m.EmployeeId == model.Id);

            await _employeePostRepository.DeleteManyAsync(employeePosts);

            if (input.PostIds != null)
            {
                foreach (var item in input.PostIds)
                {
                    var employeePost = new EmployeePost()
                    {
                        EmployeeId = model.Id,
                        PostId = item
                    };

                    employeePost.CreateKey();

                    await _employeePostRepository.InsertAsync(employeePost);
                }
            }
        }

        public async Task<EmployeeOutputDto> GetDetailAsync(GetDetailInputDto input)
        {
            var model = await _employeeRepository.GetAsync(input.Id);

            var data = _objectMapper.Map<Employee, EmployeeOutputDto>(model);

            return data;
        }

        public async Task<EmployeeOutputDto> GetDetailByUserAsync(GetDetailInputDto input)
        {
            var model = await _employeeRepository.SingleOrDefaultAsync(m => m.UserId == input.Id);

            var data = _objectMapper.Map<Employee, EmployeeOutputDto>(model);

            return data;
        }

        /// <summary>
        /// 获取当前租户下所有employee
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeOutputDto>> GetAllAsync()
        {
            var queryable = await _employeeRepository
                .WithDetailsAsync(m => m.User, m => m.Tenant, m => m.Department);

            queryable = queryable
                .Where(m => m.User.ManagerType == EManagerType.TenantUser)
                .OrderByDescending(m => m.Id);

            var list = await _employeeRepository.AsyncExecuter
                .ToListAsync(queryable);

            var data = _objectMapper.Map<List<Employee>, List<EmployeeOutputDto>>(list);

            return data;
        }

        /// <summary>
        /// 分页获取employee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutputDto<EmployeeOutputDto>> GetPageAsync(EmployeeGetPageInputDto input)
        {
            var queryable = await _employeeRepository.WithDetailsAsync(m => m.User, m => m.Department);

            if (!String.IsNullOrWhiteSpace(input.Username))
            {
                queryable = queryable.Where(m => m.User.Username.Contains(input.Username));
            }

            if (!String.IsNullOrWhiteSpace(input.Nickname))
            {
                queryable = queryable.Where(m => m.User.Nickname.Contains(input.Nickname));
            }

            if (!String.IsNullOrWhiteSpace(input.Name))
            {
                queryable = queryable.Where(m => m.Name.Contains(input.Name));
            }

            if (input.DepartmentId != null)
            {
                var department = await _departmentRepository.GetAsync((long)input.DepartmentId);

                var emdQueryable = await _employeeDepartmentRepository.WithDetailsAsync(m => m.Department);

                var parentId = $"{department.Id},";

                var employeeIds = await emdQueryable
                    .Where(m => m.Department.ParentIds.Contains(parentId) || m.DepartmentId == department.Id)
                    .OrderBy(m => m.Id)
                    .Select(m => m.EmployeeId)
                    .ToListAsync();

                queryable = queryable.Where(m => employeeIds.Contains(m.Id));
            }

            var count = await _employeeRepository.AsyncExecuter
                .CountAsync(queryable);

            queryable = queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input);

            var list = await _employeeRepository.AsyncExecuter
                .ToListAsync(queryable);

            var data = _objectMapper.Map<List<Employee>, List<EmployeeOutputDto>>(list);

            var ids = data.Select(m => m.Id).ToList();

            var epQueryable = await _employeePostRepository.WithDetailsAsync(m => m.Post);

            epQueryable = epQueryable
                .Where(m => ids.Contains(m.EmployeeId));

            var epList = await _employeePostRepository.AsyncExecuter
              .ToListAsync(epQueryable);

            foreach (var item in data)
            {
                var tPosts = epList
                    .Where(m => m.EmployeeId == item.Id)
                    .Select(m => m.Post)
                    .ToList();

                item.Username = item.User.Username;
                item.Nickname = item.User.Nickname;
                item.PostList = _objectMapper.Map<List<Post>, List<PostOutputDto>>(tPosts);
            }

            return new PagedOutputDto<EmployeeOutputDto>(count, data);
        }
    }
}
