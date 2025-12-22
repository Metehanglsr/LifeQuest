using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Abstractions.Gamification;
using LifeQuestAPI.Application.Abstractions.Token;
using LifeQuestAPI.Infrastructure.Services.Gamification;
using LifeQuestAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace LifeQuestAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IGamificationService, GamificationService>();
    }
}