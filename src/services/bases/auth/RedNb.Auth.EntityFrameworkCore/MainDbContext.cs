using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using RedNb.Core.Data;
using Microsoft.AspNetCore.Http;

namespace RedNb.Auth.Data
{
    public class MainDbContext : DbContextBase<MainDbContext>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        #region Admins

        public virtual DbSet<LoginAccount> LoginAccounts { get; set; }
        public virtual DbSet<LoginAccountUser> LoginAccountUsers { get; set; }
        public virtual DbSet<Api> Apis { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<DictData> DictDatas { get; set; }
        public virtual DbSet<DictType> DictTypes { get; set; }
        public virtual DbSet<Instance> Instances { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleCallback> ModuleCallbacks { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionApi> PermissionApis { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<RateLimit> RateLimits { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleDataScope> RoleDataScopes { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantModule> TenantModules { get; set; }
        public virtual DbSet<TenantPermission> TenantPermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserDataScope> UserDataScopes { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WhiteList> WhiteLists { get; set; }

        #endregion

        #region Offices

        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<CompanyDepartment> CompanyDepartments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual DbSet<EmployeePost> EmployeePosts { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Permission>().Property(p => p.TreeSort).HasPrecision(10, 0);
            builder.Entity<Company>().Property(p => p.TreeSort).HasPrecision(10, 0);
            builder.Entity<Department>().Property(p => p.TreeSort).HasPrecision(10, 0);
        }
    }
}
