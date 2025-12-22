using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using LifeQuestAPI.Persistence.Contexts;

namespace LifeQuestAPI.Persistence.Repositories;

public sealed class AppUserWriteRepository : WriteRepository<AppUser>, IAppUserWriteRepository
{
    public AppUserWriteRepository(LifeQuestDbContext context) : base(context)
    {
    }
}