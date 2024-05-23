using EmployeeApi.Data;
using EmployeeApi.Repositories;
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
            .AddTransient(typeof(IRepository<>),typeof(Repository<>))
            .AddTransient<IPersistence, DbPersistence>();
    }
}