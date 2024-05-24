using EmployeeApi.Data;
using EmployeeApi.Factories;
using EmployeeApi.Repositories;
using EmployeeApi.Services;
using EmployeeApi.Services.impls;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddTransient(typeof(IRepository<>), typeof(Repository<>))
            .AddTransient<IPersistence, DbPersistence>();
    }
    
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddTransient<IGroupService, GroupService>()
            .AddTransient<DatabaseSeeder>();

    }
}