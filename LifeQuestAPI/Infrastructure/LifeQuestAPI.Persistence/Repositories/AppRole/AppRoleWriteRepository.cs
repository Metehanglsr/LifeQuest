using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class AppRoleWriteRepository : WriteRepository<AppRole>, IAppRoleWriteRepository
{
    public AppRoleWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}