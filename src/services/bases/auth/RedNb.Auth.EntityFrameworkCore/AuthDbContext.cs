using RedNb.Core.Data;
using RedNb.Auth.Domain.Companys;
using RedNb.Auth.Domain.Departments;
using RedNb.Auth.Domain.Dicts;
using RedNb.Auth.Domain.Employees;
using RedNb.Auth.Domain.Menus;
using RedNb.Auth.Domain.Platforms;
using RedNb.Auth.Domain.Posts;
using RedNb.Auth.Domain.Products;
using RedNb.Auth.Domain.Roles;
using RedNb.Auth.Domain.Services;
using RedNb.Auth.Domain.Tenants;
using RedNb.Auth.Domain.Users;
using RedNb.Auth.Domain.Views;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RedNb.Auth.EntityFrameworkCore;

public class AuthDbContext : BaseDbContext<AuthDbContext>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {

    }

    public DbSet<Company> Companys { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DictData> DictDatas { get; set; }
    public DbSet<DictType> DictTypes { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    public DbSet<EmployeePost> EmployeePosts { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuApi> MenuApis { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleDataScope> RoleDataScopes { get; set; }
    public DbSet<RoleMenu> RoleMenus { get; set; }
    public DbSet<Api> Apis { get; set; }
    public DbSet<Config> Configs { get; set; }
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDataScope> UserDataScopes { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<View> Views { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CompanyDepartment>(b =>
        {
            b.HasNoKey();
        });

        builder.Entity<EmployeeDepartment>(b =>
        {
            b.HasNoKey();
        });

        builder.Entity<EmployeePost>(b =>
        {
            b.HasNoKey();
        });

        builder.Entity<RoleMenu>(b =>
        {
            b.HasNoKey();
        });

        builder.Entity<UserRole>(b =>
        {
            b.HasNoKey();
        });
    }
}