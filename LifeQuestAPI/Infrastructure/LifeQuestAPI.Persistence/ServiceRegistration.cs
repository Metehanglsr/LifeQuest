using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Persistence.Contexts;
using LifeQuestAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeQuestAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LifeQuestDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IAppUserReadRepository, AppUserReadRepository>();
        services.AddScoped<IAppUserWriteRepository, AppUserWriteRepository>();

        services.AddScoped<IAppRoleReadRepository, AppRoleReadRepository>();
        services.AddScoped<IAppRoleWriteRepository, AppRoleWriteRepository>();

        services.AddScoped<IAppTaskReadRepository, AppTaskReadRepository>();
        services.AddScoped<IAppTaskWriteRepository, AppTaskWriteRepository>();

        services.AddScoped<IUserTaskReadRepository, UserTaskReadRepository>();
        services.AddScoped<IUserTaskWriteRepository, UserTaskWriteRepository>();

        services.AddScoped<IBadgeReadRepository, BadgeReadRepository>();
        services.AddScoped<IBadgeWriteRepository, BadgeWriteRepository>();

        services.AddScoped<IUserBadgeReadRepository, UserBadgeReadRepository>();
        services.AddScoped<IUserBadgeWriteRepository, UserBadgeWriteRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        services.AddScoped<IUserCategoryProgressReadRepository, UserCategoryProgressReadRepository>();
        services.AddScoped<IUserCategoryProgressWriteRepository, UserCategoryProgressWriteRepository>();
    }
}