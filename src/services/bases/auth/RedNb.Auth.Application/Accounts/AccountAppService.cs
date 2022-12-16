using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Extensions;
using RedNb.Core.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RedNb.Auth.Application.Contracts.Accounts.Dtos;
using RedNb.Auth.Application.Contracts.Permissions.Dtos;
using RedNb.Auth.Application.Contracts.Roles.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Application.Contracts.Accounts;
using RedNb.Core.Contracts;
using RedNb.Core.Application;
using RedNb.Auth.Domain.Shared;
using RedNb.Auth.Application.Contracts.Users;
using RestSharp;
using Volo.Abp.Uow;
using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Auth.Domain.Menus;
using RedNb.Auth.Domain.Accounts;

namespace RedNb.Auth.Application.Accounts
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IRepository<Client, long> _clientRepository;
        private readonly IRepository<TenantModule, long> _tenantModuleRepository;
        private readonly IRepository<ModuleCallback, long> _moduleCallbackRepository;
        private readonly IRepository<LoginAccount, long> _loginAccountRepository;
        private readonly IRepository<LoginAccountUser, long> _loginAccountUserRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Tenant, long> _tenantRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Permission, long> _permissionRepository;
        private readonly IRepository<RolePermission, long> _rolePermissionRepository;
        private readonly IRepository<TenantPermission, long> _tenantPermissionRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAppService _userAppService;
        private readonly UnitOfWorkManager _unitOfWorkManager;

        public LoginUser LoginUser { get; set; }

        public AccountAppService(
            IRepository<Client, long> clientRepository,
            IRepository<TenantModule, long> tenantModuleRepository,
            IRepository<ModuleCallback, long> moduleCallbackRepository,
            IRepository<LoginAccount, long> loginAccountRepository,
            IRepository<LoginAccountUser, long> loginAccountUserRepository,
            IRepository<User, long> userRepository,
            IRepository<Tenant, long> tenantRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Permission, long> permissionRepository,
            IRepository<RolePermission, long> rolePermissionRepository,
            IRepository<TenantPermission, long> tenantPermissionRepository,
            IObjectMapper objectMapper,
            IHttpContextAccessor httpContextAccessor,
            IUserAppService userAppService,
            UnitOfWorkManager unitOfWorkManager)
        {
            _clientRepository = clientRepository;
            _tenantModuleRepository = tenantModuleRepository;
            _moduleCallbackRepository = moduleCallbackRepository;
            _loginAccountRepository = loginAccountRepository;
            _loginAccountUserRepository = loginAccountUserRepository;
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _userRoleRepository = userRoleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _tenantPermissionRepository = tenantPermissionRepository;
            _objectMapper = objectMapper;
            _httpContextAccessor = httpContextAccessor;
            _userAppService = userAppService;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<LoginOutputDto> WxAppRegAsync(WxAppRegInputDto input)
        {
            var loginAccount = await _loginAccountRepository
                .SingleOrDefaultAsync(m => m.WxUnionId == input.WxUnionId &&
                m.WxAppOpenId == input.WxAppOpenId);

            if (loginAccount == null)
            {
                loginAccount = new LoginAccount();

                loginAccount.CreateKey();

                loginAccount.WxUnionId = input.WxUnionId;
                loginAccount.WxAppOpenId = input.WxAppOpenId;

                await _loginAccountRepository.InsertAsync(loginAccount);
            }

            var userData = await _userAppService.AddAsync(new UserAddInputDto
            {
                ManagerType = EManagerType.TenantUser,
                Nickname = input.Nickname,
                Avatar = input.Avatar,
                Type = input.Type,
                ReferenceId = input.ReferenceId,
                ReferenceName = input.ReferenceName,
                TenantId = input.TenantId
            });

            if (!await _loginAccountUserRepository
                .AnyAsync(m => m.LoginAccountId == loginAccount.Id &&
                            m.UserId == userData.Id))
            {
                var loginAccountUser = new LoginAccountUser();

                loginAccountUser.CreateKey();

                loginAccountUser.LoginAccountId = loginAccount.Id;
                loginAccountUser.UserId = userData.Id;

                await _loginAccountUserRepository.InsertAsync(loginAccountUser);
            }

            var tenant = await _tenantRepository.GetAsync(input.TenantId);

            return await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), userData);
        }

        public async Task<LoginOutputDto> WxAppLoginAsync(WxAppLoginInputDto input)
        {
            var loginAccountUserQueryable = await _loginAccountUserRepository.WithDetailsAsync(m => m.LoginAccount, m => m.User);

            var loginAccountUser = await loginAccountUserQueryable
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m => 
                //m.LoginAccount.WxUnionId == input.WxUnionId &&
                m.LoginAccount.WxAppOpenId == input.WxAppOpenId &&
                m.User.Type == input.Type &&
                m.User.TenantId == input.TenantId);

            var tenant = await _tenantRepository.GetAsync(input.TenantId);

            if (loginAccountUser != null)
            {
                var queryable = await _userRepository.GetQueryableAsync();

                var user = await queryable
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(m => m.Id == loginAccountUser.UserId);

                var userData = _objectMapper.Map<User, UserOutputDto>(user);

                return await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), userData);
            }

            return new LoginOutputDto();
        }

        public async Task<LoginOutputDto> ClientRegAsync(ClientRegInputDto input)
        {
            var clientQueryable = await _clientRepository.GetQueryableAsync();

            var client = await clientQueryable
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m =>
                m.Key == input.Key &&
                m.Secret == input.Secret);

            if (client == null)
            {
                throw new UserFriendlyException("客户端不存在");
            }

            if (!client.IsActive)
            {
                throw new UserFriendlyException("客户端被禁用");
            }

            var now = DateTime.Now;

            var tenant = await _tenantRepository
                .SingleOrDefaultAsync(m => !m.IsSystem && m.Id == client.TenantId);

            if (tenant == null)
            {
                throw new UserFriendlyException("租户不存在");
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException("租户被禁用");
            }

            if (tenant.ExpireDate != null && tenant.ExpireDate < now)
            {
                throw new UserFriendlyException("租户已过期");
            }

            var userData = await _userAppService.AddAsync(new UserAddInputDto
            {
                ManagerType = EManagerType.TenantUser,
                Nickname = input.Nickname,
                Avatar = input.Avatar,
                TenantId = client.TenantId
            });

            var _tenantModuleQueryable = await _tenantModuleRepository.WithDetailsAsync(m => m.Module);

            var modules = _tenantModuleQueryable
              .IgnoreQueryFilters()
              .Where(m => m.Module.Type == EModuleType.Product &&
                      m.TenantId == client.TenantId)
              .Select(m => m.Module)
              .ToList();

            foreach (var item in modules)
            {
                if (!String.IsNullOrWhiteSpace(item.RegCallback))
                {
                    var restClient = new RestClient();

                    var request = new RestRequest(item.RegCallback, Method.Post);

                    request.AddJsonBody(new
                    {
                        Nickname = userData.Nickname,
                        UserId = userData.Id,
                        TenantId = userData.TenantId
                    });

                    var response = await restClient.ExecuteAsync(request);

                    var callback = new ModuleCallback();

                    callback.CreateKey();
                    callback.Url = item.RegCallback;
                    callback.ResultCode = response.StatusCode;
                    callback.ResultValue = response.Content;
                    callback.ModuleId = item.Id;

                    await _moduleCallbackRepository.InsertAsync(callback);
                }
            }

            return await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), userData);
        }

        public async Task<LoginOutputDto> LoginAsync(LoginInputDto input)
        {
            var now = DateTime.Now;

            var queryable = await _userRepository.WithDetailsAsync(m => m.Tenant);

            var password = CommonHelper.Encrypt(input.Password);

            var user = await queryable
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m => m.Username == input.Username &&
                m.Password == password);

            if (user == null)
            {
                throw new UserFriendlyException("账户或密码错误");
            }

            if (!user.IsActive)
            {
                throw new UserFriendlyException("用户被禁用");
            }

            var tenant = user.Tenant;

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException("租户被禁用");
            }

            if (tenant.ExpireDate != null && tenant.ExpireDate < now)
            {
                throw new UserFriendlyException("租户已过期");
            }

            var userData = _objectMapper.Map<User, UserOutputDto>(user);

            return await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), userData);
        }

        public async Task<LoginOutputDto> ClientLoginAsync(ClientLoginInputDto input)
        {
            var clientQueryable = await _clientRepository.GetQueryableAsync();

            var client = await clientQueryable
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m =>
                m.Key == input.Key &&
                m.Secret == input.Secret);

            if (client == null)
            {
                throw new UserFriendlyException("客户端不存在");
            }

            if (!client.IsActive)
            {
                throw new UserFriendlyException("客户端被禁用");
            }

            var now = DateTime.Now;

            var queryable = await _userRepository.WithDetailsAsync(m => m.Tenant);

            var user = await queryable
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m => m.Id == input.UserId &&
                m.TenantId == client.TenantId);

            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            if (!user.IsActive)
            {
                throw new UserFriendlyException("用户被禁用");
            }

            var tenant = user.Tenant;

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException("租户被禁用");
            }

            if (tenant.ExpireDate != null && tenant.ExpireDate < now)
            {
                throw new UserFriendlyException("租户已过期");
            }

            var userData = _objectMapper.Map<User, UserOutputDto>(user);

            return await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), userData);
        }

        private async Task<LoginOutputDto> _LoginAsync(TenantOutputDto tenant, UserOutputDto user)
        {
            var flag = _httpContextAccessor.GetFlag();

            var loginUser = new LoginUserDto()
            {
                Flag = flag,
                UserId = user.Id,
                Username = user.Username,
                Nickname = user.Nickname,
                TenantId = user.TenantId,
                TenantName = tenant.Name,
                Exp = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds()
            };

            var payload = new Dictionary<string, object>
                        {
                            {"flag", loginUser.Flag},
                            {"userId", loginUser.UserId},
                            {"username", loginUser.Username},
                            {"nickname", loginUser.Nickname},
                            {"tenantId", loginUser.TenantId},
                            {"tenantName", loginUser.TenantName},
                            {"exp", loginUser.Exp}
                        };

            var token = JwtHelper.CreateToken(payload);

            var key = RedisKeyManger.GetTokenKey(flag, user.Id);

            RedisHelper.StringSet(key, token, null);

            var userData = user;

            var routeList = new List<RouteOutputDto>();
            var roleList = new List<RoleOutputDto>();

            var root = new RouteOutputDto
            {
                Router = "root",
                Children = new List<RouteOutputDto>()
            };

            var permissionData = new List<PermissionOutputDto>();

            if (userData.ManagerType == EManagerType.SuperAdmin)
            {
                routeList = new List<RouteOutputDto>() {
                    new RouteOutputDto
                            {
                                Router = "home",
                                Name = "控制台",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "home",
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "home_workplace",
                                        Meta = new MetaOutputDto
                                        {
                                            Page = new PageOutputDto
                                            {
                                                Closable = false
                                            },
                                        },
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "home_analysis",
                                        Meta = new MetaOutputDto
                                        {
                                            Page = new PageOutputDto
                                            {
                                                Closable = false
                                            },
                                        },
                                    }
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "my",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "home",
                                    Invisible = true
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "my_profile",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "my_updatepwd",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "organization",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "apartment"
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "organization_employee"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "organization_department"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "organization_company"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "organization_post"
                                    }
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "permission",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "usergroup-add"
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "permission_role"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_roleAllot",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_roleMenu",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_roleData",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_secondAdmin"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_tenant"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_tenantPermission",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "permission_audit"
                                    }
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "setting",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "setting"
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "setting_module"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_moduleadd",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_moduleedit",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_moduledetail",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_api"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_platform"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_permission"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_config"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_dicttype"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_dicttypeadd",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_dicttypeedit",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "setting_dicttypedetail",
                                        Meta = new MetaOutputDto
                                        {
                                            Invisible = true
                                        }
                                    },
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "weixin",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "setting"
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "weixin_setting"
                                    },
                                }
                            },
                            new RouteOutputDto
                            {
                                Router = "analysis",
                                Meta = new MetaOutputDto
                                {
                                    Icon = "thunderbolt"
                                },
                                Children = new List<RouteOutputDto>()
                                {
                                    new RouteOutputDto
                                    {
                                        Router = "analysis_log"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "analysis_dbState"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "analysis_cacheState"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "analysis_serverState"
                                    },
                                    new RouteOutputDto
                                    {
                                        Router = "analysis_online"
                                    }
                                }
                            }
                };

                root.Children = routeList;

                var permissions = await _permissionRepository
                    .GetListAsync();

                permissionData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(permissions);
            }
            else if (userData.ManagerType == EManagerType.TenantAdmin)
            {
                var tenantPermissionQueryable = await _tenantPermissionRepository
                    .WithDetailsAsync(m => m.Permission, m => m.Permission.Platform);

                tenantPermissionQueryable = tenantPermissionQueryable.Where(m => !m.Permission.IsDeleted && m.Permission.Platform.Key == flag);

                var permissions = await tenantPermissionQueryable
                .IgnoreQueryFilters()
                .Where(m => m.TenantId == user.TenantId)
                .OrderByDescending(m => m.Permission.TreeSort)
                .ThenBy(m => m.Permission.Id)
                .Select(m => m.Permission)
                .ToListAsync();

                permissionData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(permissions);

                var topData = permissionData.Where(m => m.TreeLevel == 0).ToList();
                var leafData = permissionData.Where(m => m.TreeLevel != 0).ToList();

                var data = new List<RouteOutputDto>();

                foreach (var item in topData)
                {
                    var route = new RouteOutputDto
                    {
                        Router = item.TreeKeys,
                        Meta = new MetaOutputDto()
                        {
                            Page = new PageOutputDto()
                        },
                        Children = new List<RouteOutputDto>()
                    };

                    route.Meta.Icon = item.Icon;
                    route.Meta.Invisible = !item.IsShow;
                    route.Meta.Page.Closable = item.Closeable;

                    if (!String.IsNullOrWhiteSpace(item.Params))
                    {
                        var json = JsonSerializer.Deserialize<dynamic>(item.Params);

                        route.Meta.Query = json;
                    }

                    ReGetTree(item, route, leafData);

                    data.Add(route);
                }

                root.Children = data;
            }
            else if (userData.ManagerType == EManagerType.TenantUser)
            {
                var userRoleQueryable = await _userRoleRepository.WithDetailsAsync(m => m.Role);

                var roles = await userRoleQueryable
                .IgnoreQueryFilters()
                .Where(m => m.UserId == user.Id)
                .Select(m => m.Role)
                .ToListAsync();

                roleList = _objectMapper.Map<List<Role>, List<RoleOutputDto>>(roles);

                var roleIds = roles
                .Select(m => m.Id)
                .ToList();

                var roleMenuQueryable = await _rolePermissionRepository
                    .WithDetailsAsync(m => m.Role, m => m.Permission, m => m.Permission.Platform);

                roleMenuQueryable = roleMenuQueryable.Where(m => m.Permission.Platform.Key == flag);

                var permissions = await roleMenuQueryable
                .IgnoreQueryFilters()
                .Where(m => roleIds.Contains(m.RoleId))
                .OrderByDescending(m => m.Permission.TreeSort)
                .ThenBy(m => m.Permission.Id)
                .Select(m => m.Permission)
                .ToListAsync();

                permissions = permissions.Distinct().ToList();

                permissionData = _objectMapper.Map<List<Permission>, List<PermissionOutputDto>>(permissions);

                var topData = permissionData.Where(m => m.TreeLevel == 0).ToList();
                var leafData = permissionData.Where(m => m.TreeLevel != 0).ToList();

                var data = new List<RouteOutputDto>();

                foreach (var item in topData)
                {
                    var route = new RouteOutputDto
                    {
                        Router = item.TreeKeys,
                        Meta = new MetaOutputDto()
                        {
                            Page = new PageOutputDto()
                        },
                        Children = new List<RouteOutputDto>()
                    };

                    route.Meta.Icon = item.Icon;
                    route.Meta.Invisible = !item.IsShow;
                    route.Meta.Page.Closable = item.Closeable;

                    if (!String.IsNullOrWhiteSpace(item.Params))
                    {
                        var json = JsonSerializer.Deserialize<dynamic>(item.Params);

                        route.Meta.Query = json;
                    }

                    ReGetTree(item, route, leafData);

                    data.Add(route);
                }

                root.Children = data;
            }

            routeList = new List<RouteOutputDto>()
            {
                root
            };

            return new LoginOutputDto()
            {
                Token = token,
                LoginUser = loginUser,
                User = userData,
                PermissionList = permissionData,
                RouteList = routeList,
                RoleList = roleList
            };
        }

        private void ReGetTree(PermissionOutputDto source, RouteOutputDto target, List<PermissionOutputDto> data)
        {
            source.Children = new List<PermissionOutputDto>();
            target.Children = new List<RouteOutputDto>();

            var nextData = data.Where(m => m.ParentId == source.Id).ToList();

            foreach (var item in nextData)
            {
                var route = new RouteOutputDto();

                route.Router = item.TreeKeys;
                route.Meta = new MetaOutputDto()
                {
                    Page = new PageOutputDto()
                };

                route.Meta.Icon = item.Icon;
                route.Meta.Invisible = !item.IsShow;
                route.Meta.Page.Closable = item.Closeable;

                if (!String.IsNullOrWhiteSpace(item.Params))
                {
                    var json = JsonSerializer.Deserialize<dynamic>(item.Params);

                    route.Meta.Query = json;
                }

                source.Children.Add(item);
                target.Children.Add(route);

                ReGetTree(item, route, data);
            }
        }

        public async Task<LoginOutputDto> ActiveAsync(AccountActiveInputDto input)
        {
            using var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);

            var queryable = await _userRepository.GetQueryableAsync();

            var password = CommonHelper.Encrypt(input.Password);

            queryable = queryable
                .IgnoreQueryFilters()
                .Where(m => m.Username == input.Username &&
                m.Password == password);

            var user = await _userRepository.AsyncExecuter
                .SingleOrDefaultAsync(queryable);

            if (user == null) throw new UserFriendlyException("账号或密码错误");

            var tenant = await _tenantRepository.GetAsync(user.TenantId);

            var queryableLoginAccountUser = await _loginAccountUserRepository
                .WithDetailsAsync(m => m.LoginAccount, m => m.User);

            queryableLoginAccountUser = queryableLoginAccountUser
                .IgnoreQueryFilters()
                .Where(m => m.UserId == user.Id);

            var loginAccountUser = await _loginAccountUserRepository.AsyncExecuter
                .SingleOrDefaultAsync(queryableLoginAccountUser);

            if (loginAccountUser == null)
            {
                var loginAccount = await _loginAccountRepository
                    .SingleOrDefaultAsync(m => m.WxAppOpenId == input.WxAppOpenId);

                if (loginAccount == null)
                {
                    loginAccount = new LoginAccount()
                    {
                        WxAppOpenId = input.WxAppOpenId,
                        WxUnionId = input.WxUnionId,
                        Mobile = input.Username,
                        Password = password
                    };

                    loginAccount.CreateKey();

                    await _loginAccountRepository.InsertAsync(loginAccount);
                }

                loginAccountUser = new LoginAccountUser()
                {
                    LoginAccountId = loginAccount.Id,
                    UserId = user.Id
                };

                loginAccountUser.CreateKey();

                await _loginAccountUserRepository.InsertAsync(loginAccountUser);
            }
            else
            {
                if (loginAccountUser.LoginAccount.WxAppOpenId != input.WxAppOpenId)
                {
                    throw new UserFriendlyException("账户已被使用");
                }
            }

            await uow.CompleteAsync();

            var data = await _LoginAsync(_objectMapper.Map<Tenant, TenantOutputDto>(tenant), _objectMapper.Map<User, UserOutputDto>(user));

            return data;
        }
    }
}
